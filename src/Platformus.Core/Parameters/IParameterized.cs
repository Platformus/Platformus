// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Parameters;

namespace Platformus.Core
{
  /// <summary>
  /// Describes something that can be parameterized using the <see cref="ParameterGroup"/> and <see cref="Parameter"/>.
  /// </summary>
  public interface IParameterized
  {
    /// <summary>
    /// Gets description.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets the parameter groups with the parameters.
    /// </summary>
    IEnumerable<ParameterGroup> ParameterGroups { get; }
  }
}