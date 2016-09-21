// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Shared
{
  public class FieldOptionViewModelBuilder : ViewModelBuilderBase
  {
    public FieldOptionViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public FieldOptionViewModel Build(FieldOption fieldOption)
    {
      return new FieldOptionViewModel()
      {
        Id = fieldOption.Id,
        Value = this.GetLocalizationValue(fieldOption.ValueId)
      };
    }
  }
}