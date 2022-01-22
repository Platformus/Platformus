// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public static class VariableViewModelFactory
  {
    public static VariableViewModel Create(Variable variable)
    {
      return new VariableViewModel()
      {
        Id = variable.Id,
        ConfigurationId = variable.ConfigurationId,
        Name = variable.Name,
        Value = variable.Value
      };
    }
  }
}