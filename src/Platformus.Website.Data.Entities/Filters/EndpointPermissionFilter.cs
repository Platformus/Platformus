// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Website.Filters
{
  public class EndpointPermissionFilter : IFilter
  {
    public EndpointFilter Endpoint { get; set; }
    public PermissionFilter Permission { get; set; }

    public EndpointPermissionFilter() { }

    public EndpointPermissionFilter(EndpointFilter endpoint = null, PermissionFilter permission = null)
    {
      Endpoint = endpoint;
      Permission = permission;
    }
  }
}