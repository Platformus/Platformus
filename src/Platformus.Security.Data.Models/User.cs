// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using ExtCore.Data.Models.Abstractions;

namespace Platformus.Security.Data.Models
{
  public class User : IEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }

    public virtual ICollection<Credential> Credentials { get; set; }
  }
}