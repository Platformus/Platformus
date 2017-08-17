// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Globalization.Data.Entities
{
  /// <summary>
  /// Represents a culture. The culture is the central unit in the Platformus globalization mechanism.
  /// </summary>
  public class Culture : IEntity
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsNeutral { get; set; }
    public bool IsDefault { get; set; }

    public virtual ICollection<Localization> Localizations { get; set; }
  }
}