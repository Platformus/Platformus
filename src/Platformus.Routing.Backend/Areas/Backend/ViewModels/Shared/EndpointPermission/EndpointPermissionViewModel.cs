// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Backend.ViewModels.Shared;

namespace Platformus.Routing.Backend.ViewModels.Shared
{
  public class EndpointPermissionViewModel : ViewModelBase
  {
    public PermissionViewModel Permission { get; set; }
    public bool IsAssigned { get; set; }
  }
}