// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Platformus.Barebone;
using Platformus.FileManager.Backend.ViewModels.FileManager;
using Platformus.FileManager.Data.Abstractions;
using Platformus.FileManager.Events;

namespace Platformus.FileManager.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseFileManagerPermission)]
  public class FileManagerController : Barebone.Backend.Controllers.ControllerBase
  {
    private IHostingEnvironment hostingEnvironment;

    public FileManagerController(IStorage storage, IHostingEnvironment hostingEnvironment)
      : base(storage)
    {
      this.hostingEnvironment = hostingEnvironment;
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

        using (FileStream output = System.IO.File.Create(this.GetFilepath(filename)))
          await source.CopyToAsync(output);

        Platformus.FileManager.Data.Entities.File file = new Platformus.FileManager.Data.Entities.File();

        file.Name = filename;
        file.Size = source.Length;
        this.Storage.GetRepository<IFileRepository>().Create(file);
        this.Storage.Save();
        Event<IFileCreatedEventHandler, IRequestHandler, Platformus.FileManager.Data.Entities.File>.Broadcast(this, file);
      }

      return this.RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Platformus.FileManager.Data.Entities.File file = this.Storage.GetRepository<IFileRepository>().WithKey(id);

      try
      {
        System.IO.File.Delete(this.GetFilepath(file.Name));
      }

      catch { }

      this.Storage.GetRepository<IFileRepository>().Delete(file);
      this.Storage.Save();
      Event<IFileDeletedEventHandler, IRequestHandler, Platformus.FileManager.Data.Entities.File>.Broadcast(this, file);
      return this.RedirectToAction("Index");
    }

    public ActionResult FileSelectorForm()
    {
      return this.PartialView("_FileSelectorForm", new FileSelectorFormViewModelFactory(this).Create());
    }

    private string EnsureCorrectFilename(string filename)
    {
      if (filename.Contains(Path.DirectorySeparatorChar.ToString()))
        filename = filename.Substring(filename.LastIndexOf(Path.DirectorySeparatorChar) + 1);

      return filename;
    }

    private string GetFilepath(string filename)
    {
      return Path.Combine(this.hostingEnvironment.WebRootPath, "files", filename);
    }
  }
}