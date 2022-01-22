// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public static class ParameterEditorViewModelFactory
  {
    public static ParameterEditorViewModel Create(string cSharpClassName)
    {
      IParameterized parameterized = StringActivator.CreateInstance<IParameterized>(cSharpClassName);

      return new ParameterEditorViewModel()
      {
        Description = parameterized.Description,
        ParameterGroups = parameterized.ParameterGroups
      };
    }
  }
}