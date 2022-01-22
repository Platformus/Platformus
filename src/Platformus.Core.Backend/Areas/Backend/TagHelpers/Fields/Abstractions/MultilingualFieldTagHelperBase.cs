// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend
{
  public abstract class MultilingualFieldTagHelperBase<T> : TextFieldTagHelperBase<T>
  {
    public IEnumerable<Localization> Localizations { get; set; }

    protected TagBuilder CreateCulture(Localization localization)
    {
      return FieldGenerator.GenerateCulture(localization);
    }

    protected virtual Validation GetValidation(Localization localization = null)
    {
      return ValidationFactory.Create(
        this.ViewContext,
        isRequired: this.IsRequired(),
        minLength: this.IsLengthLimited() ? this.GetMinimumLength() : null,
        maxLength: this.IsLengthLimited() ? this.GetMaximumLength() : null,
        isValid: this.IsValid(localization)
      );
    }
  }
}