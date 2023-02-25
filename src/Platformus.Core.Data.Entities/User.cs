// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Core.Data.Entities;

/// <summary>
/// Represents a user. The user is the central unit in the Platformus authentication and authorization mechanism.
/// </summary>
public class User : IEntity<int>
{
  /// <summary>
  /// Gets or sets the unique identifier of the user.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Gets or sets the user name.
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// Gets or sets the date and time this user is created at.
  /// </summary>
  public DateTime Created { get; set; }

  public virtual ICollection<UserRole> UserRoles { get; set; }
  public virtual ICollection<Credential> Credentials { get; set; }
}