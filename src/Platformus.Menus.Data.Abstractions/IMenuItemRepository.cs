// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Menus.Data.Models;

namespace Platformus.Menus.Data.Abstractions
{
  public interface IMenuItemRepository : IRepository
  {
    MenuItem WithKey(int id);
    IEnumerable<MenuItem> FilteredByMenuId(int menuId);
    IEnumerable<MenuItem> FilteredByMenuItemId(int menuItemId);
    void Create(MenuItem menuItem);
    void Edit(MenuItem menuItem);
    void Delete(int id);
    void Delete(MenuItem menuItem);
  }
}