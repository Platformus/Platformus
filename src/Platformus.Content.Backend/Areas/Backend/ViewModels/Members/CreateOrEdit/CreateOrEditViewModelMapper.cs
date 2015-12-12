// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Members
{
  public class CreateOrEditViewModelMapper : ViewModelBuilderBase
  {
    public CreateOrEditViewModelMapper(IHandler handler)
      : base(handler)
    {
    }

    public Member Map(CreateOrEditViewModel createOrEdit)
    {
      Member member = new Member();

      if (createOrEdit.Id != null)
        member = this.handler.Storage.GetRepository<IMemberRepository>().WithKey((int)createOrEdit.Id);

      else member.ClassId = createOrEdit.ClassId;

      member.TabId = createOrEdit.TabId;
      member.Code = createOrEdit.Code;
      member.Name = createOrEdit.Name;
      member.DisplayInList = createOrEdit.DisplayInList ? true : (bool?)null;
      member.Position = createOrEdit.Position;
      member.RelationClassId = createOrEdit.RelationClassId;
      member.IsRelationSingleParent = createOrEdit.IsRelationSingleParent ? true : (bool?)null;
      member.PropertyDataTypeId = createOrEdit.PropertyDataTypeId;
      return member;
    }
  }
}