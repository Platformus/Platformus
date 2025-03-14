// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using Platformus.Core.Api.Services.Abstractions;

namespace Platformus.Core.Api.Services;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
  public string Generate()
  {
    byte[] randomNumber = new byte[32];

    using RandomNumberGenerator generator = RandomNumberGenerator.Create();
    generator.GetBytes(randomNumber);
    return Convert.ToBase64String(randomNumber);
  }
}
