// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class UserViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
  }
}