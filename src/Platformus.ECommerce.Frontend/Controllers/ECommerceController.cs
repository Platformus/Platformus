// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Frontend;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;
using Platformus.ECommerce.Frontend.ViewModels.ECommerce;

namespace Platformus.ECommerce.Frontend.Controllers
{
  public class ECommerceController : Core.Frontend.Controllers.ControllerBase
  {
    private const string cartId = "cart_id";

    private IRepository<int, Position, PositionFilter> PositionRepository
    {
      get => this.Storage.GetRepository<int, Position, PositionFilter>();
    }

    private IRepository<int, Cart, CartFilter> CartRepository
    {
      get => this.Storage.GetRepository<int, Cart, CartFilter>();
    }

    private IRepository<int, Order, OrderFilter> OrderRepository
    {
      get => this.Storage.GetRepository<int, Order, OrderFilter>();
    }

    public ECommerceController(IStorage storage) : base(storage)
    {
    }

    [HttpGet]
    public async Task<IActionResult> CheckoutAsync()
    {
      if (this.HttpContext.GetCartManager().IsEmpty)
        return this.Redirect(GlobalizedUrlFormatter.Format(this.HttpContext, "/"));

      return this.View(await CheckoutPageViewModelFactory.CreateAsync(this.HttpContext));
    }

    [HttpPost]
    public async Task<IActionResult> CheckoutAsync(CheckoutPageViewModel checkoutPageViewModel)
    {
      if (this.ModelState.IsValid)
      {
        Order order = new Order();

        order.OrderStateId = 1;
        order.DeliveryMethodId = checkoutPageViewModel.DeliveryMethodId;
        order.PaymentMethodId = checkoutPageViewModel.PaymentMethodId;
        order.CustomerFirstName = checkoutPageViewModel.FirstName;
        order.CustomerLastName = checkoutPageViewModel.LastName;
        order.CustomerPhone = checkoutPageViewModel.Phone;
        order.CustomerEmail = checkoutPageViewModel.Email;
        order.CustomerAddress = checkoutPageViewModel.Address;
        order.Note = checkoutPageViewModel.Note;
        order.Created = DateTime.Now.ToUniversalTime();
        this.OrderRepository.Create(order);
        await this.Storage.SaveAsync();

        if (this.HttpContext.GetCartManager().TryGetClientSideId(out Guid clientSideId))
        {
          Cart cart = (await this.CartRepository.GetAllAsync(
            new CartFilter(clientSideId: clientSideId),
            inclusions: new Inclusion<Cart>[] {
              new Inclusion<Cart>(c => c.Positions)
            }
          )).FirstOrDefault();

          if (cart != null)
          {
            foreach (Position position in cart.Positions)
            {
              position.OrderId = order.Id;
              this.PositionRepository.Edit(position);
            }

            await this.Storage.SaveAsync();
          }

          this.Response.Cookies.Delete(cartId);
        }

        return this.Redirect(GlobalizedUrlFormatter.Format(this.HttpContext, $"/ecommerce/thank-you/{order.Id}"));
      }

      return this.View();
    }

    [HttpGet]
    public async Task<IActionResult> ThankYouAsync(int orderId)
    {
      return this.View(ThankYouPageViewModelFactory.Create(
        await this.OrderRepository.GetByIdAsync(
          orderId,
          new Inclusion<Order>(o => o.OrderState.Name.Localizations),
          new Inclusion<Order>(o => o.DeliveryMethod.Name.Localizations),
          new Inclusion<Order>(o => o.PaymentMethod.Name.Localizations),
          new Inclusion<Order>("Positions.Product.Category.Name.Localizations"),
          new Inclusion<Order>("Positions.Product.Name.Localizations")
        )
      ));
    }
  }
}