// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus
{
  public class SerializationManager
  {
    public IRequestHandler requestHandler;

    public SerializationManager(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
    }

    public void SerializeMenu(Menu menu)
    {
      foreach (Culture culture in this.requestHandler.Storage.GetRepository<ICultureRepository>().NotNeutral().ToList())
      {
        SerializedMenu serializedMenu = this.requestHandler.Storage.GetRepository<ISerializedMenuRepository>().WithKey(culture.Id, menu.Id);

        if (serializedMenu == null)
          this.requestHandler.Storage.GetRepository<ISerializedMenuRepository>().Create(this.SerializeMenu(culture, menu));

        else
        {
          SerializedMenu temp = this.SerializeMenu(culture, menu);

          serializedMenu.Code = temp.Code;
          serializedMenu.SerializedMenuItems = temp.SerializedMenuItems;
          this.requestHandler.Storage.GetRepository<ISerializedMenuRepository>().Edit(serializedMenu);
        }
      }

      this.requestHandler.Storage.Save();
    }

    private SerializedMenu SerializeMenu(Culture culture, Menu menu)
    {
      List<SerializedMenuItem> serializedMenuItems = new List<SerializedMenuItem>();

      foreach (MenuItem menuItem in this.requestHandler.Storage.GetRepository<IMenuItemRepository>().FilteredByMenuId(menu.Id).ToList())
        serializedMenuItems.Add(this.SerializeMenuItem(culture, menuItem));

      SerializedMenu serializedForm = new SerializedMenu();

      serializedForm.MenuId = menu.Id;
      serializedForm.CultureId = culture.Id;
      serializedForm.Code = menu.Code;

      if (serializedMenuItems.Count != 0)
        serializedForm.SerializedMenuItems = this.SerializeObject(serializedMenuItems);

      return serializedForm;
    }

    private SerializedMenuItem SerializeMenuItem(Culture culture, MenuItem menuItem)
    {
      List<SerializedMenuItem> serializedChildMenuItems = new List<SerializedMenuItem>();

      foreach (MenuItem childMenuItem in this.requestHandler.Storage.GetRepository<IMenuItemRepository>().FilteredByMenuItemId(menuItem.Id).ToList())
        serializedChildMenuItems.Add(this.SerializeMenuItem(culture, childMenuItem));

      SerializedMenuItem serializedMenuItem = new SerializedMenuItem();

      serializedMenuItem.MenuItemId = menuItem.Id;
      serializedMenuItem.Name = this.requestHandler.GetLocalizationValue(menuItem.NameId, culture.Id);
      serializedMenuItem.Url = menuItem.Url;
      serializedMenuItem.Position = menuItem.Position;

      if (serializedChildMenuItems.Count != 0)
        serializedMenuItem.SerializedMenuItems = this.SerializeObject(serializedChildMenuItems);

      return serializedMenuItem;
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