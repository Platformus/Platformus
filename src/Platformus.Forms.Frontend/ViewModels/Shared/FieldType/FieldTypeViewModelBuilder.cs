// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Models;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Forms.Frontend.ViewModels.Shared
{
  public class FieldTypeViewModelBuilder : ViewModelBuilderBase
  {
    public FieldTypeViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public FieldTypeViewModel Build(FieldType fieldType)
    {
      return new FieldTypeViewModel()
      {
        Code = fieldType.Code,
        Name = fieldType.Name
      };
    }
  }
}