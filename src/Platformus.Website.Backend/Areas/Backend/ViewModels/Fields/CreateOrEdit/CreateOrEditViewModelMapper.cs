// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Fields;

public static class CreateOrEditViewModelMapper
{
  public static Field Map(FieldFilter filter, Field field, CreateOrEditViewModel createOrEdit)
  {
    if (field.Id == 0)
      field.FormId = (int)filter.Form.Id;

    field.FieldTypeId = createOrEdit.FieldTypeId;
    field.Code = createOrEdit.Code;
    field.IsRequired = createOrEdit.IsRequired;
    field.MaxLength = createOrEdit.MaxLength;
    field.Position = createOrEdit.Position;
    return field;
  }
}