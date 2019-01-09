// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Infrastructure;
using Platformus.Barebone;
using Platformus.Barebone.Primitives;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.ProductProviders;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Catalogs
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
          Url = "/",
          NameLocalizations = this.GetLocalizations(),
          CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
          ProductProviders = this.GetProductProviders()
        };

      Catalog catalog = this.RequestHandler.Storage.GetRepository<ICatalogRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = catalog.Id,
        Url = catalog.Url,
        NameLocalizations = this.GetLocalizations(catalog.NameId),
        CSharpClassName = catalog.CSharpClassName,
        CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
        Parameters = catalog.Parameters,
        ProductProviders = this.GetProductProviders(),
        Position =  catalog.Position
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