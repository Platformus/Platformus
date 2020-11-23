// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Data.Entities;

namespace Platformus.Website
{
  public static class ClassExtensions
  {
    public static IEnumerable<Tab> GetTabs(this Class @class)
    {
      List<Tab> tabs = new List<Tab>();

      tabs.AddRange(@class.Tabs);

      if (@class.Parent != null)
        tabs.AddRange(@class.Parent.Tabs);

      return tabs.OrderBy(m => m.Position);
    }

    public static IEnumerable<Member> GetMembers(this Class @class)
    {
      List<Member> members = new List<Member>();

      members.AddRange(@class.Members);

      if (@class.Parent != null)
        members.AddRange(@class.Parent.Members);

      return members.OrderBy(m => m.Position);
    }

    public static IEnumerable<Member> GetVisibleInListMembers(this Class @class)
    {
      List<Member> members = new List<Member>();

      members.AddRange(@class.Members);

      if (@class.Parent != null)
        members.AddRange(@class.Parent.Members);

      return members
        .Where(m => m.IsPropertyVisibleInList == true || m.IsRelationSingleParent == true)
        .OrderBy(m => m.Position);
    }
  }
}