// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Website
{
  public static class MemberSelectorFormViewModelFactory
  {
    public static MemberSelectorFormViewModel Create(HttpContext httpContext, IEnumerable<Class> classes, int? memberId)
    {
      Dictionary<ClassViewModel, IEnumerable<MemberViewModel>> membersByClasses = new Dictionary<ClassViewModel, IEnumerable<MemberViewModel>>();
      IStringLocalizer<MemberSelectorFormViewModel> localizer = httpContext.GetStringLocalizer<MemberSelectorFormViewModel>();

      foreach (Class @class in classes)
        membersByClasses.Add(
          ClassViewModelFactory.Create(@class),
          @class.Members.Select(MemberViewModelFactory.Create)
        );

      return new MemberSelectorFormViewModel()
      {
        TableColumns = new[] {
          new TableTagHelper.Column(localizer["Class"]),
          new TableTagHelper.Column(localizer["Name"])
        },
        MembersByClasses = membersByClasses,
        MemberId = memberId
      };
    }
  }
}