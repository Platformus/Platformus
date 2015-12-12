// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Content.Data.Models
{
  public class DataSource : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }

    //[Required]
    public int ClassId { get; set; }

    //[Required]
    //[StringLength(32)]
    public string Code { get; set; }

    //[Required]
    //[StringLength(128)]
    public string CSharpClassName { get; set; }

    //[StringLength(1024)]
    public string Parameters { get; set; }

    public virtual Class Class { get; set; }
  }
}