// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Core.Backend.ViewModels.Shared;

namespace Platformus.Core.Backend;

public class PagerTagHelper : TagHelper
{
  private IHtmlHelper htmlHelper;

  [ViewContext]
  [HtmlAttributeNotBound]
  public ViewContext ViewContext { get; set; }
  public int Offset { get; set; }
  public int Limit { get; set; }
  public int Total { get; set; }

  public PagerTagHelper(IHtmlHelper htmlHelper)
  {
    this.htmlHelper = htmlHelper;
  }

  public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
  {
    (this.htmlHelper as IViewContextAware).Contextualize(this.ViewContext);
    output.SuppressOutput();
    output.Content.SetHtmlContent(await this.htmlHelper.PartialAsync("_Pager", PagerViewModelFactory.Create(this.ViewContext.HttpContext, this.Offset, this.Limit, this.Total)));
  }
}