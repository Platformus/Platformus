// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.FieldOptions
{
  public class CreateOrEditViewModelMapper : ViewModelBuilderBase
  {
    public CreateOrEditViewModelMapper(IHandler handler)
      : base(handler)
    {
    }

    public FieldOption Map(CreateOrEditViewModel createOrEdit)
    {
      FieldOption fieldOption = new FieldOption();

      if (createOrEdit.Id != null)
        fieldOption = this.handler.Storage.GetRepository<IFieldOptionRepository>().WithKey((int)createOrEdit.Id);

      else fieldOption.FieldId = createOrEdit.FieldId;

      fieldOption.Position = createOrEdit.Position;
      return fieldOption;
    }
  }
}