// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Security.Services.Abstractions
{
  public enum ChangeSecretResultError
  {
    CredentialTypeNotFound,
    CredentialNotFound
  }

  public class ChangeSecretResult
  {
    public bool Success { get; set; }
    public ChangeSecretResultError? Error { get; set; }

    public ChangeSecretResult(bool success = false, ChangeSecretResultError? error = null)
    {
      this.Success = success;
      this.Error = error;
    }
  }
}