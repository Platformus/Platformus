// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.Metadata.Providers;

namespace Platformus.Core.Backend.ViewComponents
{
  public class BackendStyleSheetsViewComponent : ViewComponentBase
  {
    private IStyleSheetsProvider styleSheetsProvider;

    public BackendStyleSheetsViewComponent(IStorage storage, IStyleSheetsProvider styleSheetsProvider)
      : base(storage)
    {
      this.styleSheetsProvider = styleSheetsProvider;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      return this.View(this.styleSheetsProvider.GetStyleSheets(this.HttpContext));
    }
  }
}