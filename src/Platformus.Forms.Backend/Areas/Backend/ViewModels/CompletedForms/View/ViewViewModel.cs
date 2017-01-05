// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Forms.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.CompletedForms
{
  public class ViewViewModel : ViewModelBase
  {
    public int Id { get; set; }
    
    public IEnumerable<CompletedFieldViewModel> CompletedFields { get; set; }
  }
}