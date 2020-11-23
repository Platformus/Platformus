﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public class VariableViewModelFactory : ViewModelFactoryBase
  {
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