// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Models;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Forms.Frontend.ViewModels.Shared
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
        FieldType = new FieldTypeViewModelFactory(this.RequestHandler).Create(this.RequestHandler.Storage.GetRepository<IFieldTypeRepository>().WithKey(field.FieldTypeId)),
        Name = this.RequestHandler.GetLocalizationValue(field.NameId),
        IsRequired = field.IsRequired,
        MaxLength = field.MaxLength,
        FieldOptions = this.RequestHandler.Storage.GetRepository<IFieldOptionRepository>().FilteredByFieldId(field.Id).Select(
          fi => new FieldOptionViewModelFactory(this.RequestHandler).Create(fi)
        )
      };
    }

    public FieldViewModel Create(SerializedField serializedField)
    {
      IEnumerable<SerializedFieldOption> cachedFieldOptions = new SerializedFieldOption[] { };

      if (!string.IsNullOrEmpty(serializedField.SerializedFieldOptions))
        cachedFieldOptions = JsonConvert.DeserializeObject<IEnumerable<SerializedFieldOption>>(serializedField.SerializedFieldOptions);

      return new FieldViewModel()
      {
        Id = serializedField.FieldId,
        FieldType = new FieldTypeViewModel() { Code = serializedField.FieldTypeCode },
        Name = serializedField.Name,
        IsRequired = serializedField.IsRequired,
        MaxLength = serializedField.MaxLength,
        FieldOptions = cachedFieldOptions.Select(
          fo => new FieldOptionViewModelFactory(this.RequestHandler).Create(fo)
        )
      };
    }
  }
}