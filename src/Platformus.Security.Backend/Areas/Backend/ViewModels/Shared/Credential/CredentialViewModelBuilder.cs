// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class CredentialViewModelBuilder : ViewModelBuilderBase
  {
    public CredentialViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public CredentialViewModel Build(Credential credential)
    {
      return new CredentialViewModel()
      {
        Id = credential.Id,
        CredentialType = new CredentialTypeViewModelBuilder(this.handler).Build(
          this.handler.Storage.GetRepository<ICredentialTypeRepository>().WithKey(credential.CredentialTypeId)
        ),
        Identifier = credential.Identifier
      };
    }
  }
}