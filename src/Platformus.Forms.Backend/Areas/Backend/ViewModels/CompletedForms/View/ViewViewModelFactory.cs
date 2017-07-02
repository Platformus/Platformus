// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Forms.Backend.ViewModels.Shared;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.CompletedForms
{
  public class ViewViewModelFactory : ViewModelFactoryBase
  {
    public ViewViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ViewViewModel Create(int id)
    {
      CompletedForm completedForm = this.RequestHandler.Storage.GetRepository<ICompletedFormRepository>().WithKey((int)id);

      return new ViewViewModel()
      {
        Id = completedForm.Id,
        CompletedFields = this.RequestHandler.Storage.GetRepository<ICompletedFieldRepository>().FilteredByCompletedFormId(completedForm.Id).Select(
          cf => new CompletedFieldViewModelFactory(this.RequestHandler).Create(cf)
        )
      };
    }
  }
}