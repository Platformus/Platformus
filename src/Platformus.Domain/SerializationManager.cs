// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Data.Entities;
using Platformus.Globalization.Services.Abstractions;

namespace Platformus.Domain
{
  public class SerializationManager
  {
    public IRequestHandler requestHandler;

    public SerializationManager(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
    }

    public void SerializeObject(Object @object)
    {
      foreach (Culture culture in this.requestHandler.GetService<ICultureManager>().GetNotNeutralCultures())
      {
        SerializedObject serializedObject = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().WithKey(culture.Id, @object.Id);

        if (serializedObject == null)
          this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Create(this.SerializeObject(culture, @object));

        else
        {
          SerializedObject temp = this.SerializeObject(culture, @object);

          serializedObject.UrlPropertyStringValue = temp.UrlPropertyStringValue;
          serializedObject.SerializedProperties = temp.SerializedProperties;
          this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Edit(serializedObject);
        }
      }

      this.requestHandler.Storage.Save();
    }

    private SerializedObject SerializeObject(Culture culture, Object @object)
    {
      Class @class = this.requestHandler.Storage.GetRepository<IClassRepository>().WithKey(@object.ClassId);
      Culture neutralCulture = requestHandler.GetService<ICultureManager>().GetNeutralCulture();
      List<SerializedProperty> serializedProperties = new List<SerializedProperty>();

      foreach (Member member in this.requestHandler.Storage.GetRepository<IMemberRepository>().FilteredByClassIdInlcudingParent(@class.Id).ToList())
      {
        if (member.PropertyDataTypeId != null)
        {
          Property property = this.requestHandler.Storage.GetRepository<IPropertyRepository>().WithObjectIdAndMemberId(@object.Id, member.Id);

          serializedProperties.Add(this.SerializeProperty(member.IsPropertyLocalizable == true ? culture : neutralCulture, property));
        }
      }

      SerializedObject serializedObject = new SerializedObject();

      serializedObject.ObjectId = @object.Id;
      serializedObject.CultureId = culture.Id;
      serializedObject.ClassId = @object.ClassId;
      serializedObject.UrlPropertyStringValue = serializedProperties.FirstOrDefault(sp => sp.Member.Code == "Url")?.StringValue;

      if (serializedProperties.Count != 0)
        serializedObject.SerializedProperties = this.SerializeObject(serializedProperties);

      return serializedObject;
    }

    private SerializedProperty SerializeProperty(Culture culture, Property property)
    {
      SerializedProperty serializedProperty = new SerializedProperty();
      Member member = this.requestHandler.Storage.GetRepository<IMemberRepository>().WithKey(property.MemberId);
      DataType dataType = this.requestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId);
      SerializedMember serializedMember = new SerializedMember();

      serializedMember.Code = member.Code;
      serializedMember.PropertyDataTypeStorageDataType = dataType.StorageDataType;
      serializedProperty.Member = serializedMember;

      if (dataType.StorageDataType == StorageDataType.Integer)
        serializedProperty.IntegerValue = property.IntegerValue;

      else if (dataType.StorageDataType == StorageDataType.Decimal)
        serializedProperty.DecimalValue = property.DecimalValue;

      else if (dataType.StorageDataType == StorageDataType.String)
      {
        Culture neutralCulture = this.requestHandler.GetService<ICultureManager>().GetNeutralCulture();
        string stringValue = member.IsPropertyLocalizable == true ?
          this.requestHandler.GetLocalizationValue((int)property.StringValueId, culture.Id) : this.requestHandler.GetLocalizationValue((int)property.StringValueId, neutralCulture.Id);

        serializedProperty.StringValue = stringValue;
      }

      else if (dataType.StorageDataType == StorageDataType.DateTime)
        serializedProperty.DateTimeValue = property.DateTimeValue;

      return serializedProperty;
    }

    private string SerializeObject(object value)
    {
      string result = JsonConvert.SerializeObject(value);

      if (string.IsNullOrEmpty(result))
        return null;

      return result;
    }
  }
}