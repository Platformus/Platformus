// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Content.Frontend.ViewModels.Shared
{
  public class DataSourceViewModelBuilder : ViewModelBuilderBase
  {
    public DataSourceViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public DataSourceViewModel Build(IEnumerable<Object> objects)
    {
      Object @object = objects.FirstOrDefault();

      return new DataSourceViewModel()
      {
        Object = @object == null ? null : new ObjectViewModelBuilder(this.handler).Build(@object),
        Objects = objects.Select(o => new ObjectViewModelBuilder(this.handler).Build(o))
      };
    }

    public DataSourceViewModel Build(IEnumerable<CachedObject> cachedObjects)
    {
      CachedObject cachedObject = cachedObjects.FirstOrDefault();

      return new DataSourceViewModel()
      {
        Object = cachedObject == null ? null : new ObjectViewModelBuilder(this.handler).Build(cachedObject),
        Objects = cachedObjects.Select(co => new ObjectViewModelBuilder(this.handler).Build(co))
      };
    }
  }
}