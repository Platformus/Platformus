// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.FormHandlers
{
  public abstract class FormHandlerBase : IFormHandler
  {
    private Form form;
    private Dictionary<string, string> parameterValuesByCodes;

    public virtual IEnumerable<FormHandlerParameterGroup> ParameterGroups => new FormHandlerParameterGroup[] { };
    public virtual string Description => null;

    public IActionResult Handle(IRequestHandler requestHandler, Form form, IDictionary<Field, string> valuesByFields, IDictionary<string, byte[]> attachmentsByFilenames)
    {
      this.form = form;
      return this.GetActionResult(requestHandler, form, valuesByFields, attachmentsByFilenames);
    }

    protected abstract IActionResult GetActionResult(IRequestHandler requestHandler, Form form, IDictionary<Field, string> valuesByFields, IDictionary<string, byte[]> attachmentsByFilenames);

    protected bool HasParameter(string code)
    {
      this.CacheParameterValuesByCodes();
      return this.parameterValuesByCodes.ContainsKey(code);
    }

    protected int GetIntParameterValue(string code)
    {
      this.CacheParameterValuesByCodes();

      if (int.TryParse(this.parameterValuesByCodes[code], out int result))
        return result;

      return 0;
    }

    protected bool GetBoolParameterValue(string code)
    {
      this.CacheParameterValuesByCodes();

      if (bool.TryParse(this.parameterValuesByCodes[code], out bool result))
        return result;

      return false;
    }

    protected string GetStringParameterValue(string code)
    {
      this.CacheParameterValuesByCodes();
      return this.parameterValuesByCodes[code];
    }

    private void CacheParameterValuesByCodes()
    {
      if (this.parameterValuesByCodes == null)
        this.parameterValuesByCodes = ParametersParser.Parse(this.form.Parameters).ToDictionary(a => a.Key, a => a.Value);
    }
  }
}