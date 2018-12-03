// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Platformus.Barebone;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security
{
  public class UserManager
  {
    public enum SignUpResultError
    {
      CredentialTypeNotFound
    }

    public class SignUpResult
    {
      public User User { get; set; }
      public bool Success { get; set; }
      public SignUpResultError? Error { get; set; }

      public SignUpResult(User user = null, bool success = false, SignUpResultError? error = null)
      {
        this.User = user;
        this.Success = success;
        this.Error = error;
      }
    }

    public enum ValidateResultError
    {
      CredentialTypeNotFound,
      CredentialNotFound,
      SecretNotValid
    }

    public class ValidateResult
    {
      public User User { get; set; }
      public bool Success { get; set; }
      public ValidateResultError? Error { get; set; }

      public ValidateResult(User user = null, bool success = false, ValidateResultError? error = null)
      {
        this.User = user;
        this.Success = success;
        this.Error = error;
      }
    }

    public enum ChangeSecretResultError
    {
      CredentialTypeNotFound,
      CredentialNotFound
    }

    public class ChangeSecretResult
    {
      public bool Success { get; set; }
      public ChangeSecretResultError? Error { get; set; }

      public ChangeSecretResult(bool success = false, ChangeSecretResultError? error = null)
      {
        this.Success = success;
        this.Error = error;
      }
    }

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

    public SignUpResult SignUp(string name, string credentialTypeCode, string identifier)
    {
      return this.SignUp(name,  credentialTypeCode, identifier, null);
    }

    public SignUpResult SignUp(string name, string credentialTypeCode, string identifier, string secret)
    {
      User user = new User();

      user.Name = name;
      user.Created = DateTime.Now;
      this.userRepository.Create(user);
      this.requestHandler.Storage.Save();

      CredentialType credentialType = this.credentialTypeRepository.WithCode(credentialTypeCode);

      if (credentialType == null)
        return new SignUpResult(success: false, error: SignUpResultError.CredentialTypeNotFound);

      Credential credential = new Credential();

      credential.UserId = user.Id;
      credential.CredentialTypeId = credentialType.Id;
      credential.Identifier = identifier;

      if (!string.IsNullOrEmpty(secret))
      {
        byte[] salt = Pbkdf2Hasher.GenerateRandomSalt();
        string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

        credential.Secret = hash;
        credential.Extra = Convert.ToBase64String(salt);
      }

      this.credentialRepository.Create(credential);
      this.requestHandler.Storage.Save();
      return new SignUpResult(user: user, success: true);
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

    public ChangeSecretResult ChangeSecret(string credentialTypeCode, string identifier, string secret)
    {
      CredentialType credentialType = this.credentialTypeRepository.WithCode(credentialTypeCode);

      if (credentialType == null)
        return new ChangeSecretResult(success: false, error: ChangeSecretResultError.CredentialTypeNotFound);

      Credential credential = this.credentialRepository.WithCredentialTypeIdAndIdentifier(credentialType.Id, identifier);

      if (credential == null)
        return new ChangeSecretResult(success: false, error: ChangeSecretResultError.CredentialNotFound);

      byte[] salt = Pbkdf2Hasher.GenerateRandomSalt();
      string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

      credential.Secret = hash;
      credential.Extra = Convert.ToBase64String(salt);
      this.credentialRepository.Edit(credential);
      this.requestHandler.Storage.Save();
      return new ChangeSecretResult(success: true);
    }

    public ValidateResult Validate(string credentialTypeCode, string identifier)
    {
      return this.Validate(credentialTypeCode, identifier, null);
    }

    public ValidateResult Validate(string credentialTypeCode, string identifier, string secret)
    {
      CredentialType credentialType = this.credentialTypeRepository.WithCode(credentialTypeCode);

      if (credentialType == null)
        return new ValidateResult(success: false, error: ValidateResultError.CredentialTypeNotFound);

      Credential credential = this.credentialRepository.WithCredentialTypeIdAndIdentifier(credentialType.Id, identifier);

      if (credential == null)
        return new ValidateResult(success: false, error: ValidateResultError.CredentialNotFound);

      if (!string.IsNullOrEmpty(secret))
      {
        byte[] salt = Convert.FromBase64String(credential.Extra);
        string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

        if (credential.Secret != hash)
          return new ValidateResult(success: false, error: ValidateResultError.SecretNotValid);
      }

      return new ValidateResult(user: this.userRepository.WithKey(credential.UserId), success: true);
    }

    public async void SignIn(User user, string authenticationScheme, bool isPersistent = false)
    {
      ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), authenticationScheme);
      ClaimsPrincipal principal = new ClaimsPrincipal(identity);

      await this.requestHandler.HttpContext.SignInAsync(
        authenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent }
      );
    }

    public async void SignOut(string authenticationScheme)
    {
      await this.requestHandler.HttpContext.SignOutAsync(authenticationScheme);
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