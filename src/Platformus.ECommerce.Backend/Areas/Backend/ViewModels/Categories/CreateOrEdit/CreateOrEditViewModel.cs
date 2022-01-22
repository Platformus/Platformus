// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Core.Backend;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;

namespace Platformus.ECommerce.Backend.ViewModels.Categories
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }

    [Display(Name = "URL")]
    [Required]
    [StringLength(128)]
    [RegularExpression(@"^\/$|^(\/[a-z0-9-]+)*$")]
    public string Url { get; set; }

    [Multilingual]
    [Display(Name = "Name")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }
    public IEnumerable<Localization> NameLocalizations { get; set; }

    [Multilingual]
    [Display(Name = "Description")]
    public string Description { get; set; }
    public IEnumerable<Localization> DescriptionLocalizations { get; set; }

    [Display(Name = "Position")]
    public int? Position { get; set; }

    [Multilingual]
    [Display(Name = "Title")]
    [StringLength(128)]
    public string Title { get; set; }
    public IEnumerable<Localization> TitleLocalizations { get; set; }

    [Multilingual]
    [Display(Name = "META description")]
    [StringLength(512)]
    public string MetaDescription { get; set; }
    public IEnumerable<Localization> MetaDescriptionLocalizations { get; set; }

    [Multilingual]
    [Display(Name = "META keywords")]
    [StringLength(256)]
    public string MetaKeywords { get; set; }
    public IEnumerable<Localization> MetaKeywordsLocalizations { get; set; }

    [Display(Name = "Product provider C# class name")]
    [Required]
    [StringLength(128)]
    public string ProductProviderCSharpClassName { get; set; }
    public IEnumerable<Option> ProductProviderCSharpClassNameOptions { get; set; }
    public string ProductProviderParameters { get; set; }
  }
}