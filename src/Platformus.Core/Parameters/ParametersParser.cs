// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Platformus.Core.Parameters;

/// <summary>
/// Parses a "key1=value1;key2=value2;key3=value3"-like string and provides methods to get values by keys.
/// </summary>
public class ParametersParser
{
  private IDictionary<string, string> parsedParameters;

  /// <summary>
  /// Gets the parsed parameters as dictionary.
  /// </summary>
  public IDictionary<string, string> ParsedParameters
  {
    get => this.parsedParameters;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ParametersParser"/> class
  /// and parses the provided <paramref name="parameters"/> string into the <see cref="ParsedParameters"/>.
  /// </summary>
  /// <param name="parameters"></param>
  public ParametersParser(string parameters)
  {
    this.ParseParameters(parameters);
  }

  /// <summary>
  /// Gets an <see cref="int"/> parameter value by code.
  /// </summary>
  /// <param name="parameterCode">A parameter code to get value of.</param>
  /// <param name="defaultValue">A default value that is returned if the parameter value is missing or fails to be parsed.</param>
  public int GetIntParameterValue(string parameterCode, int defaultValue = 0)
  {
    if (this.parsedParameters.ContainsKey(parameterCode) && int.TryParse(this.parsedParameters[parameterCode], out int result))
      return result;

    return defaultValue;
  }

  /// <summary>
  /// Gets an <see cref="decimal"/> parameter value by code.
  /// </summary>
  /// <param name="parameterCode">A parameter code to get value of.</param>
  /// <param name="defaultValue">A default value that is returned if the parameter value is missing or fails to be parsed.</param>
  public decimal GetDecimalParameterValue(string parameterCode, decimal defaultValue = 0)
  {
    if (this.parsedParameters.ContainsKey(parameterCode) && decimal.TryParse(this.parsedParameters[parameterCode], out decimal result))
      return result;

    return defaultValue;
  }

  /// <summary>
  /// Gets an <see cref="bool"/> parameter value by code.
  /// </summary>
  /// <param name="parameterCode">A parameter code to get value of.</param>
  /// <param name="defaultValue">A default value that is returned if the parameter value is missing or fails to be parsed.</param>
  public bool GetBoolParameterValue(string parameterCode, bool defaultValue = false)
  {
    if (this.parsedParameters.ContainsKey(parameterCode) && bool.TryParse(this.parsedParameters[parameterCode], out bool result))
      return result;

    return defaultValue;
  }

  /// <summary>
  /// Gets an <see cref="string"/> parameter value by code.
  /// </summary>
  /// <param name="parameterCode">A parameter code to get value of.</param>
  /// <param name="defaultValue">A default value that is returned if the parameter value is missing.</param>
  public string GetStringParameterValue(string parameterCode, string defaultValue = null)
  {
    if (this.parsedParameters.ContainsKey(parameterCode))
      return this.parsedParameters[parameterCode];

    return defaultValue;
  }

  private void ParseParameters(string parameters)
  {
    List<KeyValuePair<string, string>> parsedParameters = new List<KeyValuePair<string, string>>();

    if (!string.IsNullOrEmpty(parameters))
    {
      foreach (string parameter in parameters.Split(';').Select(p => p.Trim()))
      {
        if (!string.IsNullOrEmpty(parameter))
        {
          string[] operands = parameter.Trim().Split('=');

          parsedParameters.Add(new KeyValuePair<string, string>(operands[0], operands[1]));
        }
      }
    }

    this.parsedParameters = new Dictionary<string, string>(parsedParameters);
  }
}