// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Data.Entities;
using Platformus.Core.Extensions;
using Platformus.Core.Filters;
using Platformus.Core.Primitives;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Endpoints;
using Platformus.Website.ResponseCaches;

namespace Platformus.Website.Backend.ViewModels.Endpoints
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Data.Entities.Endpoint endpoint)
    {
      if (endpoint == null)
        return new CreateOrEditViewModel()
        {
          ResponseCacheCSharpClassNameOptions = this.GetResponseCacheCSharpClassNameOptions(),
          CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
          Endpoints = this.GetEndpoints(),
          EndpointPermissions = await this.GetEndpointPermissionsAsync(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = endpoint.Id,
        Name = endpoint.Name,
        UrlTemplate = endpoint.UrlTemplate,
        Position = endpoint.Position,
        DisallowAnonymous = endpoint.DisallowAnonymous,
        SignInUrl = endpoint.SignInUrl,
        ResponseCacheCSharpClassName = endpoint.ResponseCacheCSharpClassName,
        ResponseCacheCSharpClassNameOptions = this.GetResponseCacheCSharpClassNameOptions(),
        CSharpClassName = endpoint.CSharpClassName,
        CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
        Parameters = endpoint.Parameters,
        Endpoints = this.GetEndpoints(),
        EndpointPermissions = await this.GetEndpointPermissionsAsync(httpContext, endpoint)
      };
    }

    private IEnumerable<Option> GetResponseCacheCSharpClassNameOptions()
    {
      List<Option> options = new List<Option>();

      options.Add(new Option("Response cache C# class name not specified", string.Empty));
      options.AddRange(
        ExtensionManager.GetImplementations<IResponseCache>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
          t => new Option(t.FullName)
        )
      );

      return options;
    }

    private IEnumerable<Option> GetCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IEndpoint>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
        t => new Option(t.FullName)
      );
    }

    private IEnumerable<dynamic> GetEndpoints()
    {
      return ExtensionManager.GetInstances<IEndpoint>().Where(e => !e.GetType().GetTypeInfo().IsAbstract).Select(
        e => new {
          cSharpClassName = e.GetType().FullName,
          parameterGroups = e.ParameterGroups.Select(
            epg => new
            {
              name = epg.Name,
              parameters = epg.Parameters.Select(
                ep => new
                {
                  code = ep.Code,
                  name = ep.Name,
                  javaScriptEditorClassName = ep.JavaScriptEditorClassName,
                  options = ep.Options == null ? null : ep.Options.Select(
                    o => new { text = o.Text, value = o.Value }
                  ),
                  defaultValue = ep.DefaultValue,
                  isRequired = ep.IsRequired
                }
              )
            }
          ),
          description = e.Description
        }
      );
    }

    public async Task<IEnumerable<EndpointPermissionViewModel>> GetEndpointPermissionsAsync(HttpContext httpContext, Data.Entities.Endpoint endpoint = null)
    {
      return (await httpContext.GetStorage().GetRepository<int, Permission, PermissionFilter>().GetAllAsync()).Select(
        p => new EndpointPermissionViewModelFactory().Create(p, endpoint != null && endpoint.EndpointPermissions.Any(ep => ep.PermissionId == p.Id))
      );
    }
  }
}