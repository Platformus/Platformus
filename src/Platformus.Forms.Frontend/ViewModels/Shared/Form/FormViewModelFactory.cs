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

    public FormViewModel Create(SerializedForm serializedForm)
    {
      IEnumerable<SerializedField> serializedFields = new SerializedField[] { };

      if (!string.IsNullOrEmpty(serializedForm.SerializedFields))
        serializedFields = JsonConvert.DeserializeObject<IEnumerable<SerializedField>>(serializedForm.SerializedFields);

      return new FormViewModel()
      {
        Id = serializedForm.FormId,
        Name = serializedForm.Name,
        Fields = serializedFields.Select(
          sf => new FieldViewModelFactory(this.RequestHandler).Create(sf)
        )
      };
    }
  }
}