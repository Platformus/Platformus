// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.IdentityModel.Tokens;
using Platformus.Core.Domain.Models;

namespace Platformus.Core.Api.Services.Abstractions;

public interface IAccessTokenGenerator
{
  string Generate(User user);
  SecurityKey CreateSecurityKey();
}
