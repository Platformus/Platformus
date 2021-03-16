﻿// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class PaymentMethodViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}