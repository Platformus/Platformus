﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Platformus.Core.Actions
{
  public class AddMvcLocalizationAction : IAddMvcAction
  {
    public int Priority => 1000;

    public void Execute(IMvcBuilder mvcBuilder, IServiceProvider serviceProvider)
    {
      mvcBuilder
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization();
    }
  }
}