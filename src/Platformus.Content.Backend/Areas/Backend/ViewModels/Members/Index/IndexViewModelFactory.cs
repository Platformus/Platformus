// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Content.Backend.ViewModels.Shared;
using Platformus.Content.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Members
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Create(int classId, string orderBy, string direction, int skip, int take)
    {
      IMemberRepository memberRepository = this.handler.Storage.GetRepository<IMemberRepository>();

      return new IndexViewModel()
      {
        ClassId = classId,
        Grid = new GridViewModelFactory(this.handler).Create(
          orderBy, direction, skip, take, memberRepository.CountByClassId(classId),
          new[] {
            new GridColumnViewModelFactory(this.handler).Create("Name", "Name"),
            new GridColumnViewModelFactory(this.handler).Create("Property Data Type"),
            new GridColumnViewModelFactory(this.handler).Create("Relation Class"),
            new GridColumnViewModelFactory(this.handler).Create("Position", "Position"),
            new GridColumnViewModelFactory(this.handler).CreateEmpty()
          },
          memberRepository.FilteredByClassRange(classId, orderBy, direction, skip, take).Select(m => new MemberViewModelFactory(this.handler).Create(m, null)),
          "_Member"
        )
      };
    }
  }
}