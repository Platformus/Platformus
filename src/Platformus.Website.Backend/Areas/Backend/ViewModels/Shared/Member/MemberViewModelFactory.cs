// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public static class MemberViewModelFactory
  {
    public static MemberViewModel Create(Member member)
    {
      return new MemberViewModel()
      {
        Id = member.Id,
        Name = member.Name,
        Position = member.Position,
        PropertyDataType = member.PropertyDataType == null ? null : DataTypeViewModelFactory.Create(member.PropertyDataType),
        RelationClass = member.RelationClass == null ? null : ClassViewModelFactory.Create(member.RelationClass)
      };
    }
  }
}