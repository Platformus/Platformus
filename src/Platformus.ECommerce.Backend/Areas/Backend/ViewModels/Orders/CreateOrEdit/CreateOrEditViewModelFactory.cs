// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Primitives;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Orders
{
  public static class CreateOrEditViewModelFactory
  {
    public static async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Order order)
    {
      if (order == null)
        return new CreateOrEditViewModel()
        {
          OrderStateOptions = await GetOrderStateOptionsAsync(httpContext),
          DeliveryMethodOptions = await GetDeliveryMethodOptionsAsync(httpContext),
          PaymentMethodOptions = await GetPaymentMethodOptionsAsync(httpContext),
          Positions = Array.Empty<PositionViewModel>()
        };

      return new CreateOrEditViewModel()
      {
        Id = order.Id,
        OrderStateId = order.OrderStateId,
        OrderStateOptions = await GetOrderStateOptionsAsync(httpContext),
        DeliveryMethodId = order.DeliveryMethodId,
        DeliveryMethodOptions = await GetDeliveryMethodOptionsAsync(httpContext),
        PaymentMethodId = order.PaymentMethodId,
        PaymentMethodOptions = await GetPaymentMethodOptionsAsync(httpContext),
        CustomerFirstName = order.CustomerFirstName,
        CustomerLastName = order.CustomerLastName,
        CustomerPhone = order.CustomerPhone,
        CustomerEmail = order.CustomerEmail,
        CustomerAddress = order.CustomerAddress,
        Note = order.Note,
        Positions = order.Positions.Select(PositionViewModelFactory.Create).ToList()
      };
    }

    private static async Task<IEnumerable<Option>> GetOrderStateOptionsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, OrderState, OrderStateFilter>().GetAllAsync(inclusions: new Inclusion<OrderState>(os => os.Name.Localizations))).Select(
        os => new Option(os.Name.GetLocalizationValue(), os.Id.ToString())
      ).ToList();
    }

    private static async Task<IEnumerable<Option>> GetDeliveryMethodOptionsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, DeliveryMethod, DeliveryMethodFilter>().GetAllAsync(inclusions: new Inclusion<DeliveryMethod>(dm => dm.Name.Localizations))).Select(
        dm => new Option(dm.Name.GetLocalizationValue(), dm.Id.ToString())
      ).ToList();
    }

    private static async Task<IEnumerable<Option>> GetPaymentMethodOptionsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, PaymentMethod, PaymentMethodFilter>().GetAllAsync(inclusions: new Inclusion<PaymentMethod>(pm => pm.Name.Localizations))).Select(
        pm => new Option(pm.Name.GetLocalizationValue(), pm.Id.ToString())
      ).ToList();
    }
  }
}