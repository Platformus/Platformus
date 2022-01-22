// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class NumericBoxGenerator
  {
    public static TagBuilder Generate(string identity, string type, string value = null, Validation validation = null)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("numeric-box");
      tb.MergeAttribute(AttributeNames.Id, identity);

      if (validation?.IsRequired == true)
        tb.AddRequiredAttributes(validation.IsRequiredValidationErrorMessage);

      if (validation?.IsValid == false)
        tb.AddCssClass("input-validation-error");

      tb.InnerHtml.AppendHtml(GenerateMinusButton());
      tb.InnerHtml.AppendHtml(GenerateTextBox(identity, type, value, validation));
      tb.InnerHtml.AppendHtml(GeneratePlusButton());
      return tb;
    }

    private static TagBuilder GenerateMinusButton()
    {
      TagBuilder tb = new TagBuilder(TagNames.A);

      tb.AddCssClass("numeric-box__button numeric-box__button--minus");
      return tb;
    }
    
    private static TagBuilder GenerateTextBox(string identity, string type, string value, Validation validation)
    {
      TagBuilder tb = new TagBuilder(TagNames.Input);

      tb.TagRenderMode = TagRenderMode.SelfClosing;
      tb.AddCssClass("numeric-box__text-box text-box");
      tb.MergeAttribute(AttributeNames.Name, identity);
      tb.MergeAttribute(AttributeNames.Type, InputTypes.Text);
      tb.MergeAttribute(AttributeNames.Value, value);
      tb.MergeAttribute(AttributeNames.DataType, type);

      if (validation?.IsRequired == true)
        tb.AddRequiredAttributes(validation.IsRequiredValidationErrorMessage);

      return tb;
    }

    private static TagBuilder GeneratePlusButton()
    {
      TagBuilder tb = new TagBuilder(TagNames.A);

      tb.AddCssClass("numeric-box__button numeric-box__button--plus");
      return tb;
    }
  }
}