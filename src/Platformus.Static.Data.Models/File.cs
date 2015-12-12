// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Static.Data.Models
{
  public class File : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }

    //[Required]
    //[StringLength(64)]
    public string Name { get; set; }

    //[Required]
    public long Size { get; set; }
  }
}