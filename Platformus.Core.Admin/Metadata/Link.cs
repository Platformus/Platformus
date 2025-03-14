// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Admin;

public class Link
{
  public string? Url { get; set; }
  public string? Label { get; set; }
  public int Position { get; set; }
  public IEnumerable<Link>? Links { get; set; }
}
