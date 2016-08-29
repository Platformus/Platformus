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
    private IRoleRepository roleRepository;
    private IUserRepository userRepository;
    private IUserRoleRepository userRoleRepository;
    private ICredentialTypeRepository credentialTypeRepository;
    private ICredentialRepository credentialRepository;
    private IRolePermissionRepository rolePermissionRepository;
    private IPermissionRepository permissionRepository;

    private void AddUserRoles(List<Claim> claims, int UserId)
    {
      IEnumerable<int> roleIds = this.userRoleRepository.FilteredByUserId(UserId)?.Select(x => x.RoleId);
      if (roleIds != null)
        foreach (int roleId in roleIds)
        {
          Role role = roleRepository.WithKey(roleId);
          claims.Add(new Claim(ClaimTypes.Role, role.Name));
          AddUserPermissions(claims, roleId);
        }
    }

    private void AddUserPermissions(List<Claim> claims, int roleId)
    {
      IEnumerable<int> permissionIds = this.rolePermissionRepository.FilteredByRoleId(roleId)?.Select(x=>x.PermissionId);
      if(permissionIds != null)
        foreach(int permissionId in permissionIds)
        {
          Permission permission = this.permissionRepository.WithKey(permissionId);
          claims.Add(new Claim(PlatformusClaimTypes.Permission, permission.Name));
        }
    }

    public UserManager(IHandler handler)
    {
      this.handler = handler;
      this.roleRepository = handler.Storage.GetRepository<IRoleRepository>();
      this.userRepository = handler.Storage.GetRepository<IUserRepository>();
      this.userRoleRepository = handler.Storage.GetRepository<IUserRoleRepository>();
      this.credentialTypeRepository = handler.Storage.GetRepository<ICredentialTypeRepository>();
      this.credentialRepository = handler.Storage.GetRepository<ICredentialRepository>();
      this.rolePermissionRepository = handler.Storage.GetRepository<IRolePermissionRepository>();
      this.permissionRepository = handler.Storage.GetRepository<IPermissionRepository>();                    
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

      AddUserRoles(claims, user.Id);
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