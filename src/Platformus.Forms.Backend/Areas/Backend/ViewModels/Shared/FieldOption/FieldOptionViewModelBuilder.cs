// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Forms.Data.Models;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Globalization.Data.Abstractions;

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
        Value = this.handler.Storage.GetRepository<ILocalizationRepository>().FilteredByDictionaryId(fieldOption.ValueId).First().Value,
      };
    }
  }
}