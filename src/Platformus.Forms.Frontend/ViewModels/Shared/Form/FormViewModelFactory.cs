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
  public class FormViewModelFactory : ViewModelFactoryBase
  {
    public FormViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public FormViewModel Create(Form form)
    {
      return new FormViewModel()
      {
        Id = form.Id,
        Name = this.RequestHandler.GetLocalizationValue(form.NameId),
        Fields = this.RequestHandler.Storage.GetRepository<IFieldRepository>().FilteredByFormId(form.Id).ToList().Select(
          f => new FieldViewModelFactory(this.RequestHandler).Create(f)
        )
      };
    }

    public FormViewModel Create(SerializedForm cachedForm)
    {
      IEnumerable<SerializedField> cachedFields = new SerializedField[] { };

      if (!string.IsNullOrEmpty(cachedForm.SerializedFields))
        cachedFields = JsonConvert.DeserializeObject<IEnumerable<SerializedField>>(cachedForm.SerializedFields);

      return new FormViewModel()
      {
        Id = cachedForm.FormId,
        Name = cachedForm.Name,
        Fields = cachedFields.Select(
          cf => new FieldViewModelFactory(this.RequestHandler).Create(cf)
        )
      };
    }
  }
}