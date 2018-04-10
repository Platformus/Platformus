// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
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
      Culture frontendDefaultCulture = requestHandler.GetService<ICultureManager>().GetFrontendDefaultCulture();

      if (frontendDefaultCulture != null)
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
            {
              if (dataType.JavaScriptEditorClassName == "booleanFlag")
              {
                bool value = property.IntegerValue == null || property.IntegerValue == 0 ? false : true;
                string displayValue = value ?
                  true.ToString(CultureInfo.CurrentUICulture).ToLower() : false.ToString(CultureInfo.CurrentUICulture).ToLower();
                string modifierCssClass = value ? "marker--positive" : "marker--negative";

                properties.Add($"<span class=\"marker {modifierCssClass}\">{displayValue}</span>");
              }

              else properties.Add(property.IntegerValue == null ? string.Empty : property.IntegerValue.ToString());
            }

            else if (dataType.StorageDataType == StorageDataType.Decimal)
              properties.Add(property.DecimalValue == null ? string.Empty : property.DecimalValue.ToString());

            else if (dataType.StorageDataType == StorageDataType.String)
            {
              Localization localization = null;

              if (member.IsPropertyLocalizable == true && frontendDefaultCulture != null)
                localization = this.requestHandler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId((int)property.StringValueId, frontendDefaultCulture.Id);

              else if (neutralCulture != null)
                localization = this.requestHandler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId((int)property.StringValueId, neutralCulture.Id);

              if (localization == null)
                properties.Add(string.Empty);

              else
              {
                if (dataType.JavaScriptEditorClassName == "image")
                  properties.Add($"<img class=\"table__image\" src=\"{localization.Value}\" />");

                else properties.Add(localization.Value);
              }
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