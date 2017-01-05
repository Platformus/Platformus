// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.FileManager.Backend.ViewModels.Shared;

namespace Platformus.FileManager.Backend.ViewModels.FileManager
{
  public class FileSelectorFormViewModel : ViewModelBase
  {
    public IEnumerable<FileViewModel> Files { get; set; }
  }
}