// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Configurations
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(IEnumerable<Configuration> configurations)
    {
      return new IndexViewModel()
      {
        Configurations = configurations.Select(
          c => new ConfigurationViewModelFactory().Create(c)
        )
      };
    }
  }
}