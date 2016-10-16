// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Security.Backend.ViewModels.Shared;
using Platformus.Security.Data.Abstractions;

namespace Platformus.Security.Backend.ViewModels.Permissions
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take)
    {
      IPermissionRepository permissionRepository = this.handler.Storage.GetRepository<IPermissionRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.handler).Create(
          orderBy, direction, skip, take, permissionRepository.Count(),
          new[] {
            new GridColumnViewModelFactory(this.handler).Create("Name", "Name"),
            new GridColumnViewModelFactory(this.handler).Create("Position", "Position"),
            new GridColumnViewModelFactory(this.handler).CreateEmpty()
          },
          permissionRepository.Range(orderBy, direction, skip, take).Select(p => new PermissionViewModelFactory(this.handler).Create(p)),
          "_Permission"
        )
      };
    }
  }
}