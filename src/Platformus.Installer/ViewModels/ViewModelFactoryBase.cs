// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace Platformus.Installer.ViewModels
{
  public abstract class ViewModelFactoryBase
  {
    protected Controller Controller { get; set; }

    public ViewModelFactoryBase(Controller controller)
    {
      this.Controller = controller;
    }
  }
}