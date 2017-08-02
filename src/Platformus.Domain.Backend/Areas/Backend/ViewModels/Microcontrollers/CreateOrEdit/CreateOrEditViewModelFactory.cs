// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;
using Platformus.Barebone;
using Platformus.Barebone.Backend;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
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
          CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
          Microcontrollers = this.GetMicrocontrollers()
        };

      Microcontroller microcontroller = this.RequestHandler.Storage.GetRepository<IMicrocontrollerRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = microcontroller.Id,
        Name = microcontroller.Name,
        UrlTemplate = microcontroller.UrlTemplate,
        Position = microcontroller.Position,
        CSharpClassName = microcontroller.CSharpClassName,
        CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
        Parameters = microcontroller.Parameters,
        Microcontrollers = this.GetMicrocontrollers()
      };
    }

    private IEnumerable<Option> GetCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IMicrocontroller>().Select(
        t => new Option(t.FullName)
      );
    }

    private IEnumerable<dynamic> GetMicrocontrollers()
    {
      return ExtensionManager.GetInstances<IMicrocontroller>().Where(m => m.GetType() != typeof(MicrocontrollerBase)).Select(
        m => new {
          cSharpClassName = m.GetType().FullName,
          microcontrollerParameterGroups = m.MicrocontrollerParameterGroups.Select(
            mpg => new
            {
              name = mpg.Name,
              microcontrollerParameters = mpg.MicrocontrollerParameters.Select(
                mp => new
                {
                  code = mp.Code,
                  name = mp.Name,
                  options = mp.Options == null ? null : mp.Options.Select(
                    o => new { text = o.Text, value = o.Value }
                  ),
                  javaScriptEditorClassName = mp.JavaScriptEditorClassName,
                  isRequired = mp.IsRequired
                }
              )
            }
          )
        }
      );
    }
  }
}