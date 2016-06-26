// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Classes
{
  public class CreateOrEditViewModelMapper : ViewModelBuilderBase
  {
    public CreateOrEditViewModelMapper(IHandler handler)
      : base(handler)
    {
    }

    public Class Map(CreateOrEditViewModel createOrEdit)
    {
      Class @class = new Class();

      if (createOrEdit.Id != null)
        @class = this.handler.Storage.GetRepository<IClassRepository>().WithKey((int)createOrEdit.Id);

      @class.ClassId = createOrEdit.ClassId;
      @class.Name = createOrEdit.Name;
      @class.PluralizedName = createOrEdit.PluralizedName;
      @class.IsAbstract = createOrEdit.IsAbstract ? true : (bool?)null;
      @class.IsStandalone = createOrEdit.IsStandalone ? true : (bool?)null;
      @class.DefaultViewName = createOrEdit.DefaultViewName;
      return @class;
    }
  }
}