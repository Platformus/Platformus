// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Security.Data.Entities;

namespace Platformus.Security.Services.Abstractions
{
  public enum SignUpResultError
  {
    CredentialTypeNotFound
  }

  public class SignUpResult
  {
    public User User { get; set; }
    public bool Success { get; set; }
    public SignUpResultError? Error { get; set; }

    public SignUpResult(User user = null, bool success = false, SignUpResultError? error = null)
    {
      this.User = user;
      this.Success = success;
      this.Error = error;
    }
  }
}