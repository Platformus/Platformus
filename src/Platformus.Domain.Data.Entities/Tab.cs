// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents a tab. The tabs are used to group the members.
  /// </summary>
  public class Tab : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the tab.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the class identifier this tab belongs to.
    /// </summary>
    public int ClassId { get; set; }

    /// <summary>
    /// Gets or sets the tab name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the tab position. Position is used to sort the tabs inside the class (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Class Class { get; set; }
  }
}