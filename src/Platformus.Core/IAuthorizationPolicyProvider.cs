// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;

namespace Platformus.Core
{
  /// <summary>
  /// Describes a provider that can be implemented
  /// to create the authorization policies in the Platformus extensions and to automatically register them
  /// using the <see cref="Actions.AddAuthorizationAction"/> action and then used
  /// with the <see cref="AuthorizeAttribute"/> attributes.
  /// </summary>
  public interface IAuthorizationPolicyProvider
  {
    /// <summary>
    /// Gets the authorization policy name (used with the <see cref="AuthorizeAttribute"/> attributes).
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Creates an authorization policy with all the applied rules.
    /// </summary>
    AuthorizationPolicy GetAuthorizationPolicy();
  }
}