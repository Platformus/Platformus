// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;
using Platformus.Barebone.Backend.Controllers;
using Platformus.Static.Backend.ViewModels.Files;
using Platformus.Static.Data.Abstractions;
using Platformus.Static.Data.Models;

namespace Platformus.Static.Backend.Controllers
{
  [Area("Backend")]
  public class FilesController : ControllerBase
  {
    public IApplicationEnvironment ApplicationEnvironment { get; private set; }

    public FilesController(IStorage storage, IApplicationEnvironment hostingEnvironment)
      : base(storage)
    {
      this.ApplicationEnvironment = hostingEnvironment;
    }

    public IActionResult Index(string orderBy = "name", string direction = "asc", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelBuilder(this).Build(orderBy, direction, skip, take));
    }

    [HttpPost]
    public async Task<IActionResult> Create(IList<IFormFile> files)
    {
      foreach (IFormFile source in files)
      {
        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

        filename = this.EnsureCorrectFilename(filename);
        await source.SaveAsAsync(this.GetPathAndFilename(filename));

        File file = new File();

        file.Name = filename;
        file.Size = source.Length;
        this.Storage.GetRepository<IFileRepository>().Create(file);
        this.Storage.Save();
      }
      
      return this.RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      File file = this.Storage.GetRepository<IFileRepository>().WithKey(id);

      try
      {
        System.IO.File.Delete(this.GetPathAndFilename(file.Name));
      }

      catch { }

      this.Storage.GetRepository<IFileRepository>().Delete(file);
      this.Storage.Save();
      return this.RedirectToAction("Index");
    }

    private string EnsureCorrectFilename(string filename)
    {
      if (filename.Contains("\\"))
        filename = filename.Substring(filename.LastIndexOf("\\") + 1);

      return filename;
    }

    private string GetPathAndFilename(string filename)
    {
      return this.ApplicationEnvironment.ApplicationBasePath + "\\wwwroot\\files\\" + filename;
    }
  }
}