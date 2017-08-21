// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Platformus.ExtensionManager.Backend.ViewModels.ExtensionManager;

namespace Platformus.ExtensionManager.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseExtensionManagerPermission)]
  public class ExtensionManagerController : Platformus.Barebone.Backend.Controllers.ControllerBase
  {
    private IHostingEnvironment hostingEnvironment;

    public ExtensionManagerController(IStorage storage, IHostingEnvironment hostingEnvironment)
      : base(storage)
    {
      this.hostingEnvironment = hostingEnvironment;
    }

    public ActionResult Index(string orderBy = "filename", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(orderBy, direction, skip, take, filter));
    }

    [HttpPost]
    public async Task<IActionResult> Create(IList<IFormFile> files)
    {
      foreach (IFormFile source in files)
      {
        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

        filename = this.EnsureCorrectFilename(filename);

        string pathAndFilename = this.GetPathAndFilename(filename);

        using (FileStream output = System.IO.File.Create(pathAndFilename))
          await source.CopyToAsync(output);

        new ExtensionManager(this).UnzipExtensionArchive(pathAndFilename);
      }

      return this.RedirectToAction("Index");
    }

    public ActionResult View(string id)
    {
      return this.View(new ViewViewModelFactory(this).Create(id));
    }

    public ActionResult Delete(string id)
    {
      return this.View(new DeleteViewModelFactory(this).Create(id));
    }

    private string EnsureCorrectFilename(string filename)
    {
      if (filename.Contains("\\"))
        filename = filename.Substring(filename.LastIndexOf("\\") + 1);

      return filename;
    }

    private string GetPathAndFilename(string filename)
    {
      return this.hostingEnvironment.WebRootPath + "\\temp\\" + filename;
    }
  }
}