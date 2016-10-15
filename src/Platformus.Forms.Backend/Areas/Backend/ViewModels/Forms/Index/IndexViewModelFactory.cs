// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Forms.Backend.ViewModels.Shared;
using Platformus.Forms.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Forms
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Create()
    {
      return new IndexViewModel()
      {
        Forms = this.handler.Storage.GetRepository<IFormRepository>().All().Select(
          f => new FormViewModelFactory(this.handler).Create(f)
        )
      };
    }
  }
}