// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Backend;

public abstract class TextFieldTagHelperBase<T> : FieldTagHelperBase<T>
{
  public int? MinimumLength { get; set; }
  public int? MaximumLength { get; set; }

  protected bool IsLengthLimited()
  {
    return this.For == null ? (this.MinimumLength != null || this.MaximumLength != null) : this.For.HasStringLengthAttribute();
  }

  protected int? GetMinimumLength()
  {
    return this.For == null ? this.MinimumLength : this.For.GetMinStringLength();
  }

  protected int? GetMaximumLength()
  {
    return this.For == null ? this.MaximumLength : this.For.GetMaxStringLength();
  }

  protected override Validation GetValidation()
  {
    return ValidationFactory.Create(
      this.ViewContext,
      isRequired: this.IsRequired(),
      minLength: this.IsLengthLimited() ? this.GetMinimumLength() : null,
      maxLength: this.IsLengthLimited() ? this.GetMaximumLength() : null,
      isValid: this.IsValid()
    );
  }
}