// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Reflection;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain
{
  public class DomainObjectMapper
  {
    private IRequestHandler handler;

    public DomainObjectMapper(IRequestHandler requestHandler)
    {
      this.handler = handler;
    }

    public void Create<T>(T domainObject) where T : DomainObject
    {
      Class @class = this.handler.Storage.GetRepository<IClassRepository>().WithCode(typeof(T).Name);
      Object @object = new Object();

      @object.ClassId = @class.Id;
      @object.ViewName = domainObject.ViewName;
      @object.Url = domainObject.Url;
      this.handler.Storage.GetRepository<IObjectRepository>().Create(@object);
      this.handler.Storage.Save();
      domainObject.Id = @object.Id;
      this.CreateProperties(@class, @object, domainObject);
      this.CreateRelations(@class, @object, domainObject);
    }

    public T WithKey<T>(int id) where T: DomainObject
    {
      T domainObject = System.Activator.CreateInstance<T>();
      Object @object = this.handler.Storage.GetRepository<IObjectRepository>().WithKey(id);
      Class @class = this.handler.Storage.GetRepository<IClassRepository>().WithKey(@object.ClassId);

      foreach (Member member in this.handler.Storage.GetRepository<IMemberRepository>().FilteredByClassIdInlcudingParent(@class.Id))
        this.ProcessMember(domainObject, @object, member);

      domainObject.Id = @object.Id;
      domainObject.ViewName = @object.ViewName;
      domainObject.Url = @object.Url;
      return domainObject;
    }

    private void CreateProperties<T>(Class @class, Object @object, T domainObject) where T : DomainObject
    {
      foreach (PropertyInfo propertyInfo in typeof(T).GetProperties().Where(pi => pi.PropertyType == typeof(DomainProperty)))
      {
        DomainProperty domainProperty = propertyInfo.GetValue(domainObject) as DomainProperty;

        if (domainProperty != null)
        {
          Member member = this.handler.Storage.GetRepository<IMemberRepository>().WithClassIdAndCode(@class.Id, propertyInfo.Name);

          if (member == null && @class.ClassId != null)
            member = this.handler.Storage.GetRepository<IMemberRepository>().WithClassIdAndCode((int)@class.ClassId, propertyInfo.Name);

          if (member != null)
          {
            foreach (string cultureCode in domainProperty.Keys)
              this.CreateProperty(@object.Id, member.Id, cultureCode, domainProperty[cultureCode]);
          }
        }
      }
    }

    private void CreateProperty(int objectId, int memberId, string cultureCode, string value)
    {
      Dictionary html = new Dictionary();

      this.handler.Storage.GetRepository<IDictionaryRepository>().Create(html);
      this.handler.Storage.Save();

      Property property = new Property();

      property.ObjectId = objectId;
      property.MemberId = memberId;
      property.HtmlId = html.Id;
      this.handler.Storage.GetRepository<IPropertyRepository>().Create(property);
      this.handler.Storage.Save();

      Localization localization = new Localization();

      localization.DictionaryId = property.HtmlId;
      localization.CultureId = this.handler.Storage.GetRepository<ICultureRepository>().WithCode(cultureCode).Id;
      localization.Value = value;
      this.handler.Storage.GetRepository<ILocalizationRepository>().Create(localization);
      this.handler.Storage.Save();
    }

    private void CreateRelations<T>(Class @class, Object @object, T domainObject) where T : DomainObject
    {
      // TODO: implement relations creating
    }

    private void ProcessMember(DomainObject domainObject, Object @object, Member member)
    {
      if (member.PropertyDataTypeId != null)
        this.ProcessProperty(domainObject, @object, member);

      else if (member.RelationClassId != null)
        this.ProcessRelation(domainObject, @object, member);
    }

    private void ProcessProperty(DomainObject domainObject, Object @object, Member member)
    {
      PropertyInfo propertyInfo = domainObject.GetType().GetProperty(member.Code);

      if (propertyInfo == null || propertyInfo.PropertyType != typeof(DomainProperty))
        return;

      DomainProperty domainProperty = this.CreateDomainProperty(
        this.handler.Storage.GetRepository<IPropertyRepository>().WithObjectIdAndMemberId(@object.Id, member.Id)
      );

      propertyInfo.SetValue(domainObject, domainProperty);
    }

    private void ProcessRelation(DomainObject domainObject, Object @object, Member member)
    {
      // TODO: implement relations processing
    }

    private DomainProperty CreateDomainProperty(Property property)
    {
      DomainProperty domainProperty = new DomainProperty();
      
      if (property != null)
      {
        foreach (Culture culture in CultureManager.GetCultures(this.handler.Storage))
        {
          Localization localization = this.handler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId(property.HtmlId, culture.Id);

          domainProperty.Add(culture.Code, localization?.Value);
        }
      }

      return domainProperty;
    }
  }
}