// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Classes
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Class Map(CreateOrEditViewModel createOrEdit)
    {
      Class @class = new Class();

      if (createOrEdit.Id != null)
        @class = this.RequestHandler.Storage.GetRepository<IClassRepository>().WithKey((int)createOrEdit.Id);

      @class.ClassId = createOrEdit.ClassId;
      @class.Code = createOrEdit.Code;
      @class.Name = createOrEdit.Name;
      @class.PluralizedName = createOrEdit.PluralizedName;
      @class.IsAbstract = createOrEdit.IsAbstract;
      return @class;
    }
  }
}