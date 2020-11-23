// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public class CredentialViewModelFactory : ViewModelFactoryBase
  {
    public CredentialViewModel Create(Credential credential)
    {
      return new CredentialViewModel()
      {
        Id = credential.Id,
        CredentialType = new CredentialTypeViewModelFactory().Create(credential.CredentialType),
        Identifier = credential.Identifier
      };
    }
  }
}