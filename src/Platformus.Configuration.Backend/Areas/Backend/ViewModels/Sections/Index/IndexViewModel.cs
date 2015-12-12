// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configuration.Backend.ViewModels.Shared;

namespace Platformus.Configuration.Backend.ViewModels.Sections
{
  public class IndexViewModel : ViewModelBase
  {
    public IEnumerable<SectionViewModel> Sections { get; set; }
  }
}