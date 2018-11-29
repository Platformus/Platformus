// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.Localization;
using Platformus.Barebone.Primitives;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class TakeSelectorViewModelFactory : ViewModelFactoryBase
  {
    public TakeSelectorViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public TakeSelectorViewModel Create(int take)
    {
      IStringLocalizer<TakeSelectorViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<TakeSelectorViewModelFactory>>();

      return new TakeSelectorViewModel()
      {
        Take = take,
        TakeOptions = new Option[] {
          new Option(localizer["By 10"], "10"),
          new Option(localizer["By 25"], "25"),
          new Option(localizer["By 50"], "50"),
          new Option(localizer["By 100"], "100")
        }
      };
    }
  }
}