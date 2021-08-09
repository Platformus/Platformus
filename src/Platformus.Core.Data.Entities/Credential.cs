// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Core.Data.Entities
{
  /// <summary>
  /// Represents a credential. The credentials are used to store the information that is used to sign in the users
  /// using the different identity providers (email and password, Microsoft, Google, Facebook and so on).
  /// The identity provider is specified by the credential type.
  /// </summary>
  public class Credential : IEntity<int>
  {
    /// <summary>
    /// Gets or sets the unique identifier of the credential.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the user identifier this credential belongs to.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the credential type identifier this credential is related to.
    /// </summary>
    public int CredentialTypeId { get; set; }

    /// <summary>
    /// Gets or sets the credential identifier.
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Gets or sets the credential secret (or its hash).
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    /// Gets or sets the credential extra data (might be used to store password salt and so on).
    /// </summary>
    public string Extra { get; set; }

    public virtual User User { get; set; }
    public virtual CredentialType CredentialType { get; set; }
  }
}