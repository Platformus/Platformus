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
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(int userId, string orderBy, string direction, int skip, int take, string filter)
    {
      ICredentialRepository credentialRepository = this.RequestHandler.Storage.GetRepository<ICredentialRepository>();

      return new IndexViewModel()
      {
        UserId = userId,
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, credentialRepository.CountByUserId(userId, filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create("Credential Type"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Identifier", "Identifier"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          credentialRepository.FilteredByUserIdRange(userId, orderBy, direction, skip, take, filter).Select(c => new CredentialViewModelFactory(this.RequestHandler).Create(c)),
          "_Credential"
        )
      };
    }
  }
}