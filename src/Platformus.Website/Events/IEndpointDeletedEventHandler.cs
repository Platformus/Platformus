// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Events;
using Microsoft.AspNetCore.Http;

namespace Platformus.Website.Events
{
  public interface IEndpointDeletedEventHandler : IEventHandler<HttpContext, Data.Entities.Endpoint>
  {
  }
}