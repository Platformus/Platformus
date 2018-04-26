// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.FormHandlers
{
  public interface IFormHandler
  {
    IEnumerable<FormHandlerParameterGroup> ParameterGroups { get; }
    string Description { get; }

    IActionResult Handle(IRequestHandler requestHandler, Form form, IDictionary<Field, string> valuesByFields, IDictionary<string, byte[]> attachmentsByFilenames, string formPageUrl);
  }
}