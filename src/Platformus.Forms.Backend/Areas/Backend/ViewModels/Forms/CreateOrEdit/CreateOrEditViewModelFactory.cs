// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Infrastructure;
using Platformus.Barebone;
using Platformus.Barebone.Primitives;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Forms.FormHandlers;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Forms
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = this.GetLocalizations(),
          SubmitButtonTitleLocalizations = this.GetLocalizations(),
          CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
          FormHandlers = this.GetFormHandlers()
        };

      Form form = this.RequestHandler.Storage.GetRepository<IFormRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = form.Id,
        Code = form.Code,
        NameLocalizations = this.GetLocalizations(form.NameId),
        SubmitButtonTitleLocalizations = this.GetLocalizations(form.SubmitButtonTitleId),
        ProduceCompletedForms = form.ProduceCompletedForms,
        CSharpClassName = form.CSharpClassName,
        CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
        Parameters = form.Parameters,
        FormHandlers = this.GetFormHandlers()
      };
    }

    private IEnumerable<Option> GetCSharpClassNameOptions()
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