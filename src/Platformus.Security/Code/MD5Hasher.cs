// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Security.Cryptography;
using System.Text;

namespace Platformus.Security
{
  public static class MD5Hasher
  {
    public static string ComputeHash(string data)
    {
      return BitConverter.ToString(
        MD5.Create().ComputeHash(
          Encoding.UTF8.GetBytes(data)
        )
      );
    }
  }
}