// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Localization;

namespace Platformus.Core.Backend
{
  public class LocalizedValidationMetadataProvider : IValidationMetadataProvider
  {
    private IHttpContextAccessor httpContextAccessor;
    private IStringLocalizer localizer;

    public LocalizedValidationMetadataProvider(IHttpContextAccessor httpContextAccessor, IStringLocalizer localizer)
    {
      this.httpContextAccessor = httpContextAccessor;
      this.localizer = localizer;
    }

    public void CreateValidationMetadata(ValidationMetadataProviderContext context)
    {
      if (!httpContextAccessor.HttpContext.Request.Path.StartsWithSegments("/backend")) return;

      foreach (object attribute in context.ValidationMetadata.ValidatorMetadata)
        if (attribute is ValidationAttribute validationAttribute)
          ValidationAttributeLocalizer.Localize(validationAttribute, localizer);
    }
  }
}