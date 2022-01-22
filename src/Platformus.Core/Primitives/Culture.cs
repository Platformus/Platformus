// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Primitives
{
  /// <summary>
  /// Describes a culture.
  /// </summary>
  public class Culture
  {
    /// <summary>
    /// A culture ID. Two-letter language code (ISO 639‑1, example: en, ru, uk).
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Culture"/> class.
    /// </summary>
    /// <param name="id">A culture ID. Two-letter language code (ISO 639‑1, example: en, ru, uk).</param>
    public Culture(string id)
    {
      this.Id = id;
    }
  }
}