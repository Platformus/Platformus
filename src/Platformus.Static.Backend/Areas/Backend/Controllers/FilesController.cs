// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Platformus.Static.Backend.ViewModels.Files;
using Platformus.Static.Data.Abstractions;

namespace Platformus.Static.Backend.Controllers
{
  [Area("Backend")]
  public class FilesController : Barebone.Backend.Controllers.ControllerBase
  {
    public IHostingEnvironment HostingEnvironment { get; private set; }

    public FilesController(IStorage storage, IHostingEnvironment hostingEnvironment)
      : base(storage)
    {
      this.HostingEnvironment = hostingEnvironment;
    }

    public IActionResult Index(string orderBy = "name", string direction = "asc", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory(this).Create(orderBy, direction, skip, take));
    }

    [HttpPost]
    public async Task<IActionResult> Create(IList<IFormFile> files)
    {
      foreach (IFormFile source in files)
      {
        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

        filename = this.EnsureCorrectFilename(filename);

        using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
          await source.CopyToAsync(output);

        Platformus.Static.Data.Models.File file = new Platformus.Static.Data.Models.File();

        file.Name = filename;
        file.Size = source.Length;
        this.Storage.GetRepository<IFileRepository>().Create(file);
        this.Storage.Save();
      }
      
      return this.RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Platformus.Static.Data.Models.File file = this.Storage.GetRepository<IFileRepository>().WithKey(id);

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
      return this.HostingEnvironment.WebRootPath + "\\files\\" + filename;
    }
  }
}