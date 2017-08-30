// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Domain
{
  public class ObjectManager
  {
    private IRequestHandler requestHandler;

    public ObjectManager(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
    }

    public string GetUrlPropertyStringValue(Object @object)
    {
      foreach (Member member in this.requestHandler.Storage.GetRepository<IMemberRepository>().FilteredByClassIdInlcudingParent(@object.ClassId).ToList())
      {
        if (member.Code == "Url")
        {
          Property property = this.requestHandler.Storage.GetRepository<IPropertyRepository>().WithObjectIdAndMemberId(@object.Id, member.Id);

          if (property == null)
            return null;

          Culture neutralCulture = requestHandler.GetService<ICultureManager>().GetNeutralCulture();
          Localization localization = null;

          if (neutralCulture != null)
            localization = this.requestHandler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId((int)property.StringValueId, neutralCulture.Id);

          return localization?.Value;
        }
      }

      return null;
    }

    public IEnumerable<string> GetDisplayProperties(Object @object)
    {
      List<string> properties = new List<string>();
      Culture neutralCulture = requestHandler.GetService<ICultureManager>().GetNeutralCulture();
      Culture defaultCulture = requestHandler.GetService<ICultureManager>().GetDefaultCulture();

      if (defaultCulture != null)
      {
        foreach (Member member in this.requestHandler.Storage.GetRepository<IMemberRepository>().FilteredByClassIdInlcudingParentPropertyVisibleInList(@object.ClassId).ToList())
        {
          Property property = this.requestHandler.Storage.GetRepository<IPropertyRepository>().WithObjectIdAndMemberId(@object.Id, member.Id);

          if (property == null)
            properties.Add(string.Empty);

          else
          {
            DataType dataType = this.requestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId);

            if (dataType.StorageDataType == StorageDataType.Integer)
              properties.Add(property.IntegerValue == null ? string.Empty : property.IntegerValue.ToString());

            else if (dataType.StorageDataType == StorageDataType.Decimal)
              properties.Add(property.DecimalValue == null ? string.Empty : property.DecimalValue.ToString());

            else if (dataType.StorageDataType == StorageDataType.String)
            {
              Localization localization = null;

              if (member.IsPropertyLocalizable == true && defaultCulture != null)
                localization = this.requestHandler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId((int)property.StringValueId, defaultCulture.Id);

              else if (neutralCulture != null)
                localization = this.requestHandler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId((int)property.StringValueId, neutralCulture.Id);

              if (localization == null)
                properties.Add(string.Empty);

              else properties.Add(localization.Value);
            }

            else if (dataType.StorageDataType == StorageDataType.DateTime)
              properties.Add(property.DateTimeValue == null ? string.Empty : property.DateTimeValue.ToString());
          }
        }
      }

      return properties;
    }
  }
}