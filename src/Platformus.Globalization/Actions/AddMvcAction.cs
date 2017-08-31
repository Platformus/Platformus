// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Platformus.Globalization.Actions
{
  public class AddMvcAction : IAddMvcAction
  {
    public int Priority => 4000;

    public void Execute(IMvcBuilder mvcBuilder, IServiceProvider serviceProvider)
    {
      mvcBuilder
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization();
    }
  }
}