// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
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
  public class ObjectDirector
  {
    private IRequestHandler requestHandler;
    private IClassRepository classRepository;
    private IMemberRepository memberRepository;
    private IPropertyRepository propertyRepository;
    private ILocalizationRepository localizationRepository;

    public ObjectDirector(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
      this.classRepository = this.requestHandler.Storage.GetRepository<IClassRepository>();
      this.memberRepository = this.requestHandler.Storage.GetRepository<IMemberRepository>();
      this.propertyRepository = this.requestHandler.Storage.GetRepository<IPropertyRepository>();
      this.localizationRepository = this.requestHandler.Storage.GetRepository<ILocalizationRepository>();
    }

    public void ConstructObject(ObjectBuilderBase objectBuilder, Object @object)
    {
      objectBuilder.BuildBasics(@object);

      Class @class = this.classRepository.WithKey(@object.ClassId);

      foreach (Member member in this.memberRepository.FilteredByClassIdInlcudingParent(@class.Id))
      {
        if (member.PropertyDataTypeId != null)
          this.ConstructProperty(objectBuilder, @object, member);

        else if (member.RelationClassId != null)
          this.ConstructRelation(objectBuilder, @object, member);
      }
    }

    private void ConstructProperty(ObjectBuilderBase objectBuilder, Object @object, Member member)
    {
      Property property = this.propertyRepository.WithObjectIdAndMemberId(@object.Id, member.Id);
      IDictionary<Culture, Localization> localizationsByCultures = new Dictionary<Culture, Localization>();

      foreach (Culture culture in CultureManager.GetCultures(this.requestHandler.Storage))
        localizationsByCultures.Add(culture, this.localizationRepository.WithDictionaryIdAndCultureId(property.HtmlId, culture.Id));

      objectBuilder.BuildProperty(@object, member, property, localizationsByCultures);
    }

    private void ConstructRelation(ObjectBuilderBase objectBuilder, Object @object, Member member)
    {
      // TODO: implement relations processing
    }
  }
}