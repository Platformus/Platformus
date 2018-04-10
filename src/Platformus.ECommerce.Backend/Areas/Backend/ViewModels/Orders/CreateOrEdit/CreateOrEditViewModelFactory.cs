// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Primitives;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Orders
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
        };

      Order order = this.RequestHandler.Storage.GetRepository<IOrderRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = order.Id,
        OrderStateOptions = this.GetOrderStateOptions(),
        PaymentMethodOptions = this.GetPaymentMethodOptions(),
        DeliveryMethodOptions = this.GetDeliveryMethodOptions(),
        CustomerFirstName = order.CustomerFirstName,
        CustomerLastName = order.CustomerLastName,
        CustomerPhone = order.CustomerPhone,
        CustomerEmail = order.CustomerEmail,
        CustomerAddress = order.CustomerAddress,
        Note = order.Note
      };
    }

    private IEnumerable<Option> GetOrderStateOptions()
    {
      return this.RequestHandler.Storage.GetRepository<IOrderStateRepository>().All().Select(
        os => new Option(this.GetLocalizationValue(os.NameId), os.Id.ToString())
      );
    }

    private IEnumerable<Option> GetPaymentMethodOptions()
    {
      return this.RequestHandler.Storage.GetRepository<IPaymentMethodRepository>().All().Select(
        pm => new Option(this.GetLocalizationValue(pm.NameId), pm.Id.ToString())
      );
    }

    private IEnumerable<Option> GetDeliveryMethodOptions()
    {
      return this.RequestHandler.Storage.GetRepository<IDeliveryMethodRepository>().All().Select(
        dm => new Option(this.GetLocalizationValue(dm.NameId), dm.Id.ToString())
      );
    }
  }
}