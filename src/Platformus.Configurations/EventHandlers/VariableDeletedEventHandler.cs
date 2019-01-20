// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Configurations.Data.Entities;
using Platformus.Configurations.Events;
using Platformus.Configurations.Services.Abstractions;

namespace Platformus.Configurations.EventHandlers
{
  public class VariableDeletedEventHandler : IVariableDeletedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, Variable variable)
    {
      requestHandler.GetService<IConfigurationManager>().InvalidateCache();
    }
  }
}