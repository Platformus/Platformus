// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Objects
{
  public class IndexViewModel : ViewModelBase
  {
    public ClassViewModel Class { get; set; }
    public IDictionary<ClassViewModel, IEnumerable<ClassViewModel>> ClassesByAbstractClasses { get; set; }
    public GridViewModel Grid { get; set; }
  }
}