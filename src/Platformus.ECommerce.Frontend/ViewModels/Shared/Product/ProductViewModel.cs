// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class ProductViewModel : ViewModelBase
  {
    public string Url { get; set; }
    public string Name { get; set; }
    public PhotoViewModel Cover { get; set; }
  }
}