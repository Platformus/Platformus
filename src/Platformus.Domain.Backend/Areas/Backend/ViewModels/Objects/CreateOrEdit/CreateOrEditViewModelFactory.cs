// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Objects
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    private IEnumerable<Member> members;

    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id, int? classId, int? objectId)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          Class = new ClassViewModelFactory(this.RequestHandler).Create(
            this.RequestHandler.Storage.GetRepository<IClassRepository>().WithKey((int)classId)
          ),
          MembersByTabs = this.GetMembersByTabs(null, classId, objectId)
        };

      Object @object = this.RequestHandler.Storage.GetRepository<IObjectRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = @object.Id,
        Class = new ClassViewModelFactory(this.RequestHandler).Create(
          this.RequestHandler.Storage.GetRepository<IClassRepository>().WithKey(@object.ClassId)
        ),
        MembersByTabs = this.GetMembersByTabs(@object)
      };
    }

    private IEnumerable<dynamic> GetMembersByTabs(Object @object, int? classId = null, int? objectId = null)
    {
      List<dynamic> membersByTabs = new List<dynamic>();
      IStringLocalizer<CreateOrEditViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<CreateOrEditViewModelFactory>>();

      membersByTabs.Add(new { id = 0, name = localizer["General"].Value, members = this.GetMembersByTab(null, @object, classId, objectId) });

      foreach (Tab tab in this.RequestHandler.Storage.GetRepository<ITabRepository>().FilteredByClassIdInlcudingParent(@object != null ? @object.ClassId : (int)classId).ToList())
        membersByTabs.Add(new { id = tab.Id, name = tab.Name, members = this.GetMembersByTab(tab, @object, classId, objectId) });

      return membersByTabs;
    }

    private IEnumerable<dynamic> GetMembersByTab(Tab tab, Object @object, int? classId = null, int? objectId = null)
    {
      if (this.members == null)
        this.members = this.RequestHandler.Storage.GetRepository<IMemberRepository>().FilteredByClassIdInlcudingParent(@object != null ? @object.ClassId : (int)classId).ToList();

      List<dynamic> membersByTab = new List<dynamic>();

      foreach (Member member in this.members)
        if ((tab == null && member.TabId == null) || (tab != null && tab.Id == member.TabId))
          membersByTab.Add(
            new
            {
              id = member.Id,
              name = member.Name,
              propertyDataType = member.PropertyDataTypeId == null ? null : new
              {
                javaScriptEditorClassName = this.RequestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId).JavaScriptEditorClassName,
                dataTypeParameters = this.RequestHandler.Storage.GetRepository<IDataTypeParameterRepository>().FilteredByDataTypeId((int)member.PropertyDataTypeId).ToList().Select(
                  dtp => new { code = dtp.Code, value = this.RequestHandler.Storage.GetRepository<IDataTypeParameterValueRepository>().WithDataTypeParameterIdAndMemberId(dtp.Id, member.Id)?.Value }
                )
              },
              isPropertyLocalizable = member.PropertyDataTypeId == null ? null : member.IsPropertyLocalizable,
              property = member.PropertyDataTypeId == null ? null : this.GetProperty(member, @object, objectId),
              relationClass = member.RelationClassId == null ? null : new
              {
                id = member.RelationClassId
              },
              isRelationSingleParent = member.RelationClassId == null ? null : member.IsRelationSingleParent,
              minRelatedObjectsNumber = member.MinRelatedObjectsNumber,
              maxRelatedObjectsNumber = member.MaxRelatedObjectsNumber,
              relations = member.RelationClassId == null ? null : this.GetRelations(member, @object, objectId)
            }
          );

      return membersByTab;
    }

    private dynamic GetProperty(Member member, Object @object, int? objectId)
    {
      Property property = null;

      if (@object != null)
        property = this.RequestHandler.Storage.GetRepository<IPropertyRepository>().WithObjectIdAndMemberId(@object.Id, member.Id);

      if (property == null)
        return new
        {
          stringValue = new
          {
            localizations = this.GetLocalizations(null).Select(
              l => new
              {
                culture = new { code = l.Culture.Code },
                value = string.IsNullOrEmpty(l.Value) ? null : l.Value
              }
            )
          },
        };

      return new
      {
        integerValue = property.IntegerValue,
        decimalValue = property.DecimalValue,
        stringValue = new
        {
          localizations = this.GetLocalizations(property == null ? null : property.StringValueId).Select(
            l => new
            {
              culture = new { code = l.Culture.Code },
              value = string.IsNullOrEmpty(l.Value) ? null : l.Value
            }
          )
        },
        dateTimeValue = property.DateTimeValue
      };
    }

    private IEnumerable<dynamic> GetRelations(Member member, Object @object, int? objectId)
    {
      if (@object == null && objectId == null)
        return new dynamic[] { };

      if (@object == null && objectId != null)
        return new dynamic[] { new { primaryId = objectId } };

      return this.RequestHandler.Storage.GetRepository<IRelationRepository>().FilteredByMemberIdAndForeignId(member.Id, @object.Id).Select(
        r => new { primaryId = r.PrimaryId }
      );
    }
  }
}