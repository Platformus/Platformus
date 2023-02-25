// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared;

public static class ObjectViewModelFactory
{
  public static ObjectViewModel Create(HttpContext httpContext, Object @object)
  {
    List<PropertyViewModel> properties = new List<PropertyViewModel>();

    foreach (Member member in @object.Class.GetVisibleInListMembers())
    {
      Property property = @object.Properties.FirstOrDefault(p => p.MemberId == member.Id) ?? new Property();

      property.Member = member;
      properties.Add(PropertyViewModelFactory.Create(httpContext, property));
    }

    return new ObjectViewModel()
    {
      Id = @object.Id,
      Properties = properties,
      RelationSingleParentMembers = @object
        .Class
        .GetRelationSingleParentMembers()
        .Select(m => MemberViewModelFactory.Create(m))
        .ToList()
    };
  }
}