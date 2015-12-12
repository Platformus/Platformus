// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Objects
{
  public class CreateOrEditViewModelMapper : ViewModelBuilderBase
  {
    public CreateOrEditViewModelMapper(IHandler handler)
      : base(handler)
    {
    }

    public Object Map(CreateOrEditViewModel createOrEdit)
    {
      Object @object = new Object();

      if (createOrEdit.Id != null)
        @object = this.handler.Storage.GetRepository<IObjectRepository>().WithKey((int)createOrEdit.Id);

      else @object.ClassId = (int)createOrEdit.ClassId;

      Class @class = this.handler.Storage.GetRepository<IClassRepository>().WithKey(@object.ClassId);

      if (@class.IsStandalone == true)
        @object.Url = createOrEdit._Url;

      else @object.Url = null;

      return @object;
    }
  }
}