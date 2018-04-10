// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Events;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;

namespace Platformus.ECommerce
{
  public class CartManager
  {
    private IRequestHandler requestHandler;
    private ICartRepository cartRepository;
    private IPositionRepository positionRepository;

    public string ClientSideId
    {
      get
      {
        return this.requestHandler.HttpContext.Request.Cookies["cart"];
      }
    }

    public Cart Cart
    {
      get
      {
        Cart cart = null;

        if (string.IsNullOrEmpty(this.ClientSideId))
        {
          cart = new Cart();
          cart.ClientSideId = Guid.NewGuid().ToString();
          cart.Created = DateTime.Now;
          this.cartRepository.Create(cart);
          this.requestHandler.Storage.Save();
          this.requestHandler.HttpContext.Response.Cookies.Append("cart", cart.ClientSideId);
        }

        else cart = this.cartRepository.WithClientSideId(this.ClientSideId);

        return cart;
      }
    }

    public IEnumerable<Position> Positions
    {
      get
      {
        return this.positionRepository.FilteredByCartId(this.Cart.Id).ToList();
      }
    }

    public CartManager(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
      this.cartRepository = this.requestHandler.Storage.GetRepository<ICartRepository>();
      this.positionRepository = this.requestHandler.Storage.GetRepository<IPositionRepository>();
    }

    public void Add(int productId)
    {
      Position position = new Position();

      position.CartId = this.Cart.Id;
      position.ProductId = productId;
      this.positionRepository.Create(position);
      this.requestHandler.Storage.Save();
      Event<IPositionCreatedEventHandler, IRequestHandler, Position>.Broadcast(this.requestHandler, position);
    }

    public void Remove(int productId)
    {
      //Event<IPositionDeletedEventHandler, IRequestHandler, Position>.Broadcast(this.requestHandler, position);
    }

    public void AssignTo(Order order)
    {
      Cart cart = this.Cart;

      cart.OrderId = order.Id;
      this.cartRepository.Edit(cart);
      this.requestHandler.Storage.Save();
      this.requestHandler.HttpContext.Response.Cookies.Delete("cart");
    }
  }
}