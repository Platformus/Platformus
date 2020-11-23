﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public class TakeSelectorViewModel : ViewModelBase
  {
    public int Take { get; set; }
    public IEnumerable<Option> TakeOptions { get; set; }
  }
}