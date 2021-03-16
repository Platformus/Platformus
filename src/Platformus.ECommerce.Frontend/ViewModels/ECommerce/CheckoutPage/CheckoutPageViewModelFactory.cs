// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Extensions;
using Platformus.Core.Frontend.ViewModels;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;
using Platformus.ECommerce.Frontend.ViewModels.Shared;

namespace Platformus.ECommerce.Frontend.ViewModels.ECommerce
{
  public class CheckoutPageViewModelFactory : ViewModelFactoryBase
  {
    public async Task<CheckoutPageViewModel> CreateAsync(HttpContext httpContext)
    {
      return new CheckoutPageViewModel()
      {
        PaymentMethodId = 1,
        PaymentMethods = await this.GetPaymentMethodsAsync(httpContext),
        DeliveryMethodId = 1,
        DeliveryMethods = await this.GetDeliveryMethodsAsync(httpContext)
      };
    }

    private async Task<IEnumerable<PaymentMethodViewModel>> GetPaymentMethodsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, PaymentMethod, PaymentMethodFilter>().GetAllAsync(inclusions: new Inclusion<PaymentMethod>(pm => pm.Name.Localizations))).Select(
        pm => new PaymentMethodViewModelFactory().Create(pm)
      );
    }

    private async Task<IEnumerable<DeliveryMethodViewModel>> GetDeliveryMethodsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, DeliveryMethod, DeliveryMethodFilter>().GetAllAsync(inclusions: new Inclusion<DeliveryMethod>(dm => dm.Name.Localizations))).Select(
        dm => new DeliveryMethodViewModelFactory().Create(dm)
      );
    }
  }
}