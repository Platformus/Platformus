// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Admin.Services.Abstractions;

public interface IAuthService
{
  Task<bool> IsAuthenticatedAsync();
  Task<bool> SignInAsync(string email, string password);
  Task SignOutAsync();
}