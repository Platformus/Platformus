// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.PaymentMethods
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(string sorting, int offset, int limit, int total, IEnumerable<PaymentMethod> paymentMethods)
    {
      return new IndexViewModel()
      {
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total,
        PaymentMethods = paymentMethods.Select(PaymentMethodViewModelFactory.Create).ToList()
      };
    }
  }
}