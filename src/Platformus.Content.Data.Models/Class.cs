// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Content.Data.Models
{
  public class Class : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }

    //[Required]
    //[StringLength(64)]
    public string Name { get; set; }

    //[Required]
    //[StringLength(64)]
    public string PluralizedName { get; set; }
    public bool? IsStandalone { get; set; }

    //[StringLength(32)]
    public string ViewName { get; set; }

    //public virtual ICollection<Tab> Tabs { get; set; }
    //public virtual ICollection<Member> Members { get; set; }
    //public virtual ICollection<DataSource> DataSources { get; set; }
    //public virtual ICollection<Object> Objects { get; set; }
  }
}