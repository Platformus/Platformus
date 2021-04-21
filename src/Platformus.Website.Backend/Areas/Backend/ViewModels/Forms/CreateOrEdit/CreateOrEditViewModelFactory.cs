// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;
using Platformus.Website.FormHandlers;

namespace Platformus.Website.Backend.ViewModels.Forms
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModel Create(HttpContext httpContext, Form form)
    {
      if (form == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = this.GetLocalizations(httpContext),
          SubmitButtonTitleLocalizations = this.GetLocalizations(httpContext),
          FormHandlerCSharpClassNameOptions = this.GetFormHandlerCSharpClassNameOptions(),
          FormHandlers = this.GetFormHandlers()
        };

      return new CreateOrEditViewModel()
      {
        Id = form.Id,
        Code = form.Code,
        NameLocalizations = this.GetLocalizations(httpContext, form.Name),
        SubmitButtonTitleLocalizations = this.GetLocalizations(httpContext, form.SubmitButtonTitle),
        ProduceCompletedForms = form.ProduceCompletedForms,
        FormHandlerCSharpClassName = form.FormHandlerCSharpClassName,
        FormHandlerCSharpClassNameOptions = this.GetFormHandlerCSharpClassNameOptions(),
        FormHandlerParameters = form.FormHandlerParameters,
        FormHandlers = this.GetFormHandlers()
      };
    }

    private IEnumerable<Option> GetFormHandlerCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IFormHandler>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
        t => new Option(t.FullName)
      );
    }

    private IEnumerable<dynamic> GetFormHandlers()
    {
      return ExtensionManager.GetInstances<IFormHandler>().Where(fh => !fh.GetType().GetTypeInfo().IsAbstract).Select(
        fh => new {
          cSharpClassName = fh.GetType().FullName,
          parameterGroups = fh.ParameterGroups.Select(
            fhpg => new
            {
              name = fhpg.Name,
              parameters = fhpg.Parameters.Select(
                fhp => new
                {
                  code = fhp.Code,
                  name = fhp.Name,
                  javaScriptEditorClassName = fhp.JavaScriptEditorClassName,
                  options = fhp.Options == null ? null : fhp.Options.Select(
                    o => new { text = o.Text, value = o.Value }
                  ),
                  defaultValue = fhp.DefaultValue,
                  isRequired = fhp.IsRequired
                }
              )
            }
          ),
          description = fh.Description
        }
      );
    }
  }
}