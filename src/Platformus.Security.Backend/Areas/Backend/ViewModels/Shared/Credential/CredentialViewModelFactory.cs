// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class CredentialViewModelFactory : ViewModelFactoryBase
  {
    public CredentialViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public CredentialViewModel Create(Credential credential)
    {
      return new CredentialViewModel()
      {
        Id = credential.Id,
        CredentialType = new CredentialTypeViewModelFactory(this.handler).Create(
          this.handler.Storage.GetRepository<ICredentialTypeRepository>().WithKey(credential.CredentialTypeId)
        ),
        Identifier = credential.Identifier
      };
    }
  }
}