// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
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
        new Inclusion<Class>("Members.PropertyDataType"),
        new Inclusion<Class>("Members.RelationClass"),
        new Inclusion<Class>("Parent.Members.Tab"),
        new Inclusion<Class>("Parent.Members.PropertyDataType"),
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

    private static IDictionary<TabViewModel, IEnumerable<object>> GetMembersByTabs(HttpContext httpContext, Class @class, Object @object = null)
    {
      Dictionary<TabViewModel, IEnumerable<object>> membersByTabs = new Dictionary<TabViewModel, IEnumerable<object>>();
      IStringLocalizer<CreateOrEditViewModel> localizer = httpContext.GetStringLocalizer<CreateOrEditViewModel>();

      membersByTabs.Add(new TabViewModel() { Name = localizer["General"] }, GetMembersByTab(httpContext, @class, null, @object));

      foreach (Tab tab in @class.GetTabs())
        membersByTabs.Add(TabViewModelFactory.Create(tab), GetMembersByTab(httpContext, @class, tab, @object));

      return membersByTabs;
    }

    private static IEnumerable<object> GetMembersByTab(HttpContext httpContext, Class @class, Tab tab, Object @object)
    {
      List<object> members = new List<object>();

      foreach (Member member in @class.GetMembers().Where(m => m.Tab?.Id == tab?.Id))
      {
        if (member.PropertyDataTypeId != null)
        {
          Property property = @object?.Properties.FirstOrDefault(p => p.MemberId == member.Id);

          if (property == null)
            property = new Property() { Member = member };

          members.Add(PropertyViewModelFactory.Create(httpContext, property));
        }

        else if (member.RelationClassId != null && member.IsRelationSingleParent != true)
        {
          IEnumerable<Relation> relations = @object?.ForeignRelations.Where(r => r.MemberId == member.Id).ToList();

          members.Add(RelationSetViewModelFactory.Create(member, relations ?? System.Array.Empty<Relation>()));
        }
      }

      return members;
    }
  }
}