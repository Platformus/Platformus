// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Backend.ViewModels.Shared
{
  public class VariableViewModelFactory : ViewModelFactoryBase
  {
    public VariableViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public VariableViewModel Create(Variable variable)
    {
      return new VariableViewModel()
      {
        Id = variable.Id,
        Name = variable.Name,
        Value = variable.Value
      };
    }
  }
}