// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;

namespace Platformus.Globalization.Frontend.ViewModels
{
  public abstract class ViewModelMapperBase : Platformus.Barebone.Frontend.ViewModels.ViewModelMapperBase
  {
    public ViewModelMapperBase(IRequestHandler requestHandler)
      : base(requestHandler)
    {
      this.RequestHandler = requestHandler;
    }
  }
}