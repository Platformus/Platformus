// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Infrastructure;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Primitives;
using Platformus.Routing.Backend.ViewModels.Shared;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;
using Platformus.Routing.Endpoints;
using Platformus.Security.Data.Abstractions;

namespace Platformus.Routing.Backend.ViewModels.Endpoints
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
          Endpoints = this.GetEndpoints(),
          EndpointPermissions = this.GetEndpointPermissions()
        };

      Endpoint endpoint = this.RequestHandler.Storage.GetRepository<IEndpointRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = endpoint.Id,
        Name = endpoint.Name,
        UrlTemplate = endpoint.UrlTemplate,
        Position = endpoint.Position,
        DisallowAnonymous = endpoint.DisallowAnonymous,
        SignInUrl = endpoint.SignInUrl,
        CSharpClassName = endpoint.CSharpClassName,
        CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
        Parameters = endpoint.Parameters,
        Endpoints = this.GetEndpoints(),
        EndpointPermissions = this.GetEndpointPermissions(endpoint)
      };
    }

    private IEnumerable<Option> GetCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IEndpoint>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
        t => new Option(t.FullName)
      );
    }

    private IEnumerable<dynamic> GetEndpoints()
    {
      return ExtensionManager.GetInstances<IEndpoint>().Where(m => !m.GetType().GetTypeInfo().IsAbstract).Select(
        m => new {
          cSharpClassName = m.GetType().FullName,
          endpointParameterGroups = m.EndpointParameterGroups.Select(
            mpg => new
            {
              name = mpg.Name,
              endpointParameters = mpg.EndpointParameters.Select(
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

    public IEnumerable<EndpointPermissionViewModel> GetEndpointPermissions(Endpoint endpoint = null)
    {
      return this.RequestHandler.Storage.GetRepository<IPermissionRepository>().All().Select(
        p => new EndpointPermissionViewModelFactory(this.RequestHandler).Create(endpoint, p)
      );
    }
  }
}