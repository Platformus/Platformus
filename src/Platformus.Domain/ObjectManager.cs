// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain
{
  public class ObjectManager
  {
    private IRequestHandler handler;

    public ObjectManager(IRequestHandler requestHandler)
    {
      this.handler = handler;
    }

    public IEnumerable<string> GetDisplayProperties(Object @object)
    {
      List<string> properties = new List<string>();

      Culture defaultCulture = CultureManager.GetDefaultCulture(this.handler.Storage);

      if (defaultCulture != null)
      {
        foreach (Member member in this.handler.Storage.GetRepository<IMemberRepository>().FilteredByClassIdInlcudingParentPropertyVisibleInList(@object.ClassId))
        {
          Property property = this.handler.Storage.GetRepository<IPropertyRepository>().WithObjectIdAndMemberId(@object.Id, member.Id);

          if (property == null)
            properties.Add(string.Empty);

          else
          {
            Localization localization = this.handler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId(property.HtmlId, defaultCulture.Id);

            if (localization == null)
              properties.Add(string.Empty);

            else properties.Add(localization.Value);
          }
        }
      }

      return properties;
    }
  }
}