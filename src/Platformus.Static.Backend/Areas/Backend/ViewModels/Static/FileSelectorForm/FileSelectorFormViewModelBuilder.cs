// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Static.Backend.ViewModels.Shared;
using Platformus.Static.Data.Abstractions;

namespace Platformus.Static.Backend.ViewModels.Static
{
  public class FileSelectorFormViewModelBuilder : ViewModelBuilderBase
  {
    public FileSelectorFormViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public FileSelectorFormViewModel Build()
    {
      return new FileSelectorFormViewModel()
      {
        Files = this.handler.Storage.GetRepository<IFileRepository>().All().Select(
          f => new FileViewModelBuilder(this.handler).Build(f)
        )
      };
    }
  }
}