// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Backend;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Backend.ViewModels.Shared;

namespace Platformus.Website.Backend.ViewModels.Objects;

public class IndexViewModel : ViewModelBase
{
  public ClassViewModel ParentClass { get; set; }
  public ClassViewModel Class { get; set; }
  public IDictionary<ClassViewModel, IEnumerable<ClassViewModel>> ClassesByAbstractClasses { get; set; }
  public string Sorting { get; set; }
  public int Offset { get; set; }
  public int Limit { get; set; }
  public int Total { get; set; }
  public IEnumerable<TableTagHelper.Column> TableColumns { get; set; }
  public IEnumerable<ObjectViewModel> Objects { get; set; }
}