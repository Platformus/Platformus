// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Frontend.Controllers;

public class CartController : Core.Frontend.Controllers.ControllerBase
{
  private const string cartId = "cart_id";

  private IRepository<int, Product, ProductFilter> ProductRepository
  {
    get => this.Storage.GetRepository<int, Product, ProductFilter>();
  }

  private IRepository<int, Position, PositionFilter> PositionRepository
  {
    get => this.Storage.GetRepository<int, Position, PositionFilter>();
  }

  private IRepository<int, Cart, CartFilter> CartRepository
  {
    get => this.Storage.GetRepository<int, Cart, CartFilter>();
  }

  public CartController(IStorage storage) : base(storage)
  {
  }

  [HttpPost]
  public async Task<IActionResult> AddToCartAsync(int productId)
  {
    Cart cart = await this.GetOrCreateCartAsync();
    Product product = await this.ProductRepository.GetByIdAsync(productId);
    Position position = new Position();

    position.CartId = cart.Id;
    position.ProductId = productId;
    position.Price = product.Price;
    position.Quantity = 1m;
    this.PositionRepository.Create(position);
    await this.Storage.SaveAsync();
    return this.Ok();
  }

  [HttpPost]
  public async Task<IActionResult> RemoveFromCartAsync(int positionId)
  {
    if (this.HttpContext.GetCartManager().TryGetClientSideId(out Guid clientSideId))
    {
      if (await this.PositionRepository.CountAsync(new PositionFilter(id: positionId, cart: new CartFilter(clientSideId: clientSideId))) != 0)
      {
        this.PositionRepository.Delete(positionId);
        await this.Storage.SaveAsync();

        if (await this.PositionRepository.CountAsync(new PositionFilter(cart: new CartFilter(clientSideId: clientSideId))) == 0)
        {
          Cart cart = (await this.CartRepository.GetAllAsync(new CartFilter(clientSideId: clientSideId))).FirstOrDefault();

          this.CartRepository.Delete(cart.Id);
          await this.Storage.SaveAsync();
          this.Response.Cookies.Delete(cartId);
        }

        return this.Ok();
      }
    }

    return this.Forbid();
  }

  private async Task<Cart> GetOrCreateCartAsync()
  {
    if (this.HttpContext.GetCartManager().TryGetClientSideId(out Guid clientSideId))
    {
      Cart cart = (await this.CartRepository.GetAllAsync(new CartFilter(clientSideId: clientSideId))).FirstOrDefault();

      if (cart != null)
        return cart;
    }

    {
      Cart cart = new Cart();

      cart.ClientSideId = Guid.NewGuid();
      cart.Created = DateTime.Now.ToUniversalTime();
      this.CartRepository.Create(cart);
      await this.Storage.SaveAsync();
      this.Response.Cookies.Append(cartId, cart.ClientSideId.ToString());
      return cart;
    }
  }
}