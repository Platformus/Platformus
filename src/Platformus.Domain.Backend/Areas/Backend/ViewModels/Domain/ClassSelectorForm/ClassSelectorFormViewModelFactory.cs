// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Domain
{
  public class ClassSelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public ClassSelectorFormViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ClassSelectorFormViewModel Create(int? classId)
    {
      IStringLocalizer<ClassSelectorFormViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<ClassSelectorFormViewModelFactory>>();

      return new ClassSelectorFormViewModel()
      {
        GridColumns = new[] {
          new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Parent Class"]),
          new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Name"])
        },
        Classes = this.RequestHandler.Storage.GetRepository<IClassRepository>().All().ToList().Select(
          c => new ClassViewModelFactory(this.RequestHandler).Create(c)
        ),
        ClassId = classId
      };
    }
  }
}