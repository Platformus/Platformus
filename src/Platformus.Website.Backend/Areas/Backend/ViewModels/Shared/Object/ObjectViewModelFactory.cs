// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public static class ObjectViewModelFactory
  {
    public static ObjectViewModel Create(Object @object, IEnumerable<Member> members)
    {
      Dictionary<MemberViewModel, object> propertiesByMembers = new Dictionary<MemberViewModel, object>();
      
      foreach (Member member in members)
      {
        MemberViewModel memberViewModel = MemberViewModelFactory.Create(member);

        if (member.PropertyDataType != null)
        {
          Property property = @object.Properties.FirstOrDefault(p => p.MemberId == member.Id);

          propertiesByMembers.Add(memberViewModel, property?.GetValue());
        }

        else if (member.RelationClass != null)
          propertiesByMembers.Add(memberViewModel, null);
      }

      return new ObjectViewModel()
      {
        Id = @object.Id,
        PropertiesByMembers = propertiesByMembers
      };
    }
  }
}