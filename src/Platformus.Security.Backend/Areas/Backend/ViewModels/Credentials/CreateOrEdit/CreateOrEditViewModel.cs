// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Primitives;

namespace Platformus.Security.Backend.ViewModels.Credentials
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }
    public int UserId { get; set; }

    [Display(Name = "Credential type")]
    [Required]
    public int CredentialTypeId { get; set; }
    public IEnumerable<Option> CredentialTypeOptions { get; set; }

    [Display(Name = "Identifier")]
    [Required]
    [StringLength(64)]
    public string Identifier { get; set; }

    [Display(Name = "Secret")]
    [StringLength(1024)]
    public string Secret { get; set; }

    [Display(Name = "Apply PBKDF2 hashing to secret")]
    public bool ApplyPbkdf2HashingToSecret { get; set; }
  }
}