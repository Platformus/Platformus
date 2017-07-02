// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Shared
{
  public class FieldViewModelFactory : ViewModelFactoryBase
  {
    public FieldViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public FieldViewModel Create(Field field)
    {
      return new FieldViewModel()
      {
        Id = field.Id,
        FieldType = new FieldTypeViewModelFactory(this.RequestHandler).Create(
          this.RequestHandler.Storage.GetRepository<IFieldTypeRepository>().WithKey(field.FieldTypeId)
        ),
        Name = this.GetLocalizationValue(field.NameId),
        FieldOptions = this.RequestHandler.Storage.GetRepository<IFieldOptionRepository>().FilteredByFieldId(field.Id).ToList().Select(
          fo => new FieldOptionViewModelFactory(this.RequestHandler).Create(fo)
        )
      };
    }
  }
}