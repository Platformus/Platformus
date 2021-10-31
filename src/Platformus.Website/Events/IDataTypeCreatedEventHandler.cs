// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Events;
using Microsoft.AspNetCore.Http;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Events
{
  /// <summary>
  /// Describes an event handler that will be automatically executed just after a <see cref="DataType"/> is created.
  /// </summary>
  public interface IDataTypeCreatedEventHandler : IEventHandler<HttpContext, DataType>
  {
  }
}