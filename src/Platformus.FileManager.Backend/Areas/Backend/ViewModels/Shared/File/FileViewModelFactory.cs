// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.FileManager.Data.Entities;

namespace Platformus.FileManager.Backend.ViewModels.Shared
{
  public class FileViewModelFactory : ViewModelFactoryBase
  {
    public FileViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public FileViewModel Create(File file)
    {
      return new FileViewModel()
      {
        Id = file.Id,
        Name = file.Name,
        Size = file.Size
      };
    }
  }
}