// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using Platformus.Core.Primitives;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.RequestProcessors;
using Platformus.Website.ResponseCaches;

namespace Platformus.Website.Backend.ViewModels.Endpoints
{
  public static class CreateOrEditViewModelFactory
  {
    public static async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Data.Entities.Endpoint endpoint)
    {
      if (endpoint == null)
        return new CreateOrEditViewModel()
        {
          EndpointPermissions = await GetEndpointPermissionsAsync(httpContext),
          RequestProcessorCSharpClassNameOptions = GetRequestProcessorCSharpClassNameOptions(),
          ResponseCacheCSharpClassNameOptions = GetResponseCacheCSharpClassNameOptions(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = endpoint.Id,
        Name = endpoint.Name,
        UrlTemplate = endpoint.UrlTemplate,
        Position = endpoint.Position,
        DisallowAnonymous = endpoint.DisallowAnonymous,
        SignInUrl = endpoint.SignInUrl,
        EndpointPermissions = await GetEndpointPermissionsAsync(httpContext, endpoint),
        RequestProcessorCSharpClassName = endpoint.RequestProcessorCSharpClassName,
        RequestProcessorCSharpClassNameOptions = GetRequestProcessorCSharpClassNameOptions(),
        RequestProcessorParameters = endpoint.RequestProcessorParameters,
        ResponseCacheCSharpClassName = endpoint.ResponseCacheCSharpClassName,
        ResponseCacheCSharpClassNameOptions = GetResponseCacheCSharpClassNameOptions(httpContext),
        ResponseCacheParameters = endpoint.ResponseCacheParameters
      };
    }

    public static async Task<IEnumerable<EndpointPermissionViewModel>> GetEndpointPermissionsAsync(HttpContext httpContext, Data.Entities.Endpoint endpoint = null)
    {
      return (await httpContext.GetStorage().GetRepository<int, Permission, PermissionFilter>().GetAllAsync()).Select(
        p => EndpointPermissionViewModelFactory.Create(p, endpoint != null && endpoint.EndpointPermissions.Any(ep => ep.PermissionId == p.Id))
      ).ToList();
    }

    private static IEnumerable<Option> GetRequestProcessorCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IRequestProcessor>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
        t => new Option(t.FullName)
      ).ToList();
    }

    private static IEnumerable<Option> GetResponseCacheCSharpClassNameOptions(HttpContext httpContext)
    {
      IStringLocalizer localizer = httpContext.GetStringLocalizer<CreateOrEditViewModel>();
      List<Option> options = new List<Option>();

      options.Add(new Option(localizer["Response cache C# class name not specified"], string.Empty));
      options.AddRange(
        ExtensionManager.GetImplementations<IResponseCache>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
          t => new Option(t.FullName)
        )
      );

      return options;
    }
  }
}