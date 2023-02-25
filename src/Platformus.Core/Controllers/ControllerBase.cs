// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Core.Controllers;

public abstract class ControllerBase : Controller
{
  public IStorage Storage { get; }

  public ControllerBase(IStorage storage)
  {
    this.Storage = storage;
  }

  protected RedirectResult CreateRedirectToSelfResult()
  {
    return this.Redirect(this.Request.Path.Value + this.Request.QueryString.Value);
  }
}