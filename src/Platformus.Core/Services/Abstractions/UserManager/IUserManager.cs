// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Platformus.Core.Data.Entities;

namespace Platformus.Core.Services.Abstractions
{
  public interface IUserManager
  {
    Task<SignUpResult> SignUpAsync(string name, string credentialTypeCode, string identifier);
    Task<SignUpResult> SignUpAsync(string name, string credentialTypeCode, string identifier, string secret);
    Task AddToRoleAsync(User user, string roleCode);
    Task AddToRoleAsync(User user, Role role);
    Task RemoveFromRoleAsync(User user, string roleCode);
    Task RemoveFromRoleAsync(User user, Role role);
    Task<ChangeSecretResult> ChangeSecretAsync(string credentialTypeCode, string identifier, string secret);
    Task<ValidateResult> ValidateAsync(string credentialTypeCode, string identifier);
    Task<ValidateResult> ValidateAsync(string credentialTypeCode, string identifier, string secret);
    Task SignInAsync(User user, string authenticationScheme, bool isPersistent = false);
    Task SignOutAsync(string authenticationScheme);
    int GetCurrentUserId();
    Task<User> GetCurrentUserAsync();
  }
}