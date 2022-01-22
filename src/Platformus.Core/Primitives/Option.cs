// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Core.Primitives
{
  /// <summary>
  /// Describes an option for the controls like drop down list.
  /// </summary>
  public class Option
  {
    /// <summary>
    /// A text displayed to a user.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// An option value that is used as the option selection result.
    /// If value is null, the <see cref="Option.Text"/> will be used instead.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// The child options.
    /// </summary>
    public IEnumerable<Option> Options { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Option"/> class.
    /// </summary>
    /// <param name="text">A text displayed to a user.</param>
    /// <param name="value">An option value that is used as the option selection result. If value is null, the <see cref="Option.Text"/> will be used instead.</param>
    /// <param name="options">The child options.</param>
    public Option(string text = null, string value = null, IEnumerable<Option> options = null)
    {
      this.Text = text;
      this.Value = value ?? text;
      this.Options = options;
    }
  }
}