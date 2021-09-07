// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;
using Platformus.Website.FormHandlers;

namespace Platformus.Website.Backend.ViewModels.Forms
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(HttpContext httpContext, Form form)
    {
      if (form == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = httpContext.GetLocalizations(),
          SubmitButtonTitleLocalizations = httpContext.GetLocalizations(),
          FormHandlerCSharpClassNameOptions = GetFormHandlerCSharpClassNameOptions(),
          FormHandlers = GetFormHandlers()
        };

      return new CreateOrEditViewModel()
      {
        Id = form.Id,
        Code = form.Code,
        NameLocalizations = httpContext.GetLocalizations(form.Name),
        SubmitButtonTitleLocalizations = httpContext.GetLocalizations(form.SubmitButtonTitle),
        ProduceCompletedForms = form.ProduceCompletedForms,
        FormHandlerCSharpClassName = form.FormHandlerCSharpClassName,
        FormHandlerCSharpClassNameOptions = GetFormHandlerCSharpClassNameOptions(),
        FormHandlerParameters = form.FormHandlerParameters,
        FormHandlers = GetFormHandlers()
      };
    }

    private static IEnumerable<Option> GetFormHandlerCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IFormHandler>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
        t => new Option(t.FullName)
      );
    }

    private static IEnumerable<dynamic> GetFormHandlers()
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