// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Members
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Member Map(CreateOrEditViewModel createOrEdit)
    {
      Member member = new Member();

      if (createOrEdit.Id != null)
        member = this.RequestHandler.Storage.GetRepository<IMemberRepository>().WithKey((int)createOrEdit.Id);

      else member.ClassId = createOrEdit.ClassId;

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