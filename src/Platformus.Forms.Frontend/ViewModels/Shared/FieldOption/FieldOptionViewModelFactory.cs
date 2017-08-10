// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Forms.Frontend.ViewModels.Shared
{
  public class FieldOptionViewModelFactory : ViewModelFactoryBase
  {
    public FieldOptionViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public FieldOptionViewModel Create(SerializedFieldOption serializedFieldOption)
    {
      return new FieldOptionViewModel()
      {
        Value = serializedFieldOption.Value
      };
    }
  }
}