// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Services.Abstractions;

/// <summary>
/// Describes a configuration manager to read the user-defined configuration.
/// </summary>
public interface IConfigurationManager
{
  /// <summary>
  /// Gets a variable value.
  /// </summary>
  /// <param name="configurationCode">A configuration code the variable belongs to.</param>
  /// <param name="variableCode">A variable code.</param>
  string this[string configurationCode, string variableCode] { get; }

  /// <summary>
  /// Invalidates the variable values cache.
  /// </summary>
  void InvalidateCache();
}