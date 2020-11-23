﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Frontend.ViewModels;

namespace Platformus.Website.Frontend.ViewModels.Shared
{
  public class FormViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string SubmitButtonTitle { get; set; }
    public IEnumerable<FieldViewModel> Fields { get; set; }
    public string PartialViewName { get; set; }
    public string AdditionalCssClass { get; set; }
  }
}