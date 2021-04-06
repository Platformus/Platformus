// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Orders
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public async Task<IndexViewModel> CreateAsync(HttpContext httpContext, OrderFilter filter, IEnumerable<Order> orders, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext,
          new[] {
            new FilterViewModelFactory().Create(httpContext, "OrderState.Id.Equals", localizer["Order state"], await this.GetOrderStateOptionsAsync(httpContext)),
            new FilterViewModelFactory().Create(httpContext, "DeliveryMethod.Id.Equals", localizer["Delivery method"], await this.GetDeliveryMethodOptionsAsync(httpContext)),
            new FilterViewModelFactory().Create(httpContext, "PaymentMethod.Id.Equals", localizer["Payment method"], await this.GetPaymentMethodOptionsAsync(httpContext)),
            new FilterViewModelFactory().Create(httpContext, "Customer.Phone.Contains", localizer["Customer phone"])
          },
          orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["#"]),
            new GridColumnViewModelFactory().Create(localizer["Order state"]),
            new GridColumnViewModelFactory().Create(localizer["Delivery method"]),
            new GridColumnViewModelFactory().Create(localizer["Payment method"]),
            new GridColumnViewModelFactory().Create(localizer["Customer"]),
            new GridColumnViewModelFactory().Create(localizer["Total"]),
            new GridColumnViewModelFactory().Create(localizer["Created"], "Created"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          orders.Select(o => new OrderViewModelFactory().Create(o)),
          "_Order"
        )
      };
    }

    private async Task<IEnumerable<Option>> GetOrderStateOptionsAsync(HttpContext httpContext)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();
      List<Option> options = new List<Option>();

      options.Add(new Option(localizer["All order states"], string.Empty));
      options.AddRange(
        (await httpContext.GetStorage().GetRepository<int, OrderState, OrderStateFilter>().GetAllAsync(inclusions: new Inclusion<OrderState>(os => os.Name.Localizations))).Select(
          os => new Option(os.Name.GetLocalizationValue(), os.Id.ToString())
        )
      );

      return options;
    }

    private async Task<IEnumerable<Option>> GetDeliveryMethodOptionsAsync(HttpContext httpContext)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();
      List<Option> options = new List<Option>();

      options.Add(new Option(localizer["All delivery methods"], string.Empty));
      options.AddRange(
        (await httpContext.GetStorage().GetRepository<int, DeliveryMethod, DeliveryMethodFilter>().GetAllAsync(inclusions: new Inclusion<DeliveryMethod>(dm => dm.Name.Localizations))).Select(
          dm => new Option(dm.Name.GetLocalizationValue(), dm.Id.ToString())
        )
      );

      return options;
    }

    private async Task<IEnumerable<Option>> GetPaymentMethodOptionsAsync(HttpContext httpContext)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();
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
}