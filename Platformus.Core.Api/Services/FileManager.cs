// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Platformus.Core.Api.Services.Abstractions;

namespace Platformus.Core.Api.Services;

public class FileManager : IFileManager
{
  private readonly FileManagerOptions fileManagerOptions;

  public FileManager(IOptions<FileManagerOptions> fileManagerOptions)
  {
    this.fileManagerOptions = fileManagerOptions.Value;
  }

  public async Task<bool> WriteAsync(string destination, IFormFile file)
  {
    try
    {
      destination = Path.Combine(this.fileManagerOptions.RootPath!, destination);

      string path = Path.GetDirectoryName(destination)!;

      if (!await Task.Run(() => Directory.Exists(path)))
        await Task.Run(() => Directory.CreateDirectory(path));

      using (FileStream stream = new FileStream(destination, FileMode.Create))
        await file.CopyToAsync(stream);

      return true;
    }

    catch { }

    return false;
  }

  public async Task<bool> MoveAsync(string source, string destination)
  {
    try
    {
      source = Path.Combine(this.fileManagerOptions.RootPath!, source);
      destination = Path.Combine(this.fileManagerOptions.RootPath!, destination);

      string path = Path.GetDirectoryName(destination)!;

      if (!await Task.Run(() => Directory.Exists(path)))
        await Task.Run(() => Directory.CreateDirectory(path));

      await Task.Run(() => File.Move(source, destination));
      return true;
    }

    catch { }

    return false;
  }

  public async Task<bool> DeleteAsync(string source)
  {
    try
    {
      source = Path.Combine(this.fileManagerOptions.RootPath!, source);
      await Task.Run(() => File.Delete(source));
    }

    catch { }

    return false;
  }
}
