// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Extensions;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Objects
{
  public static class CreateOrEditViewModelFactory
  {
    public static async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, ObjectFilter filter, Object @object)
    {
      Class @class = await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetByIdAsync(
        (int)filter.Class.Id,
        new Inclusion<Class>(c => c.Tabs),
        new Inclusion<Class>(c => c.Parent.Tabs),
        new Inclusion<Class>("Members.Tab"),
        new Inclusion<Class>("Members.PropertyDataType.DataTypeParameters.DataTypeParameterValues"),
        new Inclusion<Class>("Members.RelationClass"),
        new Inclusion<Class>("Parent.Members.Tab"),
        new Inclusion<Class>("Parent.Members.PropertyDataType.DataTypeParameters.DataTypeParameterValues"),
        new Inclusion<Class>("Parent.Members.RelationClass")
      );

      if (@object == null)
        return new CreateOrEditViewModel()
        {
          Class = ClassViewModelFactory.Create(@class),
          MembersByTabs = GetMembersByTabs(httpContext, @class)
        };

      return new CreateOrEditViewModel()
      {
        Id = @object.Id,
        Class = ClassViewModelFactory.Create(@class),
        MembersByTabs = GetMembersByTabs(httpContext, @class, @object)
      };
    }

    private static List<dynamic> GetMembersByTabs(HttpContext httpContext, Class @class, Object @object = null)
    {
      List<dynamic> membersByTabs = new List<dynamic>();
      IStringLocalizer<CreateOrEditViewModel> localizer = httpContext.GetStringLocalizer<CreateOrEditViewModel>();

      membersByTabs.Add(new { id = 0, name = localizer["General"].Value, members = GetMembersByTab(httpContext, @class, null, @object) });

      foreach (Tab tab in @class.GetTabs())
        membersByTabs.Add(new { id = tab.Id, name = tab.Name, members = GetMembersByTab(httpContext, @class, tab, @object) });

      return membersByTabs;
    }

    private static dynamic GetMembersByTab(HttpContext httpContext, Class @class, Tab tab, Object @object)
    {
      return @class.GetMembers().Where(m => m.Tab?.Id == tab?.Id).Select(
        m => new
        {
          id = m.Id,
          name = m.Name,
          propertyDataType = m.PropertyDataType == null ? null : new
          {
            javaScriptEditorClassName = m.PropertyDataType.JavaScriptEditorClassName,
            dataTypeParameters = m.PropertyDataType.DataTypeParameters.Select(
              dtp => new { code = dtp.Code, value = dtp.DataTypeParameterValues.FirstOrDefault(dtpv => dtpv.MemberId == m.Id)?.Value }
            )
          },
          isPropertyLocalizable = m.IsPropertyLocalizable,
          property = m.PropertyDataType == null ? null : GetProperty(httpContext, m, @object),
          relationClass = m.RelationClass == null ? null : new
          {
            id = m.RelationClass.Id
          },
          isRelationSingleParent = m.IsRelationSingleParent,
          minRelatedObjectsNumber = m.MinRelatedObjectsNumber,
          maxRelatedObjectsNumber = m.MaxRelatedObjectsNumber,
          relations = m.RelationClass == null ? null : GetRelations(m, @object)
        }
      );
    }

    private static dynamic GetProperty(HttpContext httpContext, Member member, Object @object)
    {
      Property property = @object?.Properties.FirstOrDefault(p => p.MemberId == member.Id);

      return new
      {
        integerValue = property?.IntegerValue,
        decimalValue = property?.DecimalValue,
        stringValue = new
        {
          localizations = httpContext.GetCultureManager().GetCulturesAsync().Result.Select(
            c => new
            {
              culture = new { id = c.Id },
              value = property?.StringValue == null ? null : property.StringValue.Localizations.FirstOrDefault(l => l.CultureId == c.Id)?.Value
            }
          )
        },
        dateTimeValue = property?.DateTimeValue
      };
    }

    private static IEnumerable<object> GetRelations(Member m, Object @object)
    {
      if (@object == null)
        return new object[] { };

      return @object.ForeignRelations
        .Where(r => r.MemberId == m.Id)
        .Select(r => new { primaryId = r.PrimaryId });
    }
  }
}