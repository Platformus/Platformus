// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Members
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(int classId, string orderBy, string direction, int skip, int take, string filter)
    {
      IMemberRepository memberRepository = this.RequestHandler.Storage.GetRepository<IMemberRepository>();

      return new IndexViewModel()
      {
        ClassId = classId,
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, memberRepository.CountByClassId(classId, filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create("Name", "Name"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Property Data Type"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Relation Class"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Position", "Position"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          memberRepository.FilteredByClassIdRange(classId, orderBy, direction, skip, take, filter).Select(m => new MemberViewModelFactory(this.RequestHandler).Create(m, null)),
          "_Member"
        )
      };
    }
  }
}