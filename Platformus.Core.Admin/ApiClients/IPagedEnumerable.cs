// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Admin.ApiServices;

public interface IPagedEnumerable<T> : IEnumerable<T>
{
  int TotalCount { get; }
  int Offset { get; }
  int Limit { get; }
  int ActivePageNumber => this.Offset / this.Limit + 1;
  int TotalPages => (int)Math.Ceiling((double)this.TotalCount / this.Limit);
}