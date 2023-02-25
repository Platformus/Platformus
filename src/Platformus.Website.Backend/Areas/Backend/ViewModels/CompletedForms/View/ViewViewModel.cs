// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Backend.ViewModels.Shared;

namespace Platformus.Website.Backend.ViewModels.CompletedForms;

public class ViewViewModel : ViewModelBase
{
  public int Id { get; set; }

  public IEnumerable<CompletedFieldViewModel> CompletedFields { get; set; }
}