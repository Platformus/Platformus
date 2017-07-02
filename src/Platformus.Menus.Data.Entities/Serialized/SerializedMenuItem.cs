// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Menus.Data.Entities
{
  public class SerializedMenuItem : IEntity
  {
    public int MenuItemId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int? Position { get; set; }
    public string SerializedMenuItems { get; set; }
  }
}