﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Core.Backend.Metadata;

public class MenuItem
{
  public string CssClass { get; set; }
  public string Url { get; set; }
  public string Name { get; }
  public int Position { get; set; }
  public IEnumerable<string> PermissionCodes { get; set; }

  public MenuItem(string cssClass, string url, string name, int position, params string[] permissionCodes)
  {
    this.CssClass = cssClass;
    this.Url = url;
    this.Name = name;
    this.Position = position;
    this.PermissionCodes = permissionCodes;
  }
}