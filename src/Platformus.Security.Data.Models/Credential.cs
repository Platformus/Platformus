// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Security.Data.Models
{
  public class Credential : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }

    //[Required]
    public int UserId { get; set; }

    //[Required]
    public int CredentialTypeId { get; set; }

    //[Required]
    //[StringLength(64)]
    public string Identifier { get; set; }

    //[StringLength(1024)]
    public string Secret { get; set; }

    public virtual User User { get; set; }
    public virtual CredentialType CredentialType { get; set; }
  }
}