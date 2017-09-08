// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace Platformus.Installer.ViewModels
{
  public abstract class ViewModelMapperBase
  {
    protected Controller Controller { get; set; }

    public ViewModelMapperBase(Controller controller)
    {
      this.Controller = controller;
    }
  }
}