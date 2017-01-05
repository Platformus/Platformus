// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Domain.Frontend.ViewModels.Shared
{
  public class DataSourceViewModelFactory : ViewModelFactoryBase
  {
    public DataSourceViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DataSourceViewModel Create(IEnumerable<Object> objects)
    {
      Object @object = objects.FirstOrDefault();

      return new DataSourceViewModel()
      {
        Object = @object == null ? null : new ObjectViewModelFactory(this.RequestHandler).Create(@object),
        Objects = objects.Select(o => new ObjectViewModelFactory(this.RequestHandler).Create(o))
      };
    }

    public DataSourceViewModel Create(IEnumerable<CachedObject> cachedObjects)
    {
      CachedObject cachedObject = cachedObjects.FirstOrDefault();

      return new DataSourceViewModel()
      {
        Object = cachedObject == null ? null : new ObjectViewModelFactory(this.RequestHandler).Create(cachedObject),
        Objects = cachedObjects.Select(co => new ObjectViewModelFactory(this.RequestHandler).Create(co))
      };
    }
  }
}