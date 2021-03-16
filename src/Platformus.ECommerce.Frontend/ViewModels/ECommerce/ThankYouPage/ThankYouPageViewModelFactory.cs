// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Frontend.ViewModels;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Frontend.ViewModels.Shared;

namespace Platformus.ECommerce.Frontend.ViewModels.ECommerce
{
  public class ThankYouPageViewModelFactory : ViewModelFactoryBase
  {
    public ThankYouPageViewModel Create(Order order)
    {
      return new ThankYouPageViewModel()
      {
        Order = new OrderViewModelFactory().Create(order)
      };
    }
  }
}