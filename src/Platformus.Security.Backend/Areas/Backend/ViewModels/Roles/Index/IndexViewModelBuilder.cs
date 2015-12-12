// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Security.Backend.ViewModels.Shared;
using Platformus.Security.Data.Abstractions;

namespace Platformus.Security.Backend.ViewModels.Roles
{
  public class IndexViewModelBuilder : ViewModelBuilderBase
  {
    public IndexViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Build(string orderBy, string direction, int skip, int take)
    {
      IRoleRepository roleRepository = this.handler.Storage.GetRepository<IRoleRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelBuilder(this.handler).Build(
          orderBy, direction, skip, take, roleRepository.Count(),
          new[] {
            new GridColumnViewModelBuilder(this.handler).Build("Name", "Name"),
            new GridColumnViewModelBuilder(this.handler).Build("Position", "Position"),
            new GridColumnViewModelBuilder(this.handler).BuildEmpty()
          },
          roleRepository.Range(orderBy, direction, skip, take).Select(r => new RoleViewModelBuilder(this.handler).Build(r)),
          "_Role"
        )
      };
    }
  }
}