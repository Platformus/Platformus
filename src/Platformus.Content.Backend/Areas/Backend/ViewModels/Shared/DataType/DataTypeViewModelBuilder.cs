// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Shared
{
  public class DataTypeViewModelBuilder : ViewModelBuilderBase
  {
    public DataTypeViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public DataTypeViewModel Build(DataType dataType)
    {
      return new DataTypeViewModel()
      {
        Id = dataType.Id,
        JavaScriptEditorClassName = dataType.JavaScriptEditorClassName,
        Name = dataType.Name,
        Position = dataType.Position
      };
    }
  }
}