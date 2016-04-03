// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Content
{
  public class CacheManager
  {
    public IHandler handler;

    public CacheManager(IHandler handler)
    {
      this.handler = handler;
    }

    public void CacheObject(Object @object)
    {
      foreach (Culture culture in this.handler.Storage.GetRepository<ICultureRepository>().All())
      {
        CachedObject cachedObject = this.handler.Storage.GetRepository<ICachedObjectRepository>().WithKey(culture.Id, @object.Id);

        if (cachedObject == null)
          this.handler.Storage.GetRepository<ICachedObjectRepository>().Create(this.CacheObject(culture, @object));

        else
        {
          CachedObject temp = this.CacheObject(culture, @object);

          cachedObject.ClassId = temp.ClassId;
          cachedObject.ViewName = temp.ViewName;
          cachedObject.Url = temp.Url;
          cachedObject.CachedProperties = temp.CachedProperties;
          cachedObject.CachedDataSources = temp.CachedDataSources;
          this.handler.Storage.GetRepository<ICachedObjectRepository>().Edit(cachedObject);
        }
      }

      this.handler.Storage.Save();
    }

    private CachedObject CacheObject(Culture culture, Object @object)
    {
      Class @class = this.handler.Storage.GetRepository<IClassRepository>().WithKey(@object.ClassId);
      List<CachedProperty> cachedProperties = new List<CachedProperty>();

      foreach (Member member in this.handler.Storage.GetRepository<IMemberRepository>().FilteredByClassId(@class.Id))
      {
        if (member.PropertyDataTypeId != null)
        {
          Property property = this.handler.Storage.GetRepository<IPropertyRepository>().WithObjectIdAndMemberId(@object.Id, member.Id);

          cachedProperties.Add(this.CacheProperty(culture, property));
        }
      }

      List<CachedDataSource> cachedDataSources = new List<CachedDataSource>();

      foreach (DataSource dataSource in this.handler.Storage.GetRepository<IDataSourceRepository>().FilteredByClassId(@class.Id))
        cachedDataSources.Add(this.CacheDataSource(culture, dataSource));

      CachedObject cachedObject = new CachedObject();

      cachedObject.ObjectId = @object.Id;
      cachedObject.ClassId = @class.Id;
      cachedObject.ViewName = string.IsNullOrEmpty(@object.ViewName) ? @class.DefaultViewName : @object.ViewName;
      cachedObject.Url = @object.Url;
      cachedObject.CultureId = culture.Id;

      if (cachedProperties.Count != 0)
        cachedObject.CachedProperties = this.SerializeObject(cachedProperties);

      if (cachedDataSources.Count != 0)
        cachedObject.CachedDataSources = this.SerializeObject(cachedDataSources);

      return cachedObject;
    }

    private CachedProperty CacheProperty(Culture culture, Property property)
    {
      CachedProperty cachedProperty = new CachedProperty();

      cachedProperty.PropertyId = property.Id;
      cachedProperty.MemberCode = this.handler.Storage.GetRepository<IMemberRepository>().WithKey(property.MemberId).Code;
      cachedProperty.Html = this.GetLocalizationValue(culture.Id, property.HtmlId);
      return cachedProperty;
    }

    private CachedDataSource CacheDataSource(Culture culture, DataSource dataSource)
    {
      CachedDataSource cachedDataSource = new CachedDataSource();

      cachedDataSource.DataSourceId = dataSource.Id;
      cachedDataSource.CSharpClassName = dataSource.CSharpClassName;
      cachedDataSource.Parameters = dataSource.Parameters;
      cachedDataSource.Code = dataSource.Code;
      return cachedDataSource;
    }

    private string GetLocalizationValue(int cultureId, int dictionaryId)
    {
      Localization localization = this.handler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId(dictionaryId, cultureId);

      if (localization == null)
        return null;

      return localization.Value;
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