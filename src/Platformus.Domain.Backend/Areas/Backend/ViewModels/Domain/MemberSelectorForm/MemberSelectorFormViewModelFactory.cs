// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Domain
{
  public class MemberSelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public MemberSelectorFormViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public MemberSelectorFormViewModel Create(int? memberId)
    {
      Dictionary<ClassViewModel, IEnumerable<MemberViewModel>> membersByClasses = new Dictionary<ClassViewModel, IEnumerable<MemberViewModel>>();
      IStringLocalizer<MemberSelectorFormViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<MemberSelectorFormViewModelFactory>>();

      foreach (Class @class in this.RequestHandler.Storage.GetRepository<IClassRepository>().All().ToList())
        membersByClasses.Add(
          new ClassViewModelFactory(this.RequestHandler).Create(@class),
          this.RequestHandler.Storage.GetRepository<IMemberRepository>().FilteredByClassId(@class.Id).ToList().Select(
            m => new MemberViewModelFactory(this.RequestHandler).Create(m)
          )
        );

      return new MemberSelectorFormViewModel()
      {
        GridColumns = new[] {
          new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Class"]),
          new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Name"])
        },
        MembersByClasses = membersByClasses,
        MemberId = memberId
      };
    }
  }
}