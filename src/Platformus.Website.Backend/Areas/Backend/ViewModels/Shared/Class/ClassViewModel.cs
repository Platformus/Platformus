﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class ClassViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public ClassViewModel Parent { get; set; }
    public string Name { get; set; }
    public string PluralizedName { get; set; }
    public bool IsAbstract { get; set; }
  }
}