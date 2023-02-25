// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Platformus.Core.Data.Entities;

namespace Platformus.Core.Services.Abstractions;

/// <summary>
/// Describes a user manager to manipulate users: validating and signing up/in, adding and removing roles etc.
/// </summary>
public interface IUserManager
{
  /// <summary>
  /// Signs up a user.
  /// </summary>
  /// <param name="name">An unique username.</param>
  /// <param name="credentialTypeCode">A code of the credential type the provided credential <paramref name="identifier"/> belongs to.</param>
  /// <param name="identifier">A user credential identifier (example: email or social network ID).</param>
  Task<SignUpResult> SignUpAsync(string name, string credentialTypeCode, string identifier);

  /// <summary>
  /// Signs up a user.
  /// </summary>
  /// <param name="name">An unique username.</param>
  /// <param name="credentialTypeCode">A code of the credential type the provided credential <paramref name="identifier"/> belongs to.</param>
  /// <param name="identifier">A user credential identifier (example: email or social network ID).</param>
  /// <param name="secret">A user credential secret (it could be a password hash for the login/password credential type).</param>
  Task<SignUpResult> SignUpAsync(string name, string credentialTypeCode, string identifier, string secret);

  /// <summary>
  /// Adds user to a role.
  /// </summary>
  /// <param name="user">A user that should be added to a role.</param>
  /// <param name="roleCode">A role code the specified <paramref name="user"/> should be added to.</param>
  Task AddToRoleAsync(User user, string roleCode);

  /// <summary>
  /// Adds user to a role.
  /// </summary>
  /// <param name="user">A user that should be added to the <paramref name="role"/>.</param>
  /// <param name="role">A role the specified <paramref name="user"/> should be added to.</param>
  Task AddToRoleAsync(User user, Role role);

  /// <summary>
  /// Removes user from a role.
  /// </summary>
  /// <param name="user">A user that should be removed from a role.</param>
  /// <param name="roleCode">A role code the specified <paramref name="user"/> should be removed from.</param>
  Task RemoveFromRoleAsync(User user, string roleCode);

  /// <summary>
  /// Removes user from a role.
  /// </summary>
  /// <param name="user">A user that should be removed from a <paramref name="role"/>.</param>
  /// <param name="role">A role the specified <paramref name="user"/> should be removed from.</param>
  Task RemoveFromRoleAsync(User user, Role role);

  /// <summary>
  /// Changes a credential secret.
  /// </summary>
  /// <param name="credentialTypeCode">A code of the credential type the provided credential <paramref name="identifier"/> belongs to.</param>
  /// <param name="identifier">A user credential identifier (example: email or social network ID).</param>
  /// <param name="secret">A new user credential secret (it could be a password hash for the login/password credential type).</param>
  Task<ChangeSecretResult> ChangeSecretAsync(string credentialTypeCode, string identifier, string secret);

  /// <summary>
  /// Validates that user with the given credential type code and identifier exists.
  /// </summary>
  /// <param name="credentialTypeCode">A code of the credential type the provided credential <paramref name="identifier"/> belongs to.</param>
  /// <param name="identifier">A user credential identifier (example: email or social network ID).</param>
  Task<ValidateResult> ValidateAsync(string credentialTypeCode, string identifier);

  /// <summary>
  /// Validates that user with the given credential type code, identifier, and secret exists.
  /// </summary>
  /// <param name="credentialTypeCode">A code of the credential type the provided credential <paramref name="identifier"/> belongs to.</param>
  /// <param name="identifier">A user credential identifier (example: email or social network ID).</param>
  /// <param name="secret">A user credential secret (it could be a password hash for the login/password credential type).</param>
  Task<ValidateResult> ValidateAsync(string credentialTypeCode, string identifier, string secret);

  /// <summary>
  /// Signs in a user.
  /// </summary>
  /// <param name="user">A user to sign in.</param>
  /// <param name="authenticationScheme">An authentication scheme the <paramref name="user"/> should be signed in with.</param>
  /// <param name="isPersistent">Indicates if the <paramref name="user"/> should remain signed in after the current session ends.</param>
  Task SignInAsync(User user, string authenticationScheme, bool isPersistent = false);

  /// <summary>
  /// Signs out a user.
  /// </summary>
  /// <param name="authenticationScheme">An authentication scheme the current user should be signed out from.</param>
  Task SignOutAsync(string authenticationScheme);

  /// <summary>
  /// Gets the current (signed in) user ID.
  /// </summary>
  int GetCurrentUserId();

  /// <summary>
  /// Gets the current (signed in) user.
  /// </summary>
  Task<User> GetCurrentUserAsync();
}