// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Barebone.Frontend.ViewModels
{
  public abstract class ViewModelMapperBase : Platformus.Barebone.ViewModels.ViewModelMapperBase
  {
    public ViewModelMapperBase(IRequestHandler requestHandler)
      : base(requestHandler)
    {
      this.RequestHandler = requestHandler;
    }
  }
}