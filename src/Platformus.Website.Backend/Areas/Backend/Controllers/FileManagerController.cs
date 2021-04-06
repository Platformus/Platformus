// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Platformus.Website.Backend.ViewModels.FileManager;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageFileManagerPermission)]
  public class FileManagerController : Core.Backend.Controllers.ControllerBase
  {
    private IWebHostEnvironment webHostEnvironment;

    private IRepository<int, Data.Entities.File, FileFilter> Repository
    {
      get => this.Storage.GetRepository<int, Data.Entities.File, FileFilter>();
    }

    public FileManagerController(IStorage storage, IWebHostEnvironment webHostEnvironment)
      : base(storage)
    {
      this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> IndexAsync([FromQuery]FileFilter filter = null, string orderBy = "+name", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory().Create(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(filter, orderBy, skip, take),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
      ));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(IList<IFormFile> files)
    {
      foreach (IFormFile source in files)
      {
        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

        filename = this.EnsureCorrectFilename(filename);

        using (FileStream output = System.IO.File.Create(this.GetFilepath(filename)))
          await source.CopyToAsync(output);

        Data.Entities.File file = new Data.Entities.File();

        file.Name = filename;
        file.Size = source.Length;
        this.Repository.Create(file);
        await this.Storage.SaveAsync();
        Event<IFileCreatedEventHandler, HttpContext, Data.Entities.File>.Broadcast(this.HttpContext, file);
      }

      return this.RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Data.Entities.File file = await this.Repository.GetByIdAsync(id);

      try
      {
        System.IO.File.Delete(this.GetFilepath(file.Name));
      }

      catch { }

      this.Repository.Delete(file.Id);
      await this.Storage.SaveAsync();
      Event<IFileDeletedEventHandler, HttpContext, Data.Entities.File>.Broadcast(this.HttpContext, file);
      return this.RedirectToAction("Index");
    }

    private string EnsureCorrectFilename(string filename)
    {
      if (filename.Contains(Path.DirectorySeparatorChar.ToString()))
        filename = filename.Substring(filename.LastIndexOf(Path.DirectorySeparatorChar) + 1);

      return filename;
    }

    private string GetFilepath(string filename)
    {
      return Path.Combine(this.webHostEnvironment.WebRootPath, "files", filename);
    }
  }
}