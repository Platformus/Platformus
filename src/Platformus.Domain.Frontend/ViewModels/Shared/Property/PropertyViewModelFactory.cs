// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization;
using Platformus.Globalization.Data.Models;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Domain.Frontend.ViewModels.Shared
{
  public class PropertyViewModelFactory : ViewModelFactoryBase
  {
    public PropertyViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public PropertyViewModel Create(Property property)
    {
      Member member = this.RequestHandler.Storage.GetRepository<IMemberRepository>().WithKey(property.MemberId);
      Culture neutralCulture = CultureManager.GetNeutralCulture(this.RequestHandler.Storage);

      return new PropertyViewModel()
      {
        MemberCode = member.Code,
        Html = member.IsPropertyLocalizable == true ? this.GetLocalizationValue(property.HtmlId) : this.GetLocalizationValue(property.HtmlId, neutralCulture.Id)
      };
    }

    public PropertyViewModel Create(CachedProperty cachedProperty)
    {
      return new PropertyViewModel()
      {
        MemberCode = cachedProperty.MemberCode,
        Html = cachedProperty.Html
      };
    }
  }
}