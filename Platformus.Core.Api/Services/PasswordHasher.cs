// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Platformus.Core.Api.Services.Abstractions;

namespace Platformus.Core.Api.Services;

public class PasswordHasher : IPasswordHasher
{
  public PasswordHasher()
  {
  }

  public string ComputeHash(string password, byte[] salt)
  {
    return Convert.ToBase64String(
      KeyDerivation.Pbkdf2(
        password: password,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 100000,
        numBytesRequested: 256 / 8
      )
    );
  }

  public byte[] GenerateRandomSalt()
  {
    byte[] salt = new byte[128 / 8];

    using (var rng = RandomNumberGenerator.Create())
      rng.GetBytes(salt);

    return salt;
  }
}
