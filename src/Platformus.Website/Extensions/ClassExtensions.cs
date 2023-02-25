// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Data.Entities;

namespace Platformus.Website;

/// <summary>
/// Contains the extension methods of the <see cref="Class"/>.
/// </summary>
public static class ClassExtensions
{
  /// <summary>
  /// Gets all the tabs of the given class and its parent one.
  /// </summary>
  /// <param name="class">A class to get tabs of.</param>
  public static IEnumerable<Tab> GetTabs(this Class @class)
  {
    List<Tab> tabs = new List<Tab>();

    tabs.AddRange(@class.Tabs);

    if (@class.Parent != null)
      tabs.AddRange(@class.Parent.Tabs);

    return tabs.OrderBy(m => m.Position);
  }

  /// <summary>
  /// Gets all the members of the given class and its parent one.
  /// </summary>
  /// <param name="class">A class to get members of.</param>
  public static IEnumerable<Member> GetMembers(this Class @class)
  {
    List<Member> members = new List<Member>();

    members.AddRange(@class.Members);

    if (@class.Parent != null)
      members.AddRange(@class.Parent.Members);

    return members.OrderBy(m => m.Position).ToList();
  }

  /// <summary>
  /// Gets all the members of the given class and its parent one that marked as visible in list.
  /// "Visible in list" means that the member's value should be displayed in the object list in the backend.
  /// </summary>
  /// <param name="class">A class to get members of.</param>
  public static IEnumerable<Member> GetVisibleInListMembers(this Class @class)
  {
    return @class.GetMembers().Where(m => m.IsPropertyVisibleInList == true).ToList();
  }

  /// <summary>
  /// Gets all the members of the given class and its parent one that marked as relation single parent.
  /// "Relation single parent" means that the object has the single parent relation (for example a category for the post).
  /// </summary>
  /// <param name="class">A class to get members of.</param>
  public static IEnumerable<Member> GetRelationSingleParentMembers(this Class @class)
  {
    return @class.GetMembers().Where(m => m.IsRelationSingleParent == true).ToList();
  }
}