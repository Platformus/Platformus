// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configuration.Backend.ViewModels.Shared;
using Platformus.Configuration.Data.Abstractions;

namespace Platformus.Configuration.Backend.ViewModels.Sections
{
  public class IndexViewModelBuilder : ViewModelBuilderBase
  {
    public IndexViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Build()
    {
      return new IndexViewModel()
      {
        Sections = this.handler.Storage.GetRepository<ISectionRepository>().All().Select(
          s => new SectionViewModelBuilder(this.handler).Build(s)
        )
      };
    }
  }
}