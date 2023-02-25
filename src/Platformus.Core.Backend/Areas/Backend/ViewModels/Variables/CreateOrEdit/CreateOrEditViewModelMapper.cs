// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.ViewModels.Variables;

public static class CreateOrEditViewModelMapper
{
  public static Variable Map(VariableFilter filter, Variable variable, CreateOrEditViewModel createOrEdit)
  {
    if (variable.Id == 0)
      variable.ConfigurationId = (int)filter.Configuration.Id;

    variable.Code = createOrEdit.Code;
    variable.Name = createOrEdit.Name;
    variable.Value = createOrEdit.Value;
    variable.Position = createOrEdit.Position;
    return variable;
  }
}