// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Platformus.Security.Services.Defaults
{
  public static class Pbkdf2Hasher
  {
    public static string ComputeHash(string password, byte[] salt)
    {
      return Convert.ToBase64String(
        KeyDerivation.Pbkdf2(
          password: password,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA1,
          iterationCount: 10000,
          numBytesRequested: 256 / 8
        )
      );
    }

    public static byte[] GenerateRandomSalt()
    {
      byte[] salt = new byte[128 / 8];

      using (var rng = RandomNumberGenerator.Create())
        rng.GetBytes(salt);

      return salt;
    }
  }
}