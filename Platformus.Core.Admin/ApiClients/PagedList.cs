// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Admin.ApiServices;

public class PagedList<T> : List<T>, IPagedEnumerable<T>
{
  public PagedList(IEnumerable<T> items, int totalCount, int offset, int limit) : base(items)
  {
    this.TotalCount = totalCount;
    this.Offset = offset;
    this.Limit = limit;
  }

  public int TotalCount { get; set;  }
  public int Offset { get; }
  public int Limit { get; }
}