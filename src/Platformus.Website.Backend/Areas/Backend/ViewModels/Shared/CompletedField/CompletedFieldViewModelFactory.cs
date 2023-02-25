// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared;

public static class CompletedFieldViewModelFactory
{
  public static CompletedFieldViewModel Create(CompletedField completedField)
  {
    return new CompletedFieldViewModel()
    {
      Id = completedField.Id,
      Field = FieldViewModelFactory.Create(completedField.Field),
      Value = completedField.Value
    };
  }
}