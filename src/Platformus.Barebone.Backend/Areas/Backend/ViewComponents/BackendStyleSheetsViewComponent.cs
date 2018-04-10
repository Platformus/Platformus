// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone.Backend.Metadata.Providers;

namespace Platformus.Barebone.Backend.ViewComponents
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
      return this.View(this.styleSheetsProvider.GetStyleSheets(this));
    }
  }
}