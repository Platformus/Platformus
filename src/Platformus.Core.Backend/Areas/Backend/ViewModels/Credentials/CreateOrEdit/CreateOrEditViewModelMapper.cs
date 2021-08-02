// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using Platformus.Core.Services.Defaults;

namespace Platformus.Core.Backend.ViewModels.Credentials
{
  public static class CreateOrEditViewModelMapper
  {
    public static Credential Map(CredentialFilter filter, Credential credential, CreateOrEditViewModel createOrEdit)
    {
      if (credential.Id == 0)
        credential.UserId = (int)filter.User.Id;

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