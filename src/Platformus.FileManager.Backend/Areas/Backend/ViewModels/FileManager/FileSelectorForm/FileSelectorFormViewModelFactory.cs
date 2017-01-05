// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.FileManager.Backend.ViewModels.Shared;
using Platformus.FileManager.Data.Abstractions;

namespace Platformus.FileManager.Backend.ViewModels.FileManager
{
  public class FileSelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public FileSelectorFormViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public FileSelectorFormViewModel Create()
    {
      return new FileSelectorFormViewModel()
      {
        Files = this.RequestHandler.Storage.GetRepository<IFileRepository>().All().Select(
          f => new FileViewModelFactory(this.RequestHandler).Create(f)
        )
      };
    }
  }
}