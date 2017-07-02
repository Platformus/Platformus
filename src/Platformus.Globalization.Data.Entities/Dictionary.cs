// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Globalization.Data.Entities
{
  public class Dictionary : IEntity
  {
    public int Id { get; set; }

    public virtual ICollection<Localization> Localizations { get; set; }
  }
}