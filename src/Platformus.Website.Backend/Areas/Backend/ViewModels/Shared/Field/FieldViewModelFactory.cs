// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class FieldViewModelFactory : ViewModelFactoryBase
  {
    public FieldViewModel Create(Field field)
    {
      return new FieldViewModel()
      {
        Id = field.Id,
        FieldType = new FieldTypeViewModelFactory().Create(field.FieldType),
        Name = field.Name.GetLocalizationValue(),
        FieldOptions = field.FieldOptions.Select(
          fo => new FieldOptionViewModelFactory().Create(fo)
        )
      };
    }
  }
}