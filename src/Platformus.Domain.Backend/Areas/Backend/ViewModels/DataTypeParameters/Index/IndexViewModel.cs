// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataTypeParameters
{
  public class IndexViewModel : ViewModelBase
  {
    public int DataTypeId { get; set; }
    public GridViewModel Grid { get; set; }
  }
}