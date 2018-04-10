// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Barebone.Backend.Controllers
{
  [Area("Backend")]
  public class PhotoUploaderController : ControllerBase
  {
    private IHostingEnvironment hostingEnvironment;

    public PhotoUploaderController(IStorage storage, IHostingEnvironment hostingEnvironment)
      : base(storage)
    {
      this.hostingEnvironment = hostingEnvironment;
    }

    [HttpGet]
    public ActionResult Index()
    {
      return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(IList<IFormFile> files)
    {
      StringBuilder filenames = new StringBuilder();

      foreach (IFormFile source in files)
      {
        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

        filename = this.EnsureCorrectFilename(filename);

        using (FileStream output = System.IO.File.Create(this.GetTempFilepath(filename)))
          await source.CopyToAsync(output);

        if (filenames.Length != 0)
          filenames.Append(',');

        filenames.Append(filename);
      }

      return this.Content("filenames=" + filenames.ToString());
    }

    private string EnsureCorrectFilename(string filename)
    {
      if (filename.Contains(Path.DirectorySeparatorChar.ToString()))
        filename = filename.Substring(filename.LastIndexOf(Path.DirectorySeparatorChar) + 1);

      return filename;
    }

    private string GetTempFilepath(string filename)
    {
      return this.GetFilepath(this.GetTempBasePath(), filename);
    }

    private string GetFilepath(string basePath, string filename)
    {
      basePath = basePath.Replace('/', '\\');

      return this.hostingEnvironment.WebRootPath + basePath.Replace('\\', Path.DirectorySeparatorChar) + filename;
    }

    private string GetTempBasePath()
    {
      char directorySeparatorChar = Path.DirectorySeparatorChar;

      return $"{directorySeparatorChar}images{directorySeparatorChar}temp{directorySeparatorChar}";
    }
  }
}