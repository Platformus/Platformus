// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace Platformus.Installer.ViewModels.Installation
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(Controller controller) : base(controller) { }

    public IndexViewModel Create()
    {
      return new IndexViewModel()
      {
        Installation = ResourceManager.GetInstallation()
      };
    }
  }
}