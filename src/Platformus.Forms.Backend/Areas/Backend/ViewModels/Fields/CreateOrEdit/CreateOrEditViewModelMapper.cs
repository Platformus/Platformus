// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Fields
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Field Map(CreateOrEditViewModel createOrEdit)
    {
      Field field = new Field();

      if (createOrEdit.Id != null)
        field = this.RequestHandler.Storage.GetRepository<IFieldRepository>().WithKey((int)createOrEdit.Id);

      else field.FormId = createOrEdit.FormId;

      field.FieldTypeId = createOrEdit.FieldTypeId;
      field.Code = createOrEdit.Code;
      field.IsRequired = createOrEdit.IsRequired;
      field.MaxLength = createOrEdit.MaxLength;
      field.Position = createOrEdit.Position;
      return field;
    }
  }
}