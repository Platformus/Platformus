// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Entities;
using Platformus.Domain.Events;

namespace Platformus.Domain.EventHandlers
{
  public class MemberDeletedEventHandler : IMemberDeletedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, Member @object)
    {
      requestHandler.GetService<IDomainManager>().InvalidateCache();
    }
  }
}