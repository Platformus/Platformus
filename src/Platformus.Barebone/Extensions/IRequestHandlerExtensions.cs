// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;

namespace Platformus
{
  public static class IRequestHandlerExtensions
  {
    public static T GetService<T>(this IRequestHandler requestHandler)
    {
      return requestHandler.HttpContext.RequestServices.GetService<T>();
    }
  }
}