// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class PositionViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public ProductViewModel Product { get; set; }
  }
}