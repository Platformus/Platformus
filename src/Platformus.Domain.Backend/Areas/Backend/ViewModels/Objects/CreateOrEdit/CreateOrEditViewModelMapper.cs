// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Objects
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Object Map(CreateOrEditViewModel createOrEdit)
    {
      Object @object = new Object();

      if (createOrEdit.Id != null)
        @object = this.RequestHandler.Storage.GetRepository<IObjectRepository>().WithKey((int)createOrEdit.Id);

      else @object.ClassId = (int)createOrEdit.ClassId;

      return @object;
    }
  }
}