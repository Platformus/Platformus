// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class CredentialViewModelFactory : ViewModelFactoryBase
  {
    public CredentialViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CredentialViewModel Create(Credential credential)
    {
      return new CredentialViewModel()
      {
        Id = credential.Id,
        CredentialType = new CredentialTypeViewModelFactory(this.RequestHandler).Create(
          this.RequestHandler.Storage.GetRepository<ICredentialTypeRepository>().WithKey(credential.CredentialTypeId)
        ),
        Identifier = credential.Identifier
      };
    }
  }
}