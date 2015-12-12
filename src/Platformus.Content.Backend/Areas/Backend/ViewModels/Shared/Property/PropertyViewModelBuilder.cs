// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Globalization.Data.Abstractions;

namespace Platformus.Content.Backend.ViewModels.Shared
{
  public class PropertyViewModelBuilder : ViewModelBuilderBase
  {
    public PropertyViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public PropertyViewModel Build(Property property)
    {
      return new PropertyViewModel()
      {
        Id = property.Id,
        HtmlLocalizations = this.GetLocalizations(
          this.handler.Storage.GetRepository<IDictionaryRepository>().WithKey(property.HtmlId)
        )
      };
    }
  }
}