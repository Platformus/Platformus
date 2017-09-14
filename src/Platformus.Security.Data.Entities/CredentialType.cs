// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Security.Data.Entities
{
  /// <summary>
  /// Represents a credential type. The credential types are used to specify which identity providers
  /// (email and password, Microsoft, Google, Facebook and so on) are supported by the web application.
  /// Web application uses the different mechanisms to verify the credentials of the different credential types.
  /// </summary>
  public class CredentialType : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the credential type.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the credential type. It is set by the user and might be used for the credential type retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the credential name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the credential type position. Position is used to sort the credential types (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual ICollection<Credential> Credentials { get; set; }
  }
}