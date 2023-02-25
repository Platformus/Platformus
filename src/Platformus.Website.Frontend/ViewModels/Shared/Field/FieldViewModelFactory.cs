// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Frontend.ViewModels.Shared;

public static class FieldViewModelFactory
{
  public static FieldViewModel Create(Field field)
  {
    return new FieldViewModel()
    {
      Id = field.Id,
      FieldType = new FieldTypeViewModel() { Code = field.FieldType.Code },
      Name = field.Name.GetLocalizationValue(),
      Code = field.Code,
      IsRequired = field.IsRequired,
      MaxLength = field.MaxLength,
      FieldOptions = field.FieldOptions.Select(FieldOptionViewModelFactory.Create)
    };
  }
}