// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public class ConfigurationViewModelFactory : ViewModelFactoryBase
  {
    public ConfigurationViewModel Create(Configuration configuration)
    {
      return new ConfigurationViewModel()
      {
        Id = configuration.Id,
        Name = configuration.Name,
        Variables = configuration.Variables.Select(
          v => new VariableViewModelFactory().Create(v)
        )
      };
    }
  }
}