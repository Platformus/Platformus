// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configuration.Data.Abstractions;
using Platformus.Configuration.Data.Models;

namespace Platformus.Configuration.Backend.ViewModels.Shared
{
  public class SectionViewModelBuilder : ViewModelBuilderBase
  {
    public SectionViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public SectionViewModel Build(Section section)
    {
      return new SectionViewModel()
      {
        Id = section.Id,
        Name = section.Name,
        Variables = this.handler.Storage.GetRepository<IVariableRepository>().FilteredBySectionId(section.Id).Select(
          v => new VariableViewModelBuilder(this.handler).Build(v)
        )
      };
    }
  }
}