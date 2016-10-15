// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Backend.ViewModels.Credentials
{
  public class CreateOrEditViewModelMapper : ViewModelFactoryBase
  {
    public CreateOrEditViewModelMapper(IHandler handler)
      : base(handler)
    {
    }

    public Credential Map(CreateOrEditViewModel createOrEdit)
    {
      Credential credential = new Credential();

      if (createOrEdit.Id != null)
        credential = this.handler.Storage.GetRepository<ICredentialRepository>().WithKey((int)createOrEdit.Id);

      else credential.UserId = createOrEdit.UserId;

      credential.CredentialTypeId = createOrEdit.CredentialTypeId;
      credential.Identifier = createOrEdit.Identifier;

      if (!string.IsNullOrEmpty(createOrEdit.Secret))
        credential.Secret = createOrEdit.ApplyMd5HashingToSecret ? MD5Hasher.ComputeHash(createOrEdit.Secret) : createOrEdit.Secret;

      return credential;
    }
  }
}