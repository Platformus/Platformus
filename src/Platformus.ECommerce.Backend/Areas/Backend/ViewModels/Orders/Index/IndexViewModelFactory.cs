// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Primitives;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Orders;

public static class IndexViewModelFactory
{
  public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, string sorting, int offset, int limit, int total, IEnumerable<Order> orders)
  {
    return new IndexViewModel()
    {
      OrderStateOptions = await GetOrderStateOptionsAsync(httpContext),
      DeliveryMethodOptions = await GetDeliveryMethodOptionsAsync(httpContext),
      PaymentMethodOptions = await GetPaymentMethodOptionsAsync(httpContext),
      Sorting = sorting,
      Offset = offset,
      Limit = limit,
      Total = total,
      Orders = orders.Select(OrderViewModelFactory.Create).ToList()
    };
  }

  private static async Task<IEnumerable<Option>> GetOrderStateOptionsAsync(HttpContext httpContext)
  {
    IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();
    List<Option> options = new List<Option>();

    options.Add(new Option(localizer["All order states"], string.Empty));
    options.AddRange(
      (await httpContext.GetStorage().GetRepository<int, OrderState, OrderStateFilter>().GetAllAsync(inclusions: new Inclusion<OrderState>(os => os.Name.Localizations))).Select(
        os => new Option(os.Name.GetLocalizationValue(), os.Id.ToString())
      )
    );

    return options;
  }

  private static async Task<IEnumerable<Option>> GetDeliveryMethodOptionsAsync(HttpContext httpContext)
  {
    IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();
    List<Option> options = new List<Option>();

    options.Add(new Option(localizer["All delivery methods"], string.Empty));
    options.AddRange(
      (await httpContext.GetStorage().GetRepository<int, DeliveryMethod, DeliveryMethodFilter>().GetAllAsync(inclusions: new Inclusion<DeliveryMethod>(dm => dm.Name.Localizations))).Select(
        dm => new Option(dm.Name.GetLocalizationValue(), dm.Id.ToString())
      )
    );

    return options;
  }

  private static async Task<IEnumerable<Option>> GetPaymentMethodOptionsAsync(HttpContext httpContext)
  {
    IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();
    List<Option> options = new List<Option>();

    options.Add(new Option(localizer["All payment methods"], string.Empty));
    options.AddRange(
      (await httpContext.GetStorage().GetRepository<int, PaymentMethod, PaymentMethodFilter>().GetAllAsync(inclusions: new Inclusion<PaymentMethod>(pm => pm.Name.Localizations))).Select(
        pm => new Option(pm.Name.GetLocalizationValue(), pm.Id.ToString())
      )
    );

    return options;
  }
}