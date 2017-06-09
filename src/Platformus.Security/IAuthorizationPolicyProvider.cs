// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;

namespace Platformus.Security
{
  public interface IAuthorizationPolicyProvider
  {
    string Name { get; }
    AuthorizationPolicy GetAuthorizationPolicy();
  }
}