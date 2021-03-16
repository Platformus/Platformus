// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.ProductProviders;

namespace Platformus.ECommerce.Backend.ViewModels.Categories
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModel Create(HttpContext httpContext, Category category)
    {
      if (category == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = this.GetLocalizations(httpContext),
          CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
          ProductProviders = this.GetProductProviders()
        };

      return new CreateOrEditViewModel()
      {
        Id = category.Id,
        Url = category.Url,
        NameLocalizations = this.GetLocalizations(httpContext, category.Name),
        CSharpClassName = category.CSharpClassName,
        CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
        Parameters = category.Parameters,
        ProductProviders = this.GetProductProviders(),
        Position =  category.Position
      };
    }

    private IEnumerable<Option> GetCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IProductProvider>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
        t => new Option(t.FullName)
      );
    }

    private IEnumerable<dynamic> GetProductProviders()
    {
      return ExtensionManager.GetInstances<IProductProvider>().Where(pp => !pp.GetType().GetTypeInfo().IsAbstract).Select(
        pp => new {
          cSharpClassName = pp.GetType().FullName,
          parameterGroups = pp.ParameterGroups.Select(
            pppg => new
            {
              name = pppg.Name,
              parameters = pppg.Parameters.Select(
                ppp => new
                {
                  code = ppp.Code,
                  name = ppp.Name,
                  javaScriptEditorClassName = ppp.JavaScriptEditorClassName,
                  options = ppp.Options == null ? null : ppp.Options.Select(
                    o => new { text = o.Text, value = o.Value }
                  ),
                  defaultValue = ppp.DefaultValue,
                  isRequired = ppp.IsRequired
                }
              )
            }
          ),
          description = pp.Description
        }
      );
    }
  }
}