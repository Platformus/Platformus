// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public static class RelationSetViewModelFactory
  {
    public static RelationSetViewModel Create(Member member, IEnumerable<Relation> relations)
    {
      return new RelationSetViewModel()
      {
        Member = MemberViewModelFactory.Create(member),
        PrimaryIds = relations.Select(r => r.PrimaryId).ToList()
      };
    }
  }
}