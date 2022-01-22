// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public static class PropertyViewModelFactory
  {
    public static PropertyViewModel Create(HttpContext httpContext, Property property)
    {
      IEnumerable<Localization> localizations = httpContext.GetLocalizations(property.StringValue);

      return new PropertyViewModel()
      {
        Id = property.Id,
        Member = property.Member == null ? null : MemberViewModelFactory.Create(property.Member),
        Value = property.GetValue(),
        IntegerValue = property.IntegerValue,
        DecimalValue = property.DecimalValue,
        NeutralStringValue = localizations.First(l => l.Culture.Id == NeutralCulture.Id),
        LocalizedStringValues = localizations.Where(l => l.Culture.Id != NeutralCulture.Id).ToList(),
        DateTimeValue = property.DateTimeValue
      };
    }
  }
}