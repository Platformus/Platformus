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
using Platformus.Website.RequestProcessors;
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
          EndpointPermissions = await this.GetEndpointPermissionsAsync(httpContext),
          RequestProcessorCSharpClassNameOptions = this.GetRequestProcessorCSharpClassNameOptions(),
          RequestProcessors = this.GetRequestProcessors(),
          ResponseCacheCSharpClassNameOptions = this.GetResponseCacheCSharpClassNameOptions()
        };

      return new CreateOrEditViewModel()
      {
        Id = endpoint.Id,
        Name = endpoint.Name,
        UrlTemplate = endpoint.UrlTemplate,
        Position = endpoint.Position,
        DisallowAnonymous = endpoint.DisallowAnonymous,
        SignInUrl = endpoint.SignInUrl,
        EndpointPermissions = await this.GetEndpointPermissionsAsync(httpContext, endpoint),
        RequestProcessorCSharpClassName = endpoint.RequestProcessorCSharpClassName,
        RequestProcessorCSharpClassNameOptions = this.GetRequestProcessorCSharpClassNameOptions(),
        RequestProcessorParameters = endpoint.RequestProcessorParameters,
        RequestProcessors = this.GetRequestProcessors(),
        ResponseCacheCSharpClassName = endpoint.ResponseCacheCSharpClassName,
        ResponseCacheCSharpClassNameOptions = this.GetResponseCacheCSharpClassNameOptions()
      };
    }

    public async Task<IEnumerable<EndpointPermissionViewModel>> GetEndpointPermissionsAsync(HttpContext httpContext, Data.Entities.Endpoint endpoint = null)
    {
      return (await httpContext.GetStorage().GetRepository<int, Permission, PermissionFilter>().GetAllAsync()).Select(
        p => new EndpointPermissionViewModelFactory().Create(p, endpoint != null && endpoint.EndpointPermissions.Any(ep => ep.PermissionId == p.Id))
      );
    }

    private IEnumerable<Option> GetRequestProcessorCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IRequestProcessor>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
        t => new Option(t.FullName)
      );
    }

    private IEnumerable<dynamic> GetRequestProcessors()
    {
      return ExtensionManager.GetInstances<IRequestProcessor>().Where(rp => !rp.GetType().GetTypeInfo().IsAbstract).Select(
        rp => new {
          cSharpClassName = rp.GetType().FullName,
          parameterGroups = rp.ParameterGroups.Select(
            rppg => new
            {
              name = rppg.Name,
              parameters = rppg.Parameters.Select(
                rpp => new
                {
                  code = rpp.Code,
                  name = rpp.Name,
                  javaScriptEditorClassName = rpp.JavaScriptEditorClassName,
                  options = rpp.Options == null ? null : rpp.Options.Select(
                    o => new { text = o.Text, value = o.Value }
                  ),
                  defaultValue = rpp.DefaultValue,
                  isRequired = rpp.IsRequired
                }
              )
            }
          ),
          description = rp.Description
        }
      );
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
  }
}