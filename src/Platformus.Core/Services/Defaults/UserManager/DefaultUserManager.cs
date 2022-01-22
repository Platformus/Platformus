// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Services.Defaults
{
  public class DefaultUserManager : IUserManager
  {
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IStorage storage;
    private readonly IRepository<int, Permission, PermissionFilter> permissionRepository;
    private readonly IRepository<int, Role, RoleFilter> roleRepository;
    private readonly IRepository<int, int, RolePermission, RolePermissionFilter> rolePermissionRepository;
    private readonly IRepository<int, User, UserFilter> userRepository;
    private readonly IRepository<int, int, UserRole, UserRoleFilter> userRoleRepository;
    private readonly IRepository<int, CredentialType, CredentialTypeFilter> credentialTypeRepository;
    private readonly IRepository<int, Credential, CredentialFilter> credentialRepository;
    
    public DefaultUserManager(IHttpContextAccessor httpContextAccessor, IStorage storage)
    {
      this.httpContextAccessor = httpContextAccessor;
      this.storage = storage;
      this.permissionRepository = this.storage.GetRepository<int, Permission, PermissionFilter>();
      this.roleRepository = this.storage.GetRepository<int, Role, RoleFilter>();
      this.rolePermissionRepository = this.storage.GetRepository<int, int, RolePermission, RolePermissionFilter>();
      this.userRepository = this.storage.GetRepository<int, User, UserFilter>();
      this.userRoleRepository = this.storage.GetRepository<int, int, UserRole, UserRoleFilter>();
      this.credentialTypeRepository = this.storage.GetRepository<int, CredentialType, CredentialTypeFilter>();
      this.credentialRepository = this.storage.GetRepository<int, Credential, CredentialFilter>();
    }

    public async Task<SignUpResult> SignUpAsync(string name, string credentialTypeCode, string identifier)
    {
      return await this.SignUpAsync(name,  credentialTypeCode, identifier, null);
    }

    public async Task<SignUpResult> SignUpAsync(string name, string credentialTypeCode, string identifier, string secret)
    {
      CredentialType credentialType = (await this.credentialTypeRepository.GetAllAsync(new CredentialTypeFilter(code: credentialTypeCode))).FirstOrDefault();

      if (credentialType == null)
        return new SignUpResult(success: false, error: SignUpError.CredentialTypeNotFound);

      User user = new User();

      user.Name = name;
      user.Created = DateTime.Now;
      this.userRepository.Create(user);

      Credential credential = new Credential();

      credential.User = user;
      credential.CredentialType = credentialType;
      credential.Identifier = identifier;

      if (!string.IsNullOrEmpty(secret))
      {
        byte[] salt = Pbkdf2Hasher.GenerateRandomSalt();
        string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

        credential.Secret = hash;
        credential.Extra = Convert.ToBase64String(salt);
      }

      this.credentialRepository.Create(credential);
      await this.storage.SaveAsync();
      return new SignUpResult(user: user, success: true);
    }

    public async Task AddToRoleAsync(User user, string roleCode)
    {
      Role role = (await this.roleRepository.GetAllAsync(new RoleFilter(code: roleCode))).FirstOrDefault();

      if (role == null)
        return;

      await this.AddToRoleAsync(user, role);
    }

    public async Task AddToRoleAsync(User user, Role role)
    {
      UserRole userRole = await this.userRoleRepository.GetByIdAsync(user.Id, role.Id);

      if (userRole != null)
        return;

      userRole = new UserRole();
      userRole.UserId = user.Id;
      userRole.RoleId = role.Id;
      this.userRoleRepository.Create(userRole);
      await this.storage.SaveAsync();
    }

    public async Task RemoveFromRoleAsync(User user, string roleCode)
    {
      Role role = (await this.roleRepository.GetAllAsync(new RoleFilter(code: roleCode))).FirstOrDefault();

      if (role == null)
        return;

      await this.RemoveFromRoleAsync(user, role);
    }

    public async Task RemoveFromRoleAsync(User user, Role role)
    {
      UserRole userRole = await this.userRoleRepository.GetByIdAsync(user.Id, role.Id);

      if (userRole == null)
        return;

      this.userRoleRepository.Delete(userRole.UserId, userRole.RoleId);
      await this.storage.SaveAsync();
    }

    public async Task<ChangeSecretResult> ChangeSecretAsync(string credentialTypeCode, string identifier, string secret)
    {
      Credential credential = (await this.credentialRepository.GetAllAsync(new CredentialFilter(credentialType: new CredentialTypeFilter(code: credentialTypeCode), identifier: new StringFilter(equals: identifier)))).FirstOrDefault();

      if (credential == null)
        return new ChangeSecretResult(success: false, error: ChangeSecretError.CredentialNotFound);

      byte[] salt = Pbkdf2Hasher.GenerateRandomSalt();
      string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

      credential.Secret = hash;
      credential.Extra = Convert.ToBase64String(salt);
      this.credentialRepository.Edit(credential);
      await this.storage.SaveAsync();
      return new ChangeSecretResult(success: true);
    }

    public async Task<ValidateResult> ValidateAsync(string credentialTypeCode, string identifier)
    {
      return await this.ValidateAsync(credentialTypeCode, identifier, null);
    }

    public async Task<ValidateResult> ValidateAsync(string credentialTypeCode, string identifier, string secret)
    {
      Credential credential = (await this.credentialRepository.GetAllAsync(new CredentialFilter(credentialType: new CredentialTypeFilter(code: credentialTypeCode), identifier: new StringFilter(equals: identifier)), inclusions: new Inclusion<Credential>(c => c.User))).FirstOrDefault();

      if (credential == null)
        return new ValidateResult(success: false, error: ValidateError.CredentialNotFound);

      if (!string.IsNullOrEmpty(secret))
      {
        byte[] salt = Convert.FromBase64String(credential.Extra);
        string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

        if (credential.Secret != hash)
          return new ValidateResult(success: false, error: ValidateError.SecretNotValid);
      }

      return new ValidateResult(user: credential.User, success: true);
    }

    public async Task SignInAsync(User user, string authenticationScheme, bool isPersistent = false)
    {
      ClaimsIdentity identity = new ClaimsIdentity(await this.GetUserClaimsAsync(user), authenticationScheme);
      ClaimsPrincipal principal = new ClaimsPrincipal(identity);

      await this.httpContextAccessor.HttpContext.SignInAsync(
        authenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent }
      );
    }

    public async Task SignOutAsync(string authenticationScheme)
    {
      await this.httpContextAccessor.HttpContext.SignOutAsync(authenticationScheme);
    }

    public int GetCurrentUserId()
    {
      if (!this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        return -1;

      Claim claim = this.httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

      if (claim == null)
        return -1;

      int currentUserId;

      if (!int.TryParse(claim.Value, out currentUserId))
        return -1;

      return currentUserId;
    }

    public async Task<User> GetCurrentUserAsync()
    {
      int currentUserId = this.GetCurrentUserId();

      if (currentUserId == -1)
        return null;

      return await this.userRepository.GetByIdAsync(currentUserId);
    }

    private async Task<IEnumerable<Claim>> GetUserClaimsAsync(User user)
    {
      List<Claim> claims = new List<Claim>();

      claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
      claims.Add(new Claim(ClaimTypes.Name, user.Name));
      claims.AddRange(await this.GetUserRoleClaimsAsync(user));
      return claims;
    }

    private async Task<IEnumerable<Claim>> GetUserRoleClaimsAsync(User user)
    {
      List<Claim> claims = new List<Claim>();
      IEnumerable<int> roleIds = (await this.userRoleRepository.GetAllAsync(new UserRoleFilter(user: new UserFilter(id: user.Id)))).Select(ur => ur.RoleId).ToList();

      if (roleIds != null)
      {
        foreach (int roleId in roleIds)
        {
          Role role = await roleRepository.GetByIdAsync(roleId);

          claims.Add(new Claim(ClaimTypes.Role, role.Code));
          claims.AddRange(await this.GetUserPermissionClaimsAsync(role));
        }
      }

      return claims;
    }

    private async Task<IEnumerable<Claim>> GetUserPermissionClaimsAsync(Role role)
    {
      List<Claim> claims = new List<Claim>();
      IEnumerable<int> permissionIds = (await this.rolePermissionRepository.GetAllAsync(new RolePermissionFilter(role: new RoleFilter(id: role.Id)))).Select(rp => rp.PermissionId).ToList();

      if (permissionIds != null)
      {
        foreach (int permissionId in permissionIds)
        {
          Permission permission = await this.permissionRepository.GetByIdAsync(permissionId);

          claims.Add(new Claim(PlatformusClaimTypes.Permission, permission.Code));
        }
      }

      return claims;
    }
  }
}