// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Platformus.Website.Domain.Models;
using Platformus.Website.Domain.Services.Extensions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Services;

public class MenuItemService : Service<int, Data.Entities.MenuItem, MenuItem, MenuItemFilter>
{
  public MenuItemService(DbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
  {
  }

  public override async Task EditAsync(MenuItem menuItem)
  {
    IEnumerable<LocalizedMenuItem>? localizedMenuItems = menuItem.LocalizedMenuItems?.ToList();

    menuItem.LocalizedMenuItems = null;

    await base.EditAsync(menuItem);

    if (localizedMenuItems != null)
    {
      foreach (LocalizedMenuItem localizedMenuItem in localizedMenuItems)
        localizedMenuItem.MenuItem = menuItem;

      await this.MergeLocalizedMenuItemsAsync(localizedMenuItems!);
    } 
  }

  public async Task MergeLocalizedMenuItemsAsync(IEnumerable<LocalizedMenuItem> localizedMenuItems)
  {
    IEnumerable<SqlParameter> parameters =
    [
      new SqlParameter("@LocalizedMenuItems", localizedMenuItems.Select(lmi => lmi.ToEntity()).ToDataTable()) { TypeName = "LocalizedMenuItemsType" }
    ];

    await this.dbContext.Database.ExecuteSqlRawAsync("MergeLocalizedMenuItems @LocalizedMenuItems", parameters);
  }
}