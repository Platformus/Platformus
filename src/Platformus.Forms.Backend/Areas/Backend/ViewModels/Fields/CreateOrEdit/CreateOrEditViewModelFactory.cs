// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend;
using Platformus.Barebone.Primitives;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Fields
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          FieldTypeOptions = this.GetFieldTypeOptions(),
          NameLocalizations = this.GetLocalizations()
        };

      Field field = this.RequestHandler.Storage.GetRepository<IFieldRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = field.Id,
        FieldTypeId = field.FieldTypeId,
        FieldTypeOptions = this.GetFieldTypeOptions(),
        Code = field.Code,
        NameLocalizations = this.GetLocalizations(field.NameId),
        IsRequired = field.IsRequired,
        MaxLength = field.MaxLength,
        Position = field.Position
      };
    }

    private IEnumerable<Option> GetFieldTypeOptions()
    {
      return this.RequestHandler.Storage.GetRepository<IFieldTypeRepository>().All().ToList().Select(
        ft => new Option(ft.Name, ft.Id.ToString())
      );
    }
  }
}