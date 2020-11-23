// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Parameters;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.FormHandlers
{
  public interface IFormHandler
  {
    IEnumerable<ParameterGroup> ParameterGroups { get; }
    string Description { get; }

    Task<IActionResult> HandleAsync(HttpContext httpContext, Form form, IDictionary<Field, string> valuesByFields, IDictionary<string, byte[]> attachmentsByFilenames);
  }
}