// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;

namespace Platformus.Globalization.Frontend.ViewModels.Shared
{
  public class CulturesViewModelFactory : ViewModelFactoryBase
  {
    public CulturesViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CulturesViewModel Create(string partialViewName, string additionalCssClass)
    {
      return new CulturesViewModel()
      {
        Cultures = this.RequestHandler.GetService<ICultureManager>().GetNotNeutralCultures().Select(
          c => new CultureViewModelFactory(this.RequestHandler).Create(c)
        ).ToList(),
        PartialViewName = partialViewName ?? "_Cultures",
        AdditionalCssClass = additionalCssClass
      };
    }
  }
}