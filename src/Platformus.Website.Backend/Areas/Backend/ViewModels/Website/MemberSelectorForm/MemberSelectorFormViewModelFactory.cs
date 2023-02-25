// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Website;

public static class MemberSelectorFormViewModelFactory
{
  public static MemberSelectorFormViewModel Create(IEnumerable<Class> classes, int? memberId)
  {
    Dictionary<ClassViewModel, IEnumerable<MemberViewModel>> membersByClasses = new Dictionary<ClassViewModel, IEnumerable<MemberViewModel>>();

    foreach (Class @class in classes)
      membersByClasses.Add(
        ClassViewModelFactory.Create(@class),
        @class.Members.Select(MemberViewModelFactory.Create).ToList()
      );

    return new MemberSelectorFormViewModel()
    {
      MembersByClasses = membersByClasses,
      MemberId = memberId
    };
  }
}