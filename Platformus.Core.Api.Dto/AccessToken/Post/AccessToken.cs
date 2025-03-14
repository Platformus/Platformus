// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Core.Api.Dto.AccessToken.Post;

public class AccessToken : IDto
{
  public string? Identifier { get; set; }
  public string? Secret { get; set; }
  public string? Extra { get; set; }
  public string? RefreshToken { get; set; }
}