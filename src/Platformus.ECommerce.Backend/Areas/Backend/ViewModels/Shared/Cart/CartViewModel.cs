// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Platformus.Core.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class CartViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public decimal Total { get; set; }
    public DateTime Created { get; set; }
  }
}