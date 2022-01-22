// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Services.Abstractions
{
  /// <summary>
  /// Describes a user validation result.
  /// </summary>
  public class ValidateResult
  {
    /// <summary>
    /// The validated user.
    /// </summary>
    public User User { get; }

    /// <summary>
    /// Indicates if a user validation was successful.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// A user validation error details.
    /// </summary>
    public ValidateError? Error { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidateResult"/> class.
    /// </summary>
    /// <param name="user">The validated user.</param>
    /// <param name="success">Indicates if a user validation was successful.</param>
    /// <param name="error">A user validation error details.</param>
    public ValidateResult(User user = null, bool success = false, ValidateError? error = null)
    {
      this.User = user;
      this.Success = success;
      this.Error = error;
    }
  }
}