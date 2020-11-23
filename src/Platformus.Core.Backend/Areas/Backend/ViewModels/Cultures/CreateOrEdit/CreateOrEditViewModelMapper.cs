// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Cultures
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public Culture Map(Culture culture, CreateOrEditViewModel createOrEdit)
    {
      culture.Code = createOrEdit.Code;
      culture.Name = createOrEdit.Name;
      culture.IsFrontendDefault = createOrEdit.IsFrontendDefault;
      culture.IsBackendDefault = createOrEdit.IsBackendDefault;
      return culture;
    }
  }
}