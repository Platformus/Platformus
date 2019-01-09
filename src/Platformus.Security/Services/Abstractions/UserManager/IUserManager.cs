// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Security.Data.Entities;

namespace Platformus.Security.Services.Abstractions
{
  public interface IUserManager
  {
    SignUpResult SignUp(string name, string credentialTypeCode, string identifier);
    SignUpResult SignUp(string name, string credentialTypeCode, string identifier, string secret);
    void AddToRole(User user, string roleCode);
    void AddToRole(User user, Role role);
    void RemoveFromRole(User user, string roleCode);
    void RemoveFromRole(User user, Role role);
    ChangeSecretResult ChangeSecret(string credentialTypeCode, string identifier, string secret);
    ValidateResult Validate(string credentialTypeCode, string identifier);
    ValidateResult Validate(string credentialTypeCode, string identifier, string secret);
    void SignIn(User user, string authenticationScheme, bool isPersistent = false);
    void SignOut(string authenticationScheme);
    int GetCurrentUserId();
    User GetCurrentUser();
  }
}