// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Website.Backend.ViewModels.Shared;

namespace Platformus.Website.Backend.ViewModels.Objects
{
  public class IndexViewModel : ViewModelBase
  {
    public ClassViewModel Class { get; set; }
    public IDictionary<ClassViewModel, IEnumerable<ClassViewModel>> ClassesByAbstractClasses { get; set; }
    public GridViewModel Grid { get; set; }
  }
}