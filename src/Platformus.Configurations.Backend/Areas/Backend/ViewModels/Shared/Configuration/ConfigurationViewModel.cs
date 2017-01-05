// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.Configurations.Backend.ViewModels.Shared
{
  public class ConfigurationViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<VariableViewModel> Variables { get; set; }
  }
}