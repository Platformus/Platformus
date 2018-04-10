// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseCartsPermission)]
  public class PositionsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public PositionsController(IStorage storage)
      : base(storage)
    {
    }

    [HttpPost]
    public IActionResult Create(int cartId, int productId)
    {
      Position position = new Position();

      position.CartId = cartId;
      position.ProductId = productId;
      this.Storage.GetRepository<IPositionRepository>().Create(position);
      this.Storage.Save();
      return this.Ok();
    }

    public IActionResult Delete(int id)
    {
      Position position = this.Storage.GetRepository<IPositionRepository>().WithKey(id);

      this.Storage.GetRepository<IPositionRepository>().Delete(position);
      this.Storage.Save();

      Cart cart = this.Storage.GetRepository<ICartRepository>().WithKey(position.CartId);

      if (cart.OrderId == null)
        return this.RedirectToAction("Details", "Carts", new { id = cart.Id });

      return this.RedirectToAction("Details", "Orders", new { id = cart.OrderId });
    }
  }
}