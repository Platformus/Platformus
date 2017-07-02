// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Shared
{
  public class CompletedFormViewModelFactory : ViewModelFactoryBase
  {
    public CompletedFormViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CompletedFormViewModel Create(CompletedForm completedForm)
    {
      return new CompletedFormViewModel()
      {
        Id = completedForm.Id,
        Form = new FormViewModelFactory(this.RequestHandler).Create(
          this.RequestHandler.Storage.GetRepository<IFormRepository>().WithKey(completedForm.FormId)
        ),
        Created = completedForm.Created
      };
    }
  }
}