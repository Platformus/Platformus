﻿// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Events;
using Platformus.Core;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Events
{
  public interface IAttributeEditedEventHandler : IEventHandler<IRequestHandler, Attribute>
  {
  }
}