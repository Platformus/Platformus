// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Services.Abstractions
{
  public interface IEndpointResolver
  {
    Endpoint GetEndpoint(IRequestHandler requestHandler, string url);
    IEnumerable<KeyValuePair<string, string>> GetArguments(string urlTemplate, string url);
  }
}