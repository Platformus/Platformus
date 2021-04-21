// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Core.Backend;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;

namespace Platformus.Website.Backend.ViewModels.Forms
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }

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
    [Display(Name = "Submit button title")]
    [Required]
    [StringLength(64)]
    public string SubmitButtonTitle { get; set; }
    public IEnumerable<Localization> SubmitButtonTitleLocalizations { get; set; }

    [Display(Name = "Produce completed forms")]
    [Required]
    public bool ProduceCompletedForms { get; set; }

    [Display(Name = "Form handler C# class name")]
    [Required]
    [StringLength(128)]
    public string FormHandlerCSharpClassName { get; set; }
    public IEnumerable<Option> FormHandlerCSharpClassNameOptions { get; set; }

    public string FormHandlerParameters { get; set; }
    public IEnumerable<dynamic> FormHandlers { get; set; }
  }
}