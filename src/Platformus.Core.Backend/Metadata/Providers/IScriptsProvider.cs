// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Platformus.Core.Backend.Metadata.Providers
{
  public interface IScriptsProvider
  {
    IEnumerable<Script> GetScripts(HttpContext httpContext);
  }
}