// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Shared
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
        Name = this.GetLocalizationValue(form.NameId),
        ProduceCompletedForms = form.ProduceCompletedForms,
        Fields = this.RequestHandler.Storage.GetRepository<IFieldRepository>().FilteredByFormId(form.Id).ToList().Select(
          f => new FieldViewModelFactory(this.RequestHandler).Create(f)
        )
      };
    }
  }
}