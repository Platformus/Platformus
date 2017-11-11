// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Domain
{
  public class ObjectDirector
  {
    private IRequestHandler requestHandler;
    private IClassRepository classRepository;
    private IMemberRepository memberRepository;
    private IDataTypeRepository dataTypeRepository;
    private IPropertyRepository propertyRepository;
    private ILocalizationRepository localizationRepository;

    public ObjectDirector(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
      this.classRepository = this.requestHandler.Storage.GetRepository<IClassRepository>();
      this.memberRepository = this.requestHandler.Storage.GetRepository<IMemberRepository>();
      this.dataTypeRepository = this.requestHandler.Storage.GetRepository<IDataTypeRepository>();
      this.propertyRepository = this.requestHandler.Storage.GetRepository<IPropertyRepository>();
      this.localizationRepository = this.requestHandler.Storage.GetRepository<ILocalizationRepository>();
    }

    public void ConstructObject(ObjectBuilderBase objectBuilder, Object @object)
    {
      objectBuilder.BuildId(@object.Id);

      Class @class = this.classRepository.WithKey(@object.ClassId);

      foreach (Member member in this.memberRepository.FilteredByClassIdInlcudingParent(@class.Id))
      {
        if (member.PropertyDataTypeId != null)
        {
          DataType dataType = this.dataTypeRepository.WithKey((int)member.PropertyDataTypeId);

          this.ConstructProperty(objectBuilder, @object, member, dataType);
        }

        else if (member.RelationClassId != null)
          this.ConstructRelation(objectBuilder, @object, member);
      }
    }

    public void ConstructObject(ObjectBuilderBase objectBuilder, SerializedObject serializedObject)
    {
      foreach (SerializedProperty serializedProperty in JsonConvert.DeserializeObject<IEnumerable<SerializedProperty>>(serializedObject.SerializedProperties))
      {
        if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.Integer)
          objectBuilder.BuildIntegerProperty(serializedProperty.Member.Code, serializedProperty.IntegerValue);

        else if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.Decimal)
          objectBuilder.BuildDecimalProperty(serializedProperty.Member.Code, serializedProperty.DecimalValue);

        if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.String)
        {
          Member member = this.memberRepository.WithClassIdAndCodeInlcudingParent(serializedObject.ClassId, serializedProperty.Member.Code);
          Property property = this.propertyRepository.WithObjectIdAndMemberId(serializedObject.ObjectId, member.Id);

          if (member.IsPropertyLocalizable == true)
            objectBuilder.BuildStringProperty(serializedProperty.Member.Code, this.GetLocalizationValuesByCultureCodes(property));

          else objectBuilder.BuildStringProperty(serializedProperty.Member.Code, this.GetLocalizationValue(property));
        }

        if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.DateTime)
          objectBuilder.BuildDateTimeProperty(serializedProperty.Member.Code, serializedProperty.DateTimeValue);
      }
    }

    private void ConstructProperty(ObjectBuilderBase objectBuilder, Object @object, Member member, DataType dataType)
    {
      Property property = this.propertyRepository.WithObjectIdAndMemberId(@object.Id, member.Id);

      if (dataType.StorageDataType == StorageDataType.Integer)
        objectBuilder.BuildIntegerProperty(member.Code, property.IntegerValue);

      else if (dataType.StorageDataType == StorageDataType.Decimal)
        objectBuilder.BuildDecimalProperty(member.Code, property.DecimalValue);

      if (dataType.StorageDataType == StorageDataType.String)
      {
        if (member.IsPropertyLocalizable == true)
          objectBuilder.BuildStringProperty(member.Code, this.GetLocalizationValuesByCultureCodes(property));

        else objectBuilder.BuildStringProperty(member.Code, this.GetLocalizationValue(property));
      }

      if (dataType.StorageDataType == StorageDataType.DateTime)
        objectBuilder.BuildDateTimeProperty(member.Code, property.DateTimeValue);
    }

    private void ConstructRelation(ObjectBuilderBase objectBuilder, Object @object, Member member)
    {
      // TODO: implement relations processing
    }

    private string GetLocalizationValue(Property property)
    {
      Culture neutralCulture = this.requestHandler.GetService<ICultureManager>().GetNeutralCulture();

      return this.localizationRepository.WithDictionaryIdAndCultureId((int)property.StringValueId, neutralCulture.Id)?.Value;
    }

    private IDictionary<string, string> GetLocalizationValuesByCultureCodes(Property property)
    {
      Dictionary<string, string> localizationValuesByCultureCodes = new Dictionary<string, string>();

      foreach (Culture culture in this.requestHandler.GetService<ICultureManager>().GetNotNeutralCultures())
        localizationValuesByCultureCodes.Add(
          culture.Code,
          this.localizationRepository.WithDictionaryIdAndCultureId((int)property.StringValueId, culture.Id)?.Value
        );

      return localizationValuesByCultureCodes;
    }
  }
}