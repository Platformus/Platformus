// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Primitives;

namespace Platformus.Core.Parameters;

/// <summary>
/// Parameters are used when it is required to build the client-side controls to get some
/// configurations from a user. Each parameter is rendered on the client-side using the corresponding editor.
/// User input then can be parsed using the <see cref="ParametersParser"/> class.
/// </summary>
public class Parameter
{
  /// <summary>
  /// Used as the control's ID and name on the client-side.
  /// </summary>
  public string Code { get; set; }

  /// <summary>
  /// Used as the control's label on the client-side.
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// One of the supported parameter editor codes listed in <see cref="ParameterEditorCodes"/> class.
  /// </summary>
  public string ParameterEditorCode { get; set; }

  /// <summary>
  /// Possible options for the controls like drop down lists or radio button lists.
  /// </summary>
  public IEnumerable<Option> Options { get; set; }

  /// <summary>
  /// A default value visible to a user before a different one is selected.
  /// </summary>
  public string DefaultValue { get; set; }

  /// <summary>
  /// A value indicating that the parameter is required and should be set by a user.
  /// </summary>
  public bool IsRequired { get; set; }

  /// <summary>
  /// Initializes a new instance of the <see cref="Parameter"/> class.
  /// </summary>
  /// <param name="code">Used as the control's ID and name on the client-side.</param>
  /// <param name="name">Used as the control's label on the client-side.</param>
  /// <param name="parameterEditorCode">One of the supported parameter editor codes listed in <see cref="ParameterEditorCodes"/> class.</param>
  /// <param name="defaultValue">A default value visible to a user before a different one is selected.</param>
  /// <param name="isRequired">A value indicating that the parameter is required and should be set by a user.</param>
  public Parameter(string code, string name, string parameterEditorCode, string defaultValue = null, bool isRequired = false)
  {
    this.Code = code;
    this.Name = name;
    this.ParameterEditorCode = parameterEditorCode;
    this.DefaultValue = defaultValue;
    this.IsRequired = isRequired;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Parameter"/> class.
  /// </summary>
  /// <param name="code">Used as the control's ID and name on the client-side.</param>
  /// <param name="name">Used as the control's label on the client-side.</param>
  /// <param name="parameterEditorCode">One of the supported parameter editor codes listed in <see cref="ParameterEditorCodes"/> class.</param>
  /// <param name="options">Possible options for the controls like drop down lists or radio button lists.</param>
  /// <param name="defaultValue">A default value visible to a user before a different one is selected.</param>
  /// <param name="isRequired">A value indicating that the parameter is required and should be set by a user.</param>
  public Parameter(string code, string name, string parameterEditorCode, IEnumerable<Option> options, string defaultValue = null, bool isRequired = false)
    : this(code, name, parameterEditorCode, defaultValue, isRequired)
  {
    this.Options = options;
  }
}