// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Security.Backend.ViewModels.Shared;
using Platformus.Security.Data.Abstractions;

namespace Platformus.Security.Backend.ViewModels.Users
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take)
    {
      IUserRepository userRepository = this.RequestHandler.Storage.GetRepository<IUserRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, userRepository.Count(),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create("Name", "Name"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Credentials"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Created", "Created"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          userRepository.Range(orderBy, direction, skip, take).Select(u => new UserViewModelFactory(this.RequestHandler).Create(u)),
          "_User"
        )
      };
    }
  }
}