// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Designers.Backend.ViewModels.Shared;

namespace Platformus.Designers.Backend.ViewModels.Views
{
  public class IndexViewModel : ViewModelBase
  {
    public DirectoryViewModel Subdirectory { get; set; }
    public IEnumerable<DirectoryViewModel> Subdirectories { get; set; }
    public GridViewModel Grid { get; set; }
  }
}