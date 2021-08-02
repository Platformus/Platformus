// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Variables
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(Variable variable)
    {
      if (variable == null)
        return new CreateOrEditViewModel()
        {
        };

      return new CreateOrEditViewModel()
      {
        Id = variable.Id,
        Code = variable.Code,
        Name = variable.Name,
        Value = variable.Value,
        Position = variable.Position
      };
    }
  }
}