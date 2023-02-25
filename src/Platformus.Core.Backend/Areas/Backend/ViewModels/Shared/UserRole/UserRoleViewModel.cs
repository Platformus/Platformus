// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Backend.ViewModels.Shared;

public class UserRoleViewModel : ViewModelBase
{
  public RoleViewModel Role { get; set; }
  public bool IsAssigned { get; set; }
}