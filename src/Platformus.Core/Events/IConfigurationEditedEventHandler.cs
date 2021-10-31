// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Events;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Data.Entities;

namespace Platformus.Core.Events
{
  /// <summary>
  /// Describes an event handler that will be automatically executed just after a <see cref="Configuration"/> is edited.
  /// </summary>
  public interface IConfigurationEditedEventHandler : IEventHandler<HttpContext, Configuration>
  {
  }
}