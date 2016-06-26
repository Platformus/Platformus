// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Infrastructure
{
  public class BackendMenuGroup
  {
    public string Name { get; set; }
    public int Position { get; set; }
    public IEnumerable<BackendMenuItem> BackendMenuItems { get; set; }

    public BackendMenuGroup(string name, int position, IEnumerable<BackendMenuItem> backendMenuItems)
    {
      this.Name = name;
      this.Position = position;
      this.BackendMenuItems = backendMenuItems;
    }
  }
}