// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Barebone.Backend.Metadata
{
  public class MenuGroup
  {
    public string Name { get; set; }
    public int Position { get; set; }
    public IEnumerable<MenuItem> MenuItems { get; set; }

    public MenuGroup(string name, int position, IEnumerable<MenuItem> MenuItems)
    {
      this.Name = name;
      this.Position = position;
      this.MenuItems = MenuItems;
    }
  }
}