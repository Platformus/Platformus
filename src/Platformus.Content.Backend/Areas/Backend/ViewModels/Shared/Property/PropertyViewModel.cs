// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone.Backend;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Shared
{
  public class PropertyViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public IEnumerable<Localization> HtmlLocalizations { get; set; }
  }
}