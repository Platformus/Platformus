// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.FieldOptions
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public FieldOption Map(FieldOptionFilter filter, FieldOption fieldOption, CreateOrEditViewModel createOrEdit)
    {
      if (fieldOption.Id == 0)
        fieldOption.FieldId = (int)filter.Field.Id;

      fieldOption.Position = createOrEdit.Position;
      return fieldOption;
    }
  }
}