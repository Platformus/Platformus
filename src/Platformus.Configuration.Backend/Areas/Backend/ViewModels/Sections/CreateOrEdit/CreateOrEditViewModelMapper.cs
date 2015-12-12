// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configuration.Data.Abstractions;
using Platformus.Configuration.Data.Models;

namespace Platformus.Configuration.Backend.ViewModels.Sections
{
  public class CreateOrEditViewModelMapper : ViewModelBuilderBase
  {
    public CreateOrEditViewModelMapper(IHandler handler)
      : base(handler)
    {
    }

    public Section Map(CreateOrEditViewModel createOrEdit)
    {
      Section section = new Section();

      if (createOrEdit.Id != null)
        section = this.handler.Storage.GetRepository<ISectionRepository>().WithKey((int)createOrEdit.Id);

      section.Code = createOrEdit.Code;
      section.Name = createOrEdit.Name;
      return section;
    }
  }
}