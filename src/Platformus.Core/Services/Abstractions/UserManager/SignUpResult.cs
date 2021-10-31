// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Services.Abstractions
{
  /// <summary>
  /// Describes possible user signing up errors.
  /// </summary>
  public enum SignUpResultError
  {
    CredentialTypeNotFound
  }

  /// <summary>
  /// Describes a user signing up result.
  /// </summary>
  public class SignUpResult
  {
    /// <summary>
    /// The signed up user.
    /// </summary>
    public User User { get; }

    /// <summary>
    /// Indicates if a user signing up was successful.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// A user signing up error details.
    /// </summary>
    public SignUpResultError? Error { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SignUpResult"/> class.
    /// </summary>
    /// <param name="user">The signed up user.</param>
    /// <param name="success">Indicates if a user signing up was successful.</param>
    /// <param name="error">A user signing up error details.</param>
    public SignUpResult(User user = null, bool success = false, SignUpResultError? error = null)
    {
      this.User = user;
      this.Success = success;
      this.Error = error;
    }
  }
}