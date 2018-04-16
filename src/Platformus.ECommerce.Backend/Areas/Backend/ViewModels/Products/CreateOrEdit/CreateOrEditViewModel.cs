// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Barebone.Primitives;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Products
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }

    [Display(Name = "Category")]
    [Required]
    public int CategoryId { get; set; }
    public IEnumerable<Option> CategoryOptions { get; set; }

    [Display(Name = "URL")]
    [Required]
    [StringLength(128)]
    public string Url { get; set; }

    [Display(Name = "Code")]
    [Required]
    [StringLength(32)]
    public string Code { get; set; }

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

    [Display(Name = "Price")]
    [Required]
    public decimal Price { get; set; }

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

    public IEnumerable<PhotoViewModel> Photos { get; set; }
    public string RemovedPhotoIds { get; set; }
  }
}