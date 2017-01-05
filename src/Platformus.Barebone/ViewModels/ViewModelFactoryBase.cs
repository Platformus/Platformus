// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Barebone.ViewModels
{
  public abstract class ViewModelFactoryBase
  {
    protected IRequestHandler RequestHandler { get; set; }

    public ViewModelFactoryBase(IRequestHandler requestHandler)
    {
      this.RequestHandler = requestHandler;
    }
  }
}