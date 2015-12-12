// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configuration.Data.Abstractions;
using Platformus.Configuration.Data.Models;

namespace Platformus.Configuration.Backend.ViewModels.Sections
{
  public class CreateOrEditViewModelBuilder : ViewModelBuilderBase
  {
    public CreateOrEditViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public CreateOrEditViewModel Build(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
        };

      Section section = this.handler.Storage.GetRepository<ISectionRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = section.Id,
        Code = section.Code,
        Name = section.Name
      };
    }
  }
}