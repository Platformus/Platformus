// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platformus.Website.Frontend.Services.Abstractions;

public interface IEndpointResolver
{
  Task<Data.Entities.Endpoint> ResolveAsync(HttpContext httpContext);
}