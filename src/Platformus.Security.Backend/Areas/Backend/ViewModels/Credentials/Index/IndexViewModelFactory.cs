// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Security.Backend.ViewModels.Shared;
using Platformus.Security.Data.Abstractions;

namespace Platformus.Security.Backend.ViewModels.Credentials
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Create(int userId, string orderBy, string direction, int skip, int take)
    {
      ICredentialRepository credentialRepository = this.handler.Storage.GetRepository<ICredentialRepository>();

      return new IndexViewModel()
      {
        UserId = userId,
        Grid = new GridViewModelFactory(this.handler).Create(
          orderBy, direction, skip, take, credentialRepository.CountByUserId(userId),
          new[] {
            new GridColumnViewModelFactory(this.handler).Create("Credential Type"),
            new GridColumnViewModelFactory(this.handler).Create("Identifier", "Identifier"),
            new GridColumnViewModelFactory(this.handler).BuildEmpty()
          },
          credentialRepository.Range(userId, orderBy, direction, skip, take).Select(c => new CredentialViewModelFactory(this.handler).Create(c)),
          "_Credential"
        )
      };
    }
  }
}