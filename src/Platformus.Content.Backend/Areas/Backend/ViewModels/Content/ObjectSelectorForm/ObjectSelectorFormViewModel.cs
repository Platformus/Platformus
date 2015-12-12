// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Content.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Content
{
  public class ObjectSelectorFormViewModel : ViewModelBase
  {
    public ClassViewModel Class { get; set; }
    public IEnumerable<GridColumnViewModel> GridColumns { get; set; }
    public IEnumerable<ObjectViewModel> Objects { get; set; }
    public IEnumerable<int> ObjectIds { get; set; }
  }
}