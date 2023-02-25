// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared;

public static class CompletedFormViewModelFactory
{
  public static CompletedFormViewModel Create(CompletedForm completedForm)
  {
    return new CompletedFormViewModel()
    {
      Id = completedForm.Id,
      Form = FormViewModelFactory.Create(completedForm.Form),
      Created = completedForm.Created
    };
  }
}