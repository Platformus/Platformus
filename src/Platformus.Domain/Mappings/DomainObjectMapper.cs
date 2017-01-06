// Copyright © 2016 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain
{
  public class DomainObjectMapper
  {
    private IRequestHandler requestHandler;
    private IClassRepository classRepository;
    private IMemberRepository memberRepositor;
    private IObjectRepository objectRepository;
    private IPropertyRepository propertyRepository;
    private IDictionaryRepository dictionaryRepository;
    private ICultureRepository cultureRepository;
    private ILocalizationRepository localizationRepository;

    public DomainObjectMapper(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
      this.classRepository = this.requestHandler.Storage.GetRepository<IClassRepository>();
      this.memberRepositor = this.requestHandler.Storage.GetRepository<IMemberRepository>();
      this.objectRepository = this.requestHandler.Storage.GetRepository<IObjectRepository>();
      this.propertyRepository = this.requestHandler.Storage.GetRepository<IPropertyRepository>();
      this.dictionaryRepository = this.requestHandler.Storage.GetRepository<IDictionaryRepository>();
      this.cultureRepository = this.requestHandler.Storage.GetRepository<ICultureRepository>();
      this.localizationRepository = this.requestHandler.Storage.GetRepository<ILocalizationRepository>();
    }

    public T WithKey<T>(int id) where T : DomainObject
    {
      Object @object = this.objectRepository.WithKey(id);
      DomainObjectBuilder<T> objectBuilder = new DomainObjectBuilder<T>();

      new ObjectDirector(this.requestHandler).ConstructObject(objectBuilder, @object);
      return objectBuilder.Build();
    }

    public IEnumerable<T> All<T>() where T : DomainObject
    {
      Class @class = this.classRepository.WithCode(typeof(T).Name);
      IEnumerable<Object> objects = this.objectRepository.FilteredByClassId(@class.Id);
      ObjectDirector objectDirector = new ObjectDirector(this.requestHandler);

      return objects.Select(
        o => {
          DomainObjectBuilder<T> objectBuilder = new DomainObjectBuilder<T>();

          objectDirector.ConstructObject(objectBuilder, o);
          return objectBuilder.Build();
        }
      );
    }

    public void Create<T>(T domainObject) where T : DomainObject
    {
      Class @class = this.classRepository.WithCode(typeof(T).Name);
      Object @object = new Object();

      @object.ClassId = @class.Id;
      @object.ViewName = domainObject.ViewName;
      @object.Url = domainObject.Url;
      this.objectRepository.Create(@object);
      this.requestHandler.Storage.Save();
      domainObject.Id = @object.Id;
      this.CreateProperties(@class, @object, domainObject);
      this.CreateRelations(@class, @object, domainObject);
    }

    private void CreateProperties<T>(Class @class, Object @object, T domainObject) where T : DomainObject
    {
      foreach (PropertyInfo propertyInfo in typeof(T).GetProperties().Where(pi => pi.PropertyType == typeof(DomainProperty)))
      {
        DomainProperty domainProperty = propertyInfo.GetValue(domainObject) as DomainProperty;

        if (domainProperty != null)
        {
          Member member = this.memberRepositor.WithClassIdAndCode(@class.Id, propertyInfo.Name);

          if (member == null && @class.ClassId != null)
            member = this.memberRepositor.WithClassIdAndCode((int)@class.ClassId, propertyInfo.Name);

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

      this.dictionaryRepository.Create(html);
      this.requestHandler.Storage.Save();

      Property property = new Property();

      property.ObjectId = objectId;
      property.MemberId = memberId;
      property.HtmlId = html.Id;
      this.propertyRepository.Create(property);
      this.requestHandler.Storage.Save();

      Localization localization = new Localization();

      localization.DictionaryId = property.HtmlId;
      localization.CultureId = this.cultureRepository.WithCode(cultureCode).Id;
      localization.Value = value;
      this.localizationRepository.Create(localization);
      this.requestHandler.Storage.Save();
    }

    private void CreateRelations<T>(Class @class, Object @object, T domainObject) where T : DomainObject
    {
      // TODO: implement relations creating
    }
  }
}