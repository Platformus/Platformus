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
  public class FieldViewModelBuilder : ViewModelBuilderBase
  {
    public FieldViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public FieldViewModel Build(Field field)
    {
      return new FieldViewModel()
      {
        Id = field.Id,
        FieldType = new FieldTypeViewModelBuilder(this.handler).Build(this.handler.Storage.GetRepository<IFieldTypeRepository>().WithKey(field.FieldTypeId)),
        Name = this.GetLocalizationValue(field.NameId),
        FieldOptions = this.handler.Storage.GetRepository<IFieldOptionRepository>().FilteredByFieldId(field.Id).Select(
          fi => new FieldOptionViewModelBuilder(this.handler).Build(fi)
        )
      };
    }

    public FieldViewModel Build(CachedField cachedField)
    {
      IEnumerable<CachedFieldOption> cachedFieldOptions = new CachedFieldOption[] { };

      if (!string.IsNullOrEmpty(cachedField.CachedFieldOptions))
        cachedFieldOptions = JsonConvert.DeserializeObject<IEnumerable<CachedFieldOption>>(cachedField.CachedFieldOptions);

      return new FieldViewModel()
      {
        Id = cachedField.FieldId,
        FieldType = new FieldTypeViewModel() { Code = cachedField.FieldTypeCode },
        Name = cachedField.Name,
        FieldOptions = cachedFieldOptions.Select(
          fo => new FieldOptionViewModelBuilder(this.handler).Build(fo)
        )
      };
    }
  }
}