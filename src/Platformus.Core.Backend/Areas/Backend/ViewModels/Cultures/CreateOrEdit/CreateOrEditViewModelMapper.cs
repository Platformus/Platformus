// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Cultures
{
  public static class CreateOrEditViewModelMapper
  {
    public static Culture Map(Culture culture, CreateOrEditViewModel createOrEdit)
    {
      culture.Id = createOrEdit.Id;
      culture.Name = createOrEdit.Name;
      culture.IsFrontendDefault = createOrEdit.IsFrontendDefault;
      culture.IsBackendDefault = createOrEdit.IsBackendDefault;
      return culture;
    }
  }
}