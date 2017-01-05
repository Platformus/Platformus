// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Domain.Frontend.ViewModels.Shared
{
  public class ObjectViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string ViewName { get; set; }
    public string Url { get; set; }
    public IDictionary<string, PropertyViewModel> Properties { get; set; }
    public IDictionary<string, DataSourceViewModel> DataSources { get; set; }
  }
}