// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Globalization.Frontend.ViewModels.Shared
{
  public class CulturesViewModel : ViewModelBase
  {
    public IEnumerable<CultureViewModel> Cultures { get; set; }
    public string PartialViewName { get; set; }
    public string AdditionalCssClass { get; set; }
  }
}