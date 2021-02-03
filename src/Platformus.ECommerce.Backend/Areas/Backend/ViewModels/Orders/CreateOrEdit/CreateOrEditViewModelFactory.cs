// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Orders
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Order order)
    {
      if (order == null)
        return new CreateOrEditViewModel()
        {
          OrderStateOptions = await this.GetOrderStateOptionsAsync(httpContext),
          DeliveryMethodOptions = await this.GetDeliveryMethodOptionsAsync(httpContext),
          PaymentMethodOptions = await this.GetPaymentMethodOptionsAsync(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = order.Id,
        OrderStateId = order.OrderStateId,
        OrderStateOptions = await this.GetOrderStateOptionsAsync(httpContext),
        DeliveryMethodId = order.DeliveryMethodId,
        DeliveryMethodOptions = await this.GetDeliveryMethodOptionsAsync(httpContext),
        PaymentMethodId = order.PaymentMethodId,
        PaymentMethodOptions = await this.GetPaymentMethodOptionsAsync(httpContext),
        CustomerFirstName = order.CustomerFirstName,
        CustomerLastName = order.CustomerLastName,
        CustomerPhone = order.CustomerPhone,
        CustomerEmail = order.CustomerEmail,
        CustomerAddress = order.CustomerAddress,
        Note = order.Note,
        Positions = order.Positions.Select(p => new PositionViewModelFactory().Create(p)),
        Total = order.GetTotal()
      };
    }

    private async Task<IEnumerable<Option>> GetOrderStateOptionsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, OrderState, OrderStateFilter>().GetAllAsync(inclusions: new Inclusion<OrderState>(os => os.Name.Localizations))).Select(
        os => new Option(os.Name.GetLocalizationValue(), os.Id.ToString())
      );
    }

    private async Task<IEnumerable<Option>> GetDeliveryMethodOptionsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, DeliveryMethod, DeliveryMethodFilter>().GetAllAsync(inclusions: new Inclusion<DeliveryMethod>(dm => dm.Name.Localizations))).Select(
        dm => new Option(dm.Name.GetLocalizationValue(), dm.Id.ToString())
      );
    }

    private async Task<IEnumerable<Option>> GetPaymentMethodOptionsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, PaymentMethod, PaymentMethodFilter>().GetAllAsync(inclusions: new Inclusion<PaymentMethod>(pm => pm.Name.Localizations))).Select(
        pm => new Option(pm.Name.GetLocalizationValue(), pm.Id.ToString())
      );
    }
  }
}