// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Primitives;
using Platformus.Routing.Backend.ViewModels.Shared;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;
using Platformus.Routing.Microcontrollers;
using Platformus.Security.Data.Abstractions;

namespace Platformus.Routing.Backend.ViewModels.Microcontrollers
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
          Microcontrollers = this.GetMicrocontrollers(),
          MicrocontrollerPermissions = this.GetMicrocontrollerPermissions()
        };

      Microcontroller microcontroller = this.RequestHandler.Storage.GetRepository<IMicrocontrollerRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = microcontroller.Id,
        Name = microcontroller.Name,
        UrlTemplate = microcontroller.UrlTemplate,
        Position = microcontroller.Position,
        DisallowAnonymous = microcontroller.DisallowAnonymous,
        SignInUrl = microcontroller.SignInUrl,
        CSharpClassName = microcontroller.CSharpClassName,
        CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
        Parameters = microcontroller.Parameters,
        Microcontrollers = this.GetMicrocontrollers(),
        MicrocontrollerPermissions = this.GetMicrocontrollerPermissions(microcontroller)
      };
    }

    private IEnumerable<Option> GetCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IMicrocontroller>().Where(m => m.GetType() != typeof(MicrocontrollerBase)).Select(
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
                  javaScriptEditorClassName = mp.JavaScriptEditorClassName,
                  options = mp.Options == null ? null : mp.Options.Select(
                    o => new { text = o.Text, value = o.Value }
                  ),
                  defaultValue = mp.DefaultValue,
                  isRequired = mp.IsRequired
                }
              )
            }
          ),
          description = m.Description
        }
      );
    }

    public IEnumerable<MicrocontrollerPermissionViewModel> GetMicrocontrollerPermissions(Microcontroller microcontroller = null)
    {
      return this.RequestHandler.Storage.GetRepository<IPermissionRepository>().All().Select(
        p => new MicrocontrollerPermissionViewModelFactory(this.RequestHandler).Create(microcontroller, p)
      );
    }
  }
}