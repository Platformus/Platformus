// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class CredentialTypeViewModelFactory : ViewModelFactoryBase
  {
    public CredentialTypeViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CredentialTypeViewModel Create(CredentialType credentialType)
    {
      return new CredentialTypeViewModel()
      {
        Id = credentialType.Id,
        Name = credentialType.Name,
        Position = credentialType.Position
      };
    }
  }
}