// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Platformus.Barebone;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security
{
  public class UserManager
  {
    private IRequestHandler requestHandler;
    private IPermissionRepository permissionRepository;
    private IRolePermissionRepository rolePermissionRepository;
    private IRoleRepository roleRepository;
    private IUserRepository userRepository;
    private IUserRoleRepository userRoleRepository;
    private ICredentialTypeRepository credentialTypeRepository;
    private ICredentialRepository credentialRepository;
    
    public UserManager(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
      this.permissionRepository = this.requestHandler.Storage.GetRepository<IPermissionRepository>();
      this.roleRepository = this.requestHandler.Storage.GetRepository<IRoleRepository>();
      this.rolePermissionRepository = this.requestHandler.Storage.GetRepository<IRolePermissionRepository>();
      this.userRepository = this.requestHandler.Storage.GetRepository<IUserRepository>();
      this.userRoleRepository = this.requestHandler.Storage.GetRepository<IUserRoleRepository>();
      this.credentialTypeRepository = this.requestHandler.Storage.GetRepository<ICredentialTypeRepository>();
      this.credentialRepository = this.requestHandler.Storage.GetRepository<ICredentialRepository>();
    }

    public User SignUp(string name, string loginTypeCode, string identifier)
    {
      return this.SignUp(name,  loginTypeCode, identifier, null);
    }

    public User SignUp(string name, string loginTypeCode, string identifier, string secret)
    {
      User user = new User();

      user.Name = name;
      user.Created = DateTime.Now;
      this.userRepository.Create(user);
      this.requestHandler.Storage.Save();

      CredentialType credentialType = this.credentialTypeRepository.WithCode(loginTypeCode);

      if (credentialType != null)
      {
        Credential credential = new Credential();

        credential.UserId = user.Id;
        credential.CredentialTypeId = credentialType.Id;
        credential.Identifier = identifier;
        credential.Secret = string.IsNullOrEmpty(secret) ? null : MD5Hasher.ComputeHash(secret);
        this.credentialRepository.Create(credential);
        this.requestHandler.Storage.Save();
      }

      return user;
    }

    public void AddToRole(User user, string roleCode)
    {
      Role role = this.roleRepository.WithCode(roleCode);

      if (role == null)
        return;

      this.AddToRole(user, role);
    }

    public void AddToRole(User user, Role role)
    {
      UserRole userRole = this.userRoleRepository.WithKey(user.Id, role.Id);

      if (userRole != null)
        return;

      userRole = new UserRole();
      userRole.UserId = user.Id;
      userRole.RoleId = role.Id;
      this.userRoleRepository.Create(userRole);
      this.requestHandler.Storage.Save();
    }

    public void RemoveFromRole(User user, string roleCode)
    {
      Role role = this.roleRepository.WithCode(roleCode);

      if (role == null)
        return;

      this.RemoveFromRole(user, role);
    }

    public void RemoveFromRole(User user, Role role)
    {
      UserRole userRole = this.userRoleRepository.WithKey(user.Id, role.Id);

      if (userRole == null)
        return;

      this.userRoleRepository.Delete(userRole);
      this.requestHandler.Storage.Save();
    }

    public User Validate(string loginTypeCode, string identifier)
    {
      return this.Validate(loginTypeCode, identifier, null);
    }

    public User Validate(string loginTypeCode, string identifier, string secret)
    {
      CredentialType credentialType = this.credentialTypeRepository.WithCode(loginTypeCode);

      if (credentialType == null)
        return null;

      Credential credential = this.credentialRepository.WithCredentialTypeIdAndIdentifierAndSecret(
        credentialType.Id, identifier, string.IsNullOrEmpty(secret) ? null : MD5Hasher.ComputeHash(secret)
      );

      if (credential == null)
        return null;

      return this.userRepository.WithKey(credential.UserId);
    }

    public async void SignIn(User user, bool isPersistent = false)
    {
      ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
      ClaimsPrincipal principal = new ClaimsPrincipal(identity);

      await this.requestHandler.HttpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent }
      );
    }

    public async void SignOut()
    {
      await this.requestHandler.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public int GetCurrentUserId()
    {
      if (!this.requestHandler.HttpContext.User.Identity.IsAuthenticated)
        return -1;

      Claim claim = this.requestHandler.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

      if (claim == null)
        return -1;

      int currentUserId;

      if (!int.TryParse(claim.Value, out currentUserId))
        return -1;

      return currentUserId;
    }

    public User GetCurrentUser()
    {
      int currentUserId = this.GetCurrentUserId();

      if (currentUserId == -1)
        return null;

      return this.userRepository.WithKey(currentUserId);
    }

    private IEnumerable<Claim> GetUserClaims(User user)
    {
      List<Claim> claims = new List<Claim>();

      claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
      claims.Add(new Claim(ClaimTypes.Name, user.Name));
      claims.AddRange(this.GetUserRoleClaims(user));
      return claims;
    }

    private IEnumerable<Claim> GetUserRoleClaims(User user)
    {
      List<Claim> claims = new List<Claim>();
      IEnumerable<int> roleIds = this.userRoleRepository.FilteredByUserId(user.Id)?.Select(ur => ur.RoleId).ToList();

      if (roleIds != null)
      {
        foreach (int roleId in roleIds)
        {
          Role role = roleRepository.WithKey(roleId);

          claims.Add(new Claim(ClaimTypes.Role, role.Code));
          claims.AddRange(this.GetUserPermissionClaims(role));
        }
      }

      return claims;
    }

    private IEnumerable<Claim> GetUserPermissionClaims(Role role)
    {
      List<Claim> claims = new List<Claim>();
      IEnumerable<int> permissionIds = this.rolePermissionRepository.FilteredByRoleId(role.Id)?.Select(rp => rp.PermissionId).ToList();

      if (permissionIds != null)
      {
        foreach (int permissionId in permissionIds)
        {
          Permission permission = this.permissionRepository.WithKey(permissionId);

          claims.Add(new Claim(PlatformusClaimTypes.Permission, permission.Code));
        }
      }

      return claims;
    }
  }
}