// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Events;
using Platformus.Barebone;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Events
{
  public interface ICultureDeletedEventHandler : IEventHandler<IRequestHandler, Culture>
  {
  }
}