// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class CompletedFormViewModelFactory : ViewModelFactoryBase
  {
    public CompletedFormViewModel Create(CompletedForm completedForm)
    {
      return new CompletedFormViewModel()
      {
        Id = completedForm.Id,
        Form = new FormViewModelFactory().Create(completedForm.Form),
        Created = completedForm.Created
      };
    }
  }
}