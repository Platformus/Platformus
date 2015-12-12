// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Security.Data.Models
{
  public class RolePermission : IEntity
  {
    //[Key]
    //[Required]
    public int RoleId { get; set; }

    //[Key]
    //[Required]
    public int PermissionId { get; set; }

    public virtual Role Role { get; set; }
    public virtual Permission Permission { get; set; }
  }
}