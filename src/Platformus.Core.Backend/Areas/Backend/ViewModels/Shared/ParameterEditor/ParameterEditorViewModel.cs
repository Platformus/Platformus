// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Parameters;

namespace Platformus.Core.Backend.ViewModels.Shared;

public class ParameterEditorViewModel : ViewModelBase
{
  public string Description { get; set; }
  public IEnumerable<ParameterGroup> ParameterGroups { get; set; }
}