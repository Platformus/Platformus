// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Domain
{
  public class ObjectManipulator
  {
    private string typeName;
    private int objectId;
    private Class @class;
    private IRequestHandler requestHandler;
    private IClassRepository classRepository;
    private IDataTypeRepository dataTypeRepository;
    private IMemberRepository memberRepository;
    private IObjectRepository objectRepository;
    private IPropertyRepository propertyRepository;
    private IDictionaryRepository dictionaryRepository;
    private ILocalizationRepository localizationRepository;

    public ObjectManipulator(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
      this.classRepository = this.requestHandler.Storage.GetRepository<IClassRepository>();
      this.dataTypeRepository = this.requestHandler.Storage.GetRepository<IDataTypeRepository>();
      this.memberRepository = this.requestHandler.Storage.GetRepository<IMemberRepository>();
      this.objectRepository = this.requestHandler.Storage.GetRepository<IObjectRepository>();
      this.propertyRepository = this.requestHandler.Storage.GetRepository<IPropertyRepository>();
      this.dictionaryRepository = this.requestHandler.Storage.GetRepository<IDictionaryRepository>();
      this.localizationRepository = this.requestHandler.Storage.GetRepository<ILocalizationRepository>();
    }

    public void BeginCreateTransaction(string typeName)
    {
      this.typeName = typeName;
      this.objectId = this.CreateObject().Id;
    }

    public void BeginCreateTransaction<T>()
    {
      this.BeginCreateTransaction(typeof(T).Name);
    }

    public void BeginEditTransaction(string typeName, int objectId)
    {
      this.typeName = typeName;
      this.objectId = objectId;
    }

    public void BeginEditTransaction<T>(int objectId)
    {
      this.BeginEditTransaction(typeof(T).Name, objectId);
    }

    public void SetPropertyValue(string code, object value)
    {
      Member member = this.GetMember(code);

      if (member == null)
        throw new System.ArgumentException("Member code is not valid.");

      Property property = this.GetProperty(member.Id);

      if (property == null)
      {
        property = new Property();
        property.ObjectId = this.objectId;
        property.MemberId = member.Id;
        this.AssignValueToProperty(member, property, value);
        this.propertyRepository.Create(property);
      }

      else
      {
        this.AssignValueToProperty(member, property, value);
        this.propertyRepository.Edit(property);
      }

      this.requestHandler.Storage.Save();
    }

    public int CommitTransaction()
    {
      // TODO: add transactions support to ExtCore
      return this.objectId;
    }

    public void RollbackTransaction()
    {
      // TODO: add transactions support to ExtCore
    }

    private Object CreateObject()
    {
      Object @object = new Object();

      @object.ClassId = this.GetClass().Id;
      this.objectRepository.Create(@object);
      this.requestHandler.Storage.Save();
      return @object;
    }

    private void AssignValueToProperty(Member member, Property property, object value)
    {
      try
      {
        DataType dataType = this.dataTypeRepository.WithKey((int)member.PropertyDataTypeId);

        if (dataType.StorageDataType == StorageDataType.Integer)
          property.IntegerValue = value is JValue ? (value as JValue).Value<int>() : (int)value;

        else if (dataType.StorageDataType == StorageDataType.Decimal)
          property.DecimalValue = value is JValue ? (value as JValue).Value<decimal>() : (decimal)value;

        else if (dataType.StorageDataType == StorageDataType.String)
        {
          if (property.StringValueId != null)
            this.DeleteLocalizations((int)property.StringValueId);

          if (property.StringValueId == null)
            property.StringValueId = this.CreateDictionary().Id;

          if (value is string)
            this.CreateLocalizations((int)property.StringValueId, value as string);

          else this.CreateLocalizations((int)property.StringValueId, value is JArray ? this.GetValuesByCultureCodes(value as JArray) : value as IDictionary<string, string>);
        }

        else if (dataType.StorageDataType == StorageDataType.DateTime)
          property.DateTimeValue = value is JValue ? (value as JValue).Value<System.DateTime>() : (System.DateTime)value;
      }

      catch (System.Exception e)
      {
        throw new System.ArgumentException(string.Format("Can't assign value {0} to the property with member code {1}", value?.ToString(), member.Code));
      }
    }

    private void DeleteLocalizations(int dictionaryId)
    {
      foreach (Localization localization in this.localizationRepository.FilteredByDictionaryId(dictionaryId))
        this.localizationRepository.Delete(localization);

      this.requestHandler.Storage.Save();
    }

    private Dictionary CreateDictionary()
    {
      Dictionary dictionary = new Dictionary();

      this.dictionaryRepository.Create(dictionary);
      this.requestHandler.Storage.Save();
      return dictionary;
    }

    private void CreateLocalizations(int dictionaryId, string value)
    {
      Culture neutralCulture = this.requestHandler.GetService<ICultureManager>().GetNeutralCulture();
      Localization localization = new Localization();

      localization.DictionaryId = dictionaryId;
      localization.CultureId = neutralCulture.Id;
      localization.Value = value;
      this.requestHandler.Storage.GetRepository<ILocalizationRepository>().Create(localization);
      this.requestHandler.Storage.Save();
    }

    private void CreateLocalizations(int dictionaryId, IDictionary<string, string> valuesByCultureCodes)
    {
      foreach (Culture culture in this.requestHandler.GetService<ICultureManager>().GetNotNeutralCultures())
      {
        Localization localization = new Localization();

        localization.DictionaryId = dictionaryId;
        localization.CultureId = culture.Id;
        localization.Value = valuesByCultureCodes[culture.Code];
        this.requestHandler.Storage.GetRepository<ILocalizationRepository>().Create(localization);
      }

      this.requestHandler.Storage.Save();
    }

    private Class GetClass()
    {
      if (this.@class == null)
        this.@class = this.classRepository.WithCode(this.typeName);

      return this.@class;
    }

    private Member GetMember(string code)
    {
      return this.memberRepository.WithClassIdAndCodeInlcudingParent(this.GetClass().Id, code);
    }

    private Property GetProperty(int memberId)
    {
      return this.propertyRepository.WithObjectIdAndMemberId(this.objectId, memberId);
    }

    private IDictionary<string, string> GetValuesByCultureCodes(JArray array)
    {
      IDictionary<string, string> valuesByCultureCodes = new Dictionary<string, string>();

      foreach (JObject item in array)
        valuesByCultureCodes.Add(item.Property("cultureCode").Value.Value<string>(), item.Property("value").Value.Value<string>());

      return valuesByCultureCodes;
    }
  }
}