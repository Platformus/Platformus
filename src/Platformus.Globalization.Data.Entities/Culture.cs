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
    /// <summary>
    /// Gets or sets the unique identifier of the culture.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the culture. It is set by the user and might be used for the culture retrieval.
    /// Only standard two letter culture codes are allowed.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the culture name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the culture is neutral or not. Neutral culture is used to store
    /// not localizable property values.
    /// </summary>
    public bool IsNeutral { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the culture is frontend default or not. Frontend default culture is used
    /// to display localizable content on the frontend when no culture is explicitly selected and to display frontend UI.
    /// </summary>
    public bool IsFrontendDefault { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the culture is backend default or not. Backend default culture is used
    /// to display localizable content on the backend when no culture is explicitly selected and to display backend UI.
    /// </summary>
    public bool IsBackendDefault { get; set; }

    public virtual ICollection<Localization> Localizations { get; set; }
  }
}