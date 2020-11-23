// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Platformus.Core.Backend.Metadata.Providers
{
  public class DefaultMenuGroupsProvider : IMenuGroupsProvider
  {
    public IEnumerable<MenuGroup> GetMenuGroups(HttpContext httpContext)
    {
      List<MenuGroup> menuGroups = new List<MenuGroup>();

      foreach (IMetadata metadata in ExtensionManager.GetInstances<IMetadata>())
      {
        foreach (MenuGroup menuGroup in metadata.GetMenuGroups(httpContext))
        {
          List<MenuItem> menuItems = new List<MenuItem>();

          foreach (MenuItem menuItem in menuGroup.MenuItems)
            if (httpContext.User.Claims.Any(c => menuItem.PermissionCodes.Any(pc => string.Equals(c.Value, pc, StringComparison.OrdinalIgnoreCase)) || string.Equals(c.Value, Permissions.DoAnything, StringComparison.OrdinalIgnoreCase)))
              menuItems.Add(menuItem);

          MenuGroup result = this.GetMenuGroup(menuGroups, menuGroup);

          if (result == null)
          {
            result = new MenuGroup(menuGroup.Name, menuGroup.Position, null);
            menuGroups.Add(result);
          }

          else menuItems.AddRange(result.MenuItems);

          result.MenuItems = menuItems.OrderBy(mi => mi.Position);
        }
      }

      return menuGroups.Where(mg => mg.MenuItems.Count() != 0).OrderBy(mg => mg.Position);
    }

    private MenuGroup GetMenuGroup(List<MenuGroup> menuGroups, MenuGroup menuGroup)
    {
      return menuGroups.FirstOrDefault(mg => string.Equals(mg.Name, menuGroup.Name, StringComparison.OrdinalIgnoreCase));
    }
  }
}