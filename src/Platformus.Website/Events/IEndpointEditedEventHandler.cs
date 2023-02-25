// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Events;
using Microsoft.AspNetCore.Http;

namespace Platformus.Website.Events;

/// <summary>
/// Describes an event handler that will be automatically executed just after a <see cref="Data.Entities.Endpoint"/> is edited.
/// </summary>
public interface IEndpointEditedEventHandler : IEventHandler<HttpContext, Data.Entities.Endpoint, Data.Entities.Endpoint>
{
}