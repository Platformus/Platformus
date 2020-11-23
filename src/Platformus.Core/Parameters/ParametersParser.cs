// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Platformus.Core.Parameters
{
  public class ParametersParser
  {
    private IDictionary<string, string> parsedParameters;

    public IDictionary<string, string> ParsedParameters
    {
      get => this.parsedParameters;
    }

    public ParametersParser(string parameters)
    {
      this.ParseParameters(parameters);
    }

    public int GetIntParameterValue(string parameterCode, int defaultValue = 0)
    {
      if (this.parsedParameters.ContainsKey(parameterCode) && int.TryParse(this.parsedParameters[parameterCode], out int result))
        return result;

      return defaultValue;
    }

    public decimal GetDecimalParameterValue(string parameterCode, decimal defaultValue = 0)
    {
      if (this.parsedParameters.ContainsKey(parameterCode) && decimal.TryParse(this.parsedParameters[parameterCode], out decimal result))
        return result;

      return defaultValue;
    }

    public bool GetBoolParameterValue(string parameterCode, bool defaultValue = false)
    {
      if (this.parsedParameters.ContainsKey(parameterCode) && bool.TryParse(this.parsedParameters[parameterCode], out bool result))
        return result;

      return defaultValue;
    }

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
}