// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class MemberViewModelFactory : ViewModelFactoryBase
  {
    public MemberViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public MemberViewModel Create(Member member)
    {
      return new MemberViewModel()
      {
        Id = member.Id,
        Name = member.Name,
        Position = member.Position,
        PropertyDataType = member.PropertyDataTypeId == null ? null : new DataTypeViewModelFactory(this.RequestHandler).Create(
          this.RequestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId)
        ),
        RelationClass = member.RelationClassId == null ? null : new ClassViewModelFactory(this.RequestHandler).Create(
          this.RequestHandler.Storage.GetRepository<IClassRepository>().WithKey((int)member.RelationClassId)
        )
      };
    }
  }
}