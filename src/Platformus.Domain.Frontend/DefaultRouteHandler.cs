// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization;

namespace Platformus.Domain.Frontend
{
  public class DefaultRouteHandler : IDefaultRouteHandler
  {
    public IActionResult TryHandle(IRequestHandler requestHandler, string url)
    {
      Microcontroller microcontroller = this.GetMicrocontroller(requestHandler, url);

      if (microcontroller == null)
        return null;

      IMicrocontroller microcontrollerInstance = this.GetMicrocontrollerInstance(microcontroller);

      if (microcontrollerInstance == null)
        return null;

      return microcontrollerInstance.Invoke(requestHandler, microcontroller/*, params*/);
    }

    private Microcontroller GetMicrocontroller(IRequestHandler requestHandler, string url)
    {
      // We must use cache here
      IEnumerable<Microcontroller> microcontrollers = requestHandler.Storage.GetRepository<IMicrocontrollerRepository>().All();
      Microcontroller microcontroller = microcontrollers.FirstOrDefault(m => m.UrlTemplate == url);

      if (microcontroller != null)
        return microcontroller;

      // We must implement real microcontroller selection logic here
      return microcontrollers.FirstOrDefault(m => m.UrlTemplate == "{*url}");
    }

    private IMicrocontroller GetMicrocontrollerInstance(Microcontroller microcontroller)
    {
      return StringActivator.CreateInstance<IMicrocontroller>(microcontroller.CSharpClassName);
    }
  }
}