// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Carts
{
  public class DetailsViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string ClientSideId { get; set; }
    public CartViewModel Cart { get; set; }
  }
}