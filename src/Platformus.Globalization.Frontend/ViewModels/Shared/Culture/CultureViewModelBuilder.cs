// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization.Frontend.ViewModels.Shared
{
  public class CultureViewModelBuilder : ViewModelBuilderBase
  {
    public CultureViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public CultureViewModel Build(Culture culture)
    {
      return new CultureViewModel()
      {
        Code = culture.Code,
        Name = culture.Name
      };
    }
  }
}