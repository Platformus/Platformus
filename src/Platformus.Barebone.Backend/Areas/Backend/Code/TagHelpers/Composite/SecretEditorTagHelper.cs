// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Razor.TagHelpers;

namespace Platformus.Barebone.Backend
{
  [HtmlTargetElement("secret-editor", Attributes = ForAttributeName)]
  public class SecretEditorTagHelper : TextBoxTagHelperBase
  {
    private const string ForAttributeName = "asp-for";

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    [HtmlAttributeName(ForAttributeName)] 
    public ModelExpression For { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null)
        return;

      output.SuppressOutput();
      output.Content.Clear();
      output.Content.Append(this.GenerateField());
    }

    private TagBuilder GenerateField()
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("field");
      tb.InnerHtml.Clear();
      tb.InnerHtml.Append(
        new CompositeHtmlContent(
          this.GenerateLabel(this.For),
          this.GenerateInput(this.ViewContext, this.For, null, "password")
        )
      );

      return tb;
    }
  }
}