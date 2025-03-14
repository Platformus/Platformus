// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Api.Services;

public class AccessTokenGeneratorOptions
{
  public string? ServerKey { get; set; }
  public string? Issuer { get; set; }
  public string? Audience { get; set; }
  public int Expires { get; set; }
}
