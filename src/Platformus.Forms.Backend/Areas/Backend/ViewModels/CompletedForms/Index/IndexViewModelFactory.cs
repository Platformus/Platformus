// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Forms.Backend.ViewModels.Shared;
using Platformus.Forms.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.CompletedForms
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(int formId, string orderBy, string direction, int skip, int take)
    {
      ICompletedFormRepository completedFormRepository = this.RequestHandler.Storage.GetRepository<ICompletedFormRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, completedFormRepository.Count(formId),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create("Form"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Created", "created"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          completedFormRepository.Range(formId, orderBy, direction, skip, take).Select(cf => new CompletedFormViewModelFactory(this.RequestHandler).Create(cf)),
          "_CompletedForm"
        )
      };
    }
  }
}