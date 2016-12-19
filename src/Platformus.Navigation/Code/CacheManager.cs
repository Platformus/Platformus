// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;
using Platformus.Navigation.Data.Abstractions;
using Platformus.Navigation.Data.Models;

namespace Platformus.Navigation
{
  public class CacheManager
  {
    public IHandler handler;

    public CacheManager(IHandler handler)
    {
      this.handler = handler;
    }

    public void CacheMenu(Menu menu)
    {
      foreach (Culture culture in this.handler.Storage.GetRepository<ICultureRepository>().NotNeutral())
      {
        CachedMenu cachedMenu = this.handler.Storage.GetRepository<ICachedMenuRepository>().WithKey(culture.Id, menu.Id);

        if (cachedMenu == null)
          this.handler.Storage.GetRepository<ICachedMenuRepository>().Create(this.CacheMenu(culture, menu));

        else
        {
          CachedMenu temp = this.CacheMenu(culture, menu);

          cachedMenu.Code = temp.Code;
          cachedMenu.CachedMenuItems = temp.CachedMenuItems;
          this.handler.Storage.GetRepository<ICachedMenuRepository>().Edit(cachedMenu);
        }
      }

      this.handler.Storage.Save();
    }

    private CachedMenu CacheMenu(Culture culture, Menu menu)
    {
      List<CachedMenuItem> cachedMenuItems = new List<CachedMenuItem>();

      foreach (MenuItem menuItem in this.handler.Storage.GetRepository<IMenuItemRepository>().FilteredByMenuId(menu.Id))
        cachedMenuItems.Add(this.CacheMenuItem(culture, menuItem));

      CachedMenu cachedForm = new CachedMenu();

      cachedForm.MenuId = menu.Id;
      cachedForm.CultureId = culture.Id;
      cachedForm.Code = menu.Code;

      if (cachedMenuItems.Count != 0)
        cachedForm.CachedMenuItems = this.SerializeObject(cachedMenuItems);

      return cachedForm;
    }

    private CachedMenuItem CacheMenuItem(Culture culture, MenuItem menuItem)
    {
      List<CachedMenuItem> cachedChildMenuItems = new List<CachedMenuItem>();

      foreach (MenuItem childMenuItem in this.handler.Storage.GetRepository<IMenuItemRepository>().FilteredByMenuItemId(menuItem.Id))
        cachedChildMenuItems.Add(this.CacheMenuItem(culture, childMenuItem));

      CachedMenuItem cachedMenuItem = new CachedMenuItem();

      cachedMenuItem.MenuItemId = menuItem.Id;
      cachedMenuItem.Name = this.GetLocalizationValue(culture.Id, menuItem.NameId);
      cachedMenuItem.Url = menuItem.Url;
      cachedMenuItem.Position = menuItem.Position;

      if (cachedChildMenuItems.Count != 0)
        cachedMenuItem.CachedMenuItems = this.SerializeObject(cachedChildMenuItems);

      return cachedMenuItem;
    }

    private string GetLocalizationValue(int cultureId, int dictionaryId)
    {
      Localization localization = this.handler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId(dictionaryId, cultureId);

      if (localization == null)
        return null;

      return localization.Value;
    }

    private string SerializeObject(object value)
    {
      string result = JsonConvert.SerializeObject(value);

      if (string.IsNullOrEmpty(result))
        return null;

      return result;
    }
  }
}