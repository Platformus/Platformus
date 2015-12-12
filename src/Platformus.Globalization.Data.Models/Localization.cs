// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Globalization.Data.Models
{
  public class Localization : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }

    //[Required]
    public int DictionaryId { get; set; }

    //[Required]
    public int CultureId { get; set; }
    public string Value { get; set; }

    public virtual Dictionary Dictionary { get; set; }
    public virtual Culture Culture { get; set; }
  }
}