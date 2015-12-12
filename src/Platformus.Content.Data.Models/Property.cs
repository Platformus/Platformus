// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Globalization.Data.Models;
using ExtCore.Data.Models.Abstractions;

namespace Platformus.Content.Data.Models
{
  public class Property : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }
    public int? ObjectId { get; set; }

    //[Required]
    public int MemberId { get; set; }

    //[Required]
    public int HtmlId { get; set; }

    public virtual Object Object { get; set; }
    public virtual Member Member { get; set; }
    public virtual Dictionary Html { get; set; }
  }
}