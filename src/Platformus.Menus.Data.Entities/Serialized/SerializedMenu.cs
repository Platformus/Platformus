// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Menus.Data.Entities
{
  /// <summary>
  /// Represents a serialized menu. The serialized menus contain the menu and corresponding menu items data
  /// inside the single entity. This reduces the number of storage read operations while menu retrieval.
  /// </summary>
  public class SerializedMenu : IEntity
  {
    /// <summary>
    /// Gets or sets the culture identifier this serialized menu belongs to.
    /// </summary>
    public int CultureId { get; set; }

    /// <summary>
    /// Gets or sets the menu identifier this serialized menu belongs to.
    /// </summary>
    public int MenuId { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the menu. It is set by the user and might be used for the menu retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the menu items serialized into a string.
    /// </summary>
    public string SerializedMenuItems { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Menu Menu { get; set; }
  }
}