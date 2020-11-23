﻿// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class PhotoViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Filename { get; set; }
    public bool IsCover { get; set; }
    public int? Position { get; set; }
  }
}