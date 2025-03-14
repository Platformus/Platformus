// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Platformus.Core.Api.Services.Abstractions;

public interface IFileManager
{
  Task<bool> WriteAsync(string destination, IFormFile file);
  Task<bool> MoveAsync(string source, string destination);
  Task<bool> DeleteAsync(string source);
}