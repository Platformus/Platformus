// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Platformus.Core.Data.Entities;

namespace Platformus.Core.Services.Abstractions
{
  /// <summary>
  /// Describes a culture manager to get the user-defined globalization parameters.
  /// </summary>
  public interface ICultureManager
  {
    /// <summary>
    /// Gets a culture by ID.
    /// </summary>
    /// <param name="id">A culture ID. Two-letter language code (ISO 639‑1, example: en, ru, uk).</param>
    Task<Culture> GetCultureAsync(string id);

    /// <summary>
    /// Gets a neutral culture (code = "__").
    /// Neutral cultures are used to store the non-localizable content.
    /// </summary>
    Task<Culture> GetNeutralCultureAsync();

    /// <summary>
    /// Gets a culture that should be used on the frontend by default.
    /// </summary>
    Task<Culture> GetFrontendDefaultCultureAsync();

    /// <summary>
    /// Gets a culture that should be used on the backend by default.
    /// </summary>
    Task<Culture> GetBackendDefaultCultureAsync();

    /// <summary>
    /// Gets a current culture (default one or selected by a user).
    /// </summary>
    Task<Culture> GetCurrentCultureAsync();

    /// <summary>
    /// Gets all the cultures.
    /// </summary>
    Task<IEnumerable<Culture>> GetCulturesAsync();

    /// <summary>
    /// Gets all the cultures except the neutral one.
    /// Neutral cultures are used to store the non-localizable content.
    /// </summary>
    Task<IEnumerable<Culture>> GetNotNeutralCulturesAsync();

    /// <summary>
    /// Invalidates the cultures cache.
    /// </summary>
    void InvalidateCache();
  }
}