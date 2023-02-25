// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Services.Abstractions;

/// <summary>
/// Describes possible user validation errors.
/// </summary>
public enum ValidateError
{
  CredentialTypeNotFound,
  CredentialNotFound,
  SecretNotValid
}