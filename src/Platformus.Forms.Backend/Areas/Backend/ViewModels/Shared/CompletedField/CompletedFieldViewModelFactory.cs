// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Shared
{
  public class CompletedFieldViewModelFactory : ViewModelFactoryBase
  {
    public CompletedFieldViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CompletedFieldViewModel Create(CompletedField completedField)
    {
      return new CompletedFieldViewModel()
      {
        Id = completedField.Id,
        Field = new FieldViewModelFactory(this.RequestHandler).Create(
          this.RequestHandler.Storage.GetRepository<IFieldRepository>().WithKey(completedField.FieldId)
        ),
        Value = completedField.Value
      };
    }
  }
}