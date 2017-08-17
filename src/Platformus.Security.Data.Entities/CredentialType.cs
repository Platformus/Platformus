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
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }

    public virtual ICollection<Credential> Credentials { get; set; }
  }
}