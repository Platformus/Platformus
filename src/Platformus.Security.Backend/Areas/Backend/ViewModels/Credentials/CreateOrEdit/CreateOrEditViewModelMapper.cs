// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;
using Platformus.Security.Services.Default;

namespace Platformus.Security.Backend.ViewModels.Credentials
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Credential Map(CreateOrEditViewModel createOrEdit)
    {
      Credential credential = new Credential();

      if (createOrEdit.Id != null)
        credential = this.RequestHandler.Storage.GetRepository<ICredentialRepository>().WithKey((int)createOrEdit.Id);

      else credential.UserId = createOrEdit.UserId;

      credential.CredentialTypeId = createOrEdit.CredentialTypeId;
      credential.Identifier = createOrEdit.Identifier;

      if (!string.IsNullOrEmpty(createOrEdit.Secret))
      {
        if (createOrEdit.ApplyPbkdf2HashingToSecret)
        {
          byte[] salt = Pbkdf2Hasher.GenerateRandomSalt();
          string hash = Pbkdf2Hasher.ComputeHash(createOrEdit.Secret, salt);

          credential.Secret = hash;
          credential.Extra = Convert.ToBase64String(salt);
        }

        else credential.Secret = createOrEdit.Secret;
      }

      return credential;
    }
  }
}