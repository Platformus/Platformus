// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Infrastructure;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class BackendScriptViewModelBuilder : ViewModelBuilderBase
  {
    public BackendScriptViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public BackendScriptViewModel Build(BackendScript backendScript)
    {
      return new BackendScriptViewModel()
      {
        Url = backendScript.Url,
        Position = backendScript.Position
      };
    }
  }
}