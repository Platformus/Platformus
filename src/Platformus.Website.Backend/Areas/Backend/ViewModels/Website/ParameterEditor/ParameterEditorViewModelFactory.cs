// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Parameters;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Website
{
  public static class ParameterEditorViewModelFactory
  {
    public static ParameterEditorViewModel Create(IEnumerable<DataTypeParameter> dataTypeParameters)
    {
      return new ParameterEditorViewModel()
      {
        ParameterGroups = new[] {
          new ParameterGroup(
            null,
            dataTypeParameters.Select(dtp => new Parameter(dtp.Code, dtp.Name, dtp.ParameterEditorCode, dtp.DataTypeParameterOptions?.Select(dtpo => new Option(dtpo.Value)))).ToArray())
        }
      };
    }
  }
}