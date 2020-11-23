// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Website
{
  public class MemberSelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public MemberSelectorFormViewModel Create(HttpContext httpContext, IEnumerable<Class> classes, int? memberId)
    {
      Dictionary<ClassViewModel, IEnumerable<MemberViewModel>> membersByClasses = new Dictionary<ClassViewModel, IEnumerable<MemberViewModel>>();
      IStringLocalizer<MemberSelectorFormViewModelFactory> localizer = httpContext.RequestServices.GetService<IStringLocalizer<MemberSelectorFormViewModelFactory>>();

      foreach (Class @class in classes)
        membersByClasses.Add(
          new ClassViewModelFactory().Create(@class),
          @class.Members.Select(m => new MemberViewModelFactory().Create(m))
        );

      return new MemberSelectorFormViewModel()
      {
        GridColumns = new[] {
          new GridColumnViewModelFactory().Create(localizer["Class"]),
          new GridColumnViewModelFactory().Create(localizer["Name"])
        },
        MembersByClasses = membersByClasses,
        MemberId = memberId
      };
    }
  }
}