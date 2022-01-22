// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public static class FormViewModelFactory
  {
    public static FormViewModel Create(Form form)
    {
      return new FormViewModel()
      {
        Id = form.Id,
        Name = form.Name.GetLocalizationValue(),
        ProduceCompletedForms = form.ProduceCompletedForms,
        Fields = form.Fields == null ?
          Array.Empty<FieldViewModel>() :
          form.Fields
            .OrderBy(f => f.Position)
            .Select(FieldViewModelFactory.Create).ToList()
      };
    }
  }
}