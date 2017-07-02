// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Shared
{
  public class FieldTypeViewModelFactory : ViewModelFactoryBase
  {
    public FieldTypeViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public FieldTypeViewModel Create(FieldType fieldType)
    {
      return new FieldTypeViewModel()
      {
        Id = fieldType.Id,
        Code = fieldType.Code,
        Name = fieldType.Name
      };
    }
  }
}