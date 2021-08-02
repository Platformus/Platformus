// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public static class FieldViewModelFactory
  {
    public static FieldViewModel Create(Field field)
    {
      return new FieldViewModel()
      {
        Id = field.Id,
        FieldType = field.FieldType == null ? null : FieldTypeViewModelFactory.Create(field.FieldType),
        Name = field.Name.GetLocalizationValue(),
        FieldOptions = field.FieldOptions == null ?
          Array.Empty<FieldOptionViewModel>() :
          field.FieldOptions.Select(FieldOptionViewModelFactory.Create).ToList()
      };
    }
  }
}