// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Models.Abstractions;

namespace Platformus.Globalization.Data.Models
{
  public class Dictionary : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }

    public virtual ICollection<Localization> Localizations { get; set; }
  }
}