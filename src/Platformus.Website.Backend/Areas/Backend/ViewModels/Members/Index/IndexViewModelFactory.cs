// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Members
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(MemberFilter filter, string sorting, int offset, int limit, int total, IEnumerable<Member> members)
    {
      return new IndexViewModel()
      {
        Filter = filter,
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total,
        Members = members.Select(MemberViewModelFactory.Create)
      };
    }
  }
}