// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Dynamic;

namespace Platformus
{
  /// <summary>
  /// Builds a dynamic object by adding properties to it.
  /// </summary>
  public class ExpandoObjectBuilder
  {
    private ExpandoObject expandoObject;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpandoObjectBuilder"/> class
    /// and starts building a new dynamic object.
    /// </summary>
    public ExpandoObjectBuilder()
    {
      this.expandoObject = new ExpandoObject();
    }

    /// <summary>
    /// Adds a property to the current dynamic object.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public ExpandoObjectBuilder AddProperty(string key, dynamic value)
    {
      (this.expandoObject as IDictionary<string, dynamic>).Add(key, value);
      return this;
    }

    /// <summary>
    /// Stops building and returns the created dynamic object.
    /// </summary>
    /// <returns></returns>
    public dynamic Build()
    {
      return this.expandoObject;
    }
  }
}