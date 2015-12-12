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
  public class FormViewModelBuilder : ViewModelBuilderBase
  {
    public FormViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public FormViewModel Build(Form form)
    {
      return new FormViewModel()
      {
        Id = form.Id,
        Name = this.GetObjectLocalizationValue(form.NameId),
        Fields = this.handler.Storage.GetRepository<IFieldRepository>().FilteredByFormId(form.Id).Select(
          f => new FieldViewModelBuilder(this.handler).Build(f)
        )
      };
    }

    public FormViewModel Build(CachedForm cachedForm)
    {
      IEnumerable<CachedField> cachedFields = new CachedField[] { };

      if (!string.IsNullOrEmpty(cachedForm.CachedFields))
        cachedFields = JsonConvert.DeserializeObject<IEnumerable<CachedField>>(cachedForm.CachedFields);

      return new FormViewModel()
      {
        Id = cachedForm.FormId,
        Name = cachedForm.Name,
        Fields = cachedFields.Select(
          cf => new FieldViewModelBuilder(this.handler).Build(cf)
        )
      };
    }
  }
}