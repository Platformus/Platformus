// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Configurations
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(Configuration configuration)
    {
      if (configuration == null)
        return new CreateOrEditViewModel()
        {
        };

      return new CreateOrEditViewModel()
      {
        Id = configuration.Id,
        Code = configuration.Code,
        Name = configuration.Name
      };
    }
  }
}