// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;
using Platformus.Barebone;
using Platformus.Barebone.Backend;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Microcontrollers
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          CSharpClassNameOptions = this.GetCSharpClassNameOptions()
        };

      Microcontroller microcontroller = this.RequestHandler.Storage.GetRepository<IMicrocontrollerRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = microcontroller.Id,
        Name = microcontroller.Name,
        UrlTemplate = microcontroller.UrlTemplate,
        ViewName = microcontroller.ViewName,
        CSharpClassName = microcontroller.CSharpClassName,
        CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
        Position = microcontroller.Position
      };
    }

    private IEnumerable<Option> GetCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IMicrocontroller>().Select(
        t => new Option(t.FullName)
      );
    }
  }
}