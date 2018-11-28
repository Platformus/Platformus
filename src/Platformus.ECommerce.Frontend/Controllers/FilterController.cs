// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.ECommerce.Frontend.Controllers
{
  [AllowAnonymous]
  public class FilterController : Platformus.Barebone.Frontend.Controllers.ControllerBase
  {
    public FilterController(IStorage storage)
      : base(storage)
    {
    }

    [HttpPost]
    public IActionResult Index(string redirectUrl)
    {
      StringBuilder attributeIds = new StringBuilder();

      foreach (string key in this.Request.Form.Keys)
        if (key.StartsWith("attribute"))
          attributeIds.Append((attributeIds.Length == 0 ? null : ",") + key.Replace("attribute", string.Empty));

      return this.Redirect($"{redirectUrl}{(attributeIds.Length == 0 ? null : "?attributeids=")}{attributeIds.ToString()}");
    }
  }
}