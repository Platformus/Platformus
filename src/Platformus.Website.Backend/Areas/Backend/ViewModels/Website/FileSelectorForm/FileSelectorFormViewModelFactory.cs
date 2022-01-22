// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Website
{
  public static class FileSelectorFormViewModelFactory
  {
    public static FileSelectorFormViewModel Create(IEnumerable<File> files)
    {
      return new FileSelectorFormViewModel()
      {
        Files = files.Select(FileViewModelFactory.Create).ToList()
      };
    }
  }
}