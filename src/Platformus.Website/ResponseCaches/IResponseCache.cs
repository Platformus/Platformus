// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platformus.Website.ResponseCaches
{
  public interface IResponseCache
  {
    public Task<byte[]> GetWithDefaultValueAsync(HttpContext httpContext, Func<Task<byte[]>> defaultValueFunc);
    public Task RemoveAllAsync(HttpContext httpContext);
  }
}