// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;

namespace Platformus.Globalization.Backend.ViewModels
{
  public abstract class ViewModelMapperBase : Platformus.Barebone.Backend.ViewModels.ViewModelMapperBase
  {
    public ViewModelMapperBase(IRequestHandler requestHandler)
      : base(requestHandler)
    {
      this.RequestHandler = requestHandler;
    }
  }
}