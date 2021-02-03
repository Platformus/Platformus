// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class FormViewModelFactory : ViewModelFactoryBase
  {
    public FormViewModel Create(Form form)
    {
      return new FormViewModel()
      {
        Id = form.Id,
        Name = form.Name.GetLocalizationValue(),
        ProduceCompletedForms = form.ProduceCompletedForms,
        Fields = form.Fields.Select(
          f => new FieldViewModelFactory().Create(f)
        )
      };
    }
  }
}