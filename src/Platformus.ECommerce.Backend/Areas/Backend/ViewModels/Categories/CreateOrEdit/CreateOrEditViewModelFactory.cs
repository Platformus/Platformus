// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.ProductProviders;

namespace Platformus.ECommerce.Backend.ViewModels.Categories
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(HttpContext httpContext, Category category)
    {
      if (category == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = httpContext.GetLocalizations(),
          DescriptionLocalizations = httpContext.GetLocalizations(),
          TitleLocalizations = httpContext.GetLocalizations(),
          MetaDescriptionLocalizations = httpContext.GetLocalizations(),
          MetaKeywordsLocalizations = httpContext.GetLocalizations(),
          ProductProviderCSharpClassNameOptions = GetProductProviderCSharpClassNameOptions(),
          ProductProviders = GetProductProviders()
        };

      return new CreateOrEditViewModel()
      {
        Id = category.Id,
        Url = category.Url,
        NameLocalizations = httpContext.GetLocalizations(category.Name),
        DescriptionLocalizations = httpContext.GetLocalizations(category.Description),
        TitleLocalizations = httpContext.GetLocalizations(category.Title),
        MetaDescriptionLocalizations = httpContext.GetLocalizations(category.MetaDescription),
        MetaKeywordsLocalizations = httpContext.GetLocalizations(category.MetaKeywords),
        Position = category.Position,
        ProductProviderCSharpClassName = category.ProductProviderCSharpClassName,
        ProductProviderCSharpClassNameOptions = GetProductProviderCSharpClassNameOptions(),
        ProductProviderParameters = category.ProductProviderParameters,
        ProductProviders = GetProductProviders()
      };
    }

    private static IEnumerable<Option> GetProductProviderCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IProductProvider>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
        t => new Option(t.FullName)
      );
    }

    private static IEnumerable<dynamic> GetProductProviders()
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