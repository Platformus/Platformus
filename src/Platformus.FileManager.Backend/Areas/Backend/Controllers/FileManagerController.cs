// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
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
using Platformus.FileManager.Backend.ViewModels.FileManager;
using Platformus.FileManager.Data.Abstractions;

namespace Platformus.FileManager.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseFileManagerPermission)]
  public class FileManagerController : Barebone.Backend.Controllers.ControllerBase
  {
    public IHostingEnvironment HostingEnvironment { get; private set; }

    public FileManagerController(IStorage storage, IHostingEnvironment hostingEnvironment)
      : base(storage)
    {
      this.HostingEnvironment = hostingEnvironment;
    }

    public IActionResult Index(string orderBy = "name", string direction = "asc", int skip = 0, int take = 10, string filter = null)
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

        using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
          await source.CopyToAsync(output);

        Platformus.FileManager.Data.Entities.File file = new Platformus.FileManager.Data.Entities.File();

        file.Name = filename;
        file.Size = source.Length;
        this.Storage.GetRepository<IFileRepository>().Create(file);
        this.Storage.Save();
      }

      return this.RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Platformus.FileManager.Data.Entities.File file = this.Storage.GetRepository<IFileRepository>().WithKey(id);

      try
      {
        System.IO.File.Delete(this.GetPathAndFilename(file.Name));
      }

      catch { }

      this.Storage.GetRepository<IFileRepository>().Delete(file);
      this.Storage.Save();
      return this.RedirectToAction("Index");
    }

    public ActionResult FileSelectorForm()
    {
      return this.PartialView("_FileSelectorForm", new FileSelectorFormViewModelFactory(this).Create());
    }

    private string EnsureCorrectFilename(string filename)
    {
      if (filename.Contains("\\"))
        filename = filename.Substring(filename.LastIndexOf("\\") + 1);

      return filename;
    }

    private string GetPathAndFilename(string filename)
    {
      return this.HostingEnvironment.WebRootPath + "\\files\\" + filename;
    }
  }
}