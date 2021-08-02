// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Members
{
  public static class CreateOrEditViewModelMapper
  {
    public static Member Map(MemberFilter filter, Member member, CreateOrEditViewModel createOrEdit)
    {
      if (member.Id == 0)
        member.ClassId = (int)filter.Class.Id;

      member.TabId = createOrEdit.TabId;
      member.Code = createOrEdit.Code;
      member.Name = createOrEdit.Name;
      member.Position = createOrEdit.Position;
      member.PropertyDataTypeId = createOrEdit.PropertyDataTypeId;
      member.IsPropertyLocalizable = createOrEdit.IsPropertyLocalizable ? true : (bool?)null;
      member.IsPropertyVisibleInList = createOrEdit.IsPropertyVisibleInList ? true : (bool?)null;
      member.RelationClassId = createOrEdit.RelationClassId;
      member.IsRelationSingleParent = createOrEdit.IsRelationSingleParent ? true : (bool?)null;
      member.MinRelatedObjectsNumber = createOrEdit.MinRelatedObjectsNumber;
      member.MaxRelatedObjectsNumber = createOrEdit.MaxRelatedObjectsNumber;
      return member;
    }
  }
}