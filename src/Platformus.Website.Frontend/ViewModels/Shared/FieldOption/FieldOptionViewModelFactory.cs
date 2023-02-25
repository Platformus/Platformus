// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;

namespace Platformus.Website.Frontend.ViewModels.Shared;

public static class FieldOptionViewModelFactory
{
  public static FieldOptionViewModel Create(FieldOption fieldOption)
  {
    return new FieldOptionViewModel()
    {
      Value = fieldOption.Value.GetLocalizationValue()
    };
  }
}