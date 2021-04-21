// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend
{
  public class ValidationErrorMessageGenerator : GeneratorBase
  {
    public TagBuilder GenerateValidationErrorMessage(ModelExpression modelExpression, Localization localization = null)
    {
      TagBuilder tb = new TagBuilder("span");

      tb.AddCssClass("field__validation-error");
      tb.AddCssClass("field-validation-valid");
      tb.AddCssClass("validation-error");
      tb.MergeAttribute("data-valmsg-for", this.GetIdentity(modelExpression, localization));
      tb.MergeAttribute("data-valmsg-replace", "true");
      return tb;
    }
  }
}