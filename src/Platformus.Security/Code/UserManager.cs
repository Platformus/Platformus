// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Platformus.Barebone;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security
{
  public class UserManager
  {
    private IHandler handler;
    private ICredentialRepository credentialRepository;
    private ICredentialTypeRepository credentialTypeRepository;
    private IUserRepository userRepository;
    private IUserRoleRepository userRoleRepository;
    private IRoleRepository roleRepository;    

    public UserManager(IHandler handler)
    {
      this.handler = handler;
      credentialRepository = handler.Storage.GetRepository<ICredentialRepository>();
      credentialTypeRepository = handler.Storage.GetRepository<ICredentialTypeRepository>();
      userRepository = handler.Storage.GetRepository<IUserRepository>();
      userRoleRepository = handler.Storage.GetRepository<IUserRoleRepository>();
      roleRepository = handler.Storage.GetRepository<IRoleRepository>();      
    }

    public User Validate(string loginTypeCode, string identifier, string secret)
    {
      CredentialType credentialType = this.credentialTypeRepository.WithCode(loginTypeCode);

      if (credentialType == null)
        return null;

      Credential login = this.credentialRepository.WithCredentialTypeIdAndIdentifierAndSecret(
        credentialType.Id, identifier, MD5Hasher.ComputeHash(secret)
      );

      if (login == null)
        return null;

      return this.userRepository.WithKey(login.UserId);
    }

    public async void SignIn(User user)
    {
      List<Claim> claims = new List<Claim>();      
      IEnumerable<int> roles = this.userRoleRepository.FilteredByUserId(user.Id)?.Select(x=>x.RoleId);      
      if(roles != null)
        foreach (var element in roles)
        {
          Role role = roleRepository.WithKey(element);
          claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }
      
      claims.Add(new Claim(ClaimTypes.Name, string.Format("user{0}", user.Id)));
      
      ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
      ClaimsPrincipal principal = new ClaimsPrincipal(identity);

      await this.handler.HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }

    public async void SignOut()
    {
      await this.handler.HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public User GetCurrentUser()
    {
      if (!this.handler.HttpContext.User.Identity.IsAuthenticated)
        return null;

      int currentUserId;

      if (!int.TryParse(this.handler.HttpContext.User.Identity.Name.Replace("user", string.Empty), out currentUserId))
        return null;

      return this.userRepository.WithKey(currentUserId);
    }
  }
}