// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Events;
using Platformus.Barebone;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Events
{
  public interface IRoleDeletedEventHandler : IEventHandler<IRequestHandler, Role>
  {
  }
}