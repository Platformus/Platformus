// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.Designers.Backend.ViewModels.Shared
{
  public class BundleViewModelFactory : ViewModelFactoryBase
  {
    public BundleViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public BundleViewModel Create(FileInfo bundleFileInfo)
    {
      return new BundleViewModel()
      {
        Filename = bundleFileInfo.Name
      };
    }
  }
}