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
  public class FormViewModelFactory : ViewModelFactoryBase
  {
    public FormViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public FormViewModel Create(SerializedForm serializedForm, string additionalCssClass)
    {
      IEnumerable<SerializedField> serializedFields = new SerializedField[] { };

      if (!string.IsNullOrEmpty(serializedForm.SerializedFields))
        serializedFields = JsonConvert.DeserializeObject<IEnumerable<SerializedField>>(serializedForm.SerializedFields);

      return new FormViewModel()
      {
        Id = serializedForm.FormId,
        Name = serializedForm.Name,
        SubmitButtonTitle = serializedForm.SubmitButtonTitle,
        Fields = serializedFields.Select(
          sf => new FieldViewModelFactory(this.RequestHandler).Create(sf)
        ).ToList(),
        AdditionalCssClass = additionalCssClass
      };
    }
  }
}