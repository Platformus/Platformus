// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Forms.Frontend.ViewModels.Shared
{
  public class FieldViewModelFactory : ViewModelFactoryBase
  {
    public FieldViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
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
        Code = serializedField.Code,
        IsRequired = serializedField.IsRequired,
        MaxLength = serializedField.MaxLength,
        FieldOptions = cachedFieldOptions.Select(
          fo => new FieldOptionViewModelFactory(this.RequestHandler).Create(fo)
        ).ToList()
      };
    }
  }
}