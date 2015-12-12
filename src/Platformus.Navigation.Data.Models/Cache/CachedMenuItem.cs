// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Navigation.Data.Models
{
  public class CachedMenuItem : IEntity
  {
    public int MenuItemId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int? Position { get; set; }
    public string CachedMenuItems { get; set; }
  }
}