// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Platformus.Barebone
{
  public interface IRequestHandler
  {
    HttpContext HttpContext { get; }
    IStorage Storage { get; }
  }
}