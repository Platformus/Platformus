// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class PhotoViewModel : ViewModelBase
  {
    public string Filename { get; set; }
    public bool IsCover { get; set; }
    public int? Position { get; set; }
  }
}