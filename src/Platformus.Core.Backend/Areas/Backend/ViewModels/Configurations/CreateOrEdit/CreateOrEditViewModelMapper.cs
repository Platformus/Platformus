// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Configurations;

public static class CreateOrEditViewModelMapper
{
  public static Configuration Map(Configuration configuration, CreateOrEditViewModel createOrEdit)
  {
    configuration.Code = createOrEdit.Code;
    configuration.Name = createOrEdit.Name;
    return configuration;
  }
}