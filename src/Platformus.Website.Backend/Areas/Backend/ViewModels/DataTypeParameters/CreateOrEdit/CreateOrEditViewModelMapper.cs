// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.DataTypeParameters
{
  public static class CreateOrEditViewModelMapper
  {
    public static DataTypeParameter Map(DataTypeParameterFilter filter, DataTypeParameter dataTypeParameter, CreateOrEditViewModel createOrEdit)
    {
      if (dataTypeParameter.Id == 0)
        dataTypeParameter.DataTypeId = (int)filter.DataType.Id;

      dataTypeParameter.JavaScriptEditorClassName = createOrEdit.JavaScriptEditorClassName;
      dataTypeParameter.Code = createOrEdit.Code;
      dataTypeParameter.Name = createOrEdit.Name;
      return dataTypeParameter;
    }
  }
}