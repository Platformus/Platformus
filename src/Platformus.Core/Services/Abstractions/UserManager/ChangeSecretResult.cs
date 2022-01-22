// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Services.Abstractions
{
  /// <summary>
  /// Describes a secret changing result.
  /// </summary>
  public class ChangeSecretResult
  {
    /// <summary>
    /// Indicates if a secret was changed successfully.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// A secret changing error details.
    /// </summary>
    public ChangeSecretError? Error { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeSecretResult"/> class.
    /// </summary>
    /// <param name="success">Indicates if a secret was changed successfully.</param>
    /// <param name="error">A secret changing error details.</param>
    public ChangeSecretResult(bool success = true, ChangeSecretError? error = null)
    {
      this.Success = success;
      this.Error = error;
    }
  }
}