// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Security.Data.Models
{
  public class UserRole : IEntity
  {
    //[Key]
    //[Required]
    public int UserId { get; set; }

    //[Key]
    //[Required]
    public int RoleId { get; set; }

    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
  }
}