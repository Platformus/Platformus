// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Platformus.Core.Backend.Actions;

public class AddLocalizedValidationMetadataProviderAction : IAddMvcAction
{
  public int Priority => 1000;

  public void Execute(IMvcBuilder mvcBuilder, IServiceProvider serviceProvider)
  {
    IHttpContextAccessor httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
    IStringLocalizerFactory localizerFactory = serviceProvider.GetService<IStringLocalizerFactory>();
    IStringLocalizer localizer = localizerFactory.Create(typeof(SharedResource));

    mvcBuilder.AddMvcOptions(options =>
    {
      options.ModelMetadataDetailsProviders.Add(new LocalizedValidationMetadataProvider(httpContextAccessor, localizer));
    });
  }
}