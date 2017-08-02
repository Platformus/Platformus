// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;
using Platformus.Security;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Domain.Frontend
{
  public class DefaultRouteHandler : IDefaultRouteHandler
  {
    public IActionResult TryHandle(IRequestHandler requestHandler, string url)
    {
      IMicrocontrollerResolver microcontrollerResolver = requestHandler.HttpContext.RequestServices.GetService<IMicrocontrollerResolver>();
      Microcontroller microcontroller = microcontrollerResolver.GetMicrocontroller(requestHandler, url);

      if (microcontroller == null)
        return null;

      if (microcontroller.DisallowAnonymous)
      {
        if (!requestHandler.HttpContext.User.Identity.IsAuthenticated || !this.HasRequiredClaims(requestHandler, microcontroller))
        {
          if (string.IsNullOrEmpty(microcontroller.SignInUrl))
            throw new HttpException(403, "Access denied.");

          return (requestHandler as Platformus.Barebone.Frontend.Controllers.ControllerBase).Redirect(microcontroller.SignInUrl);
        }
      }

      IMicrocontroller microcontrollerInstance = this.GetMicrocontrollerInstance(microcontroller);

      if (microcontrollerInstance == null)
        return null;

      return microcontrollerInstance.Invoke(requestHandler, microcontroller, microcontrollerResolver.GetParameters(microcontroller.UrlTemplate, url));
    }

    private IMicrocontroller GetMicrocontrollerInstance(Microcontroller microcontroller)
    {
      return StringActivator.CreateInstance<IMicrocontroller>(microcontroller.CSharpClassName);
    }

    private bool HasRequiredClaims(IRequestHandler requestHandler, Microcontroller microcontroller)
    {
      IEnumerable<MicrocontrollerPermission> microcontrollerPermissions = requestHandler.Storage.GetRepository<IMicrocontrollerPermissionRepository>().FilteredByMicrocontrollerId(microcontroller.Id);

      if (microcontrollerPermissions.Count() == 0 || requestHandler.HttpContext.User.HasClaim(PlatformusClaimTypes.Permission, Platformus.Security.Permissions.DoEverything))
        return true;

      foreach (MicrocontrollerPermission microcontrollerPermission in microcontrollerPermissions)
      {
        Permission permission = requestHandler.Storage.GetRepository<IPermissionRepository>().WithKey(microcontrollerPermission.PermissionId);

        if (!requestHandler.HttpContext.User.HasClaim(PlatformusClaimTypes.Permission, permission.Code))
          return false;
      }

      return true;
    }
  }
}