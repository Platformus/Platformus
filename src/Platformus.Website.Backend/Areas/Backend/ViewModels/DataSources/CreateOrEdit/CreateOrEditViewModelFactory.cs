// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Infrastructure;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;
using Platformus.Website.DataProviders;

namespace Platformus.Website.Backend.ViewModels.DataSources
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModel Create(DataSource dataSource)
    {
      if (dataSource == null)
        return new CreateOrEditViewModel()
        {
          DataProviderCSharpClassNameOptions = this.GetDataProviderCSharpClassNameOptions(),
          DataProviders = this.GetDataProviders()
        };

      return new CreateOrEditViewModel()
      {
        Id = dataSource.Id,
        Code = dataSource.Code,
        DataProviderCSharpClassName = dataSource.DataProviderCSharpClassName,
        DataProviderCSharpClassNameOptions = this.GetDataProviderCSharpClassNameOptions(),
        DataProviderParameters = dataSource.DataProviderParameters,
        DataProviders = this.GetDataProviders()
      };
    }

    private IEnumerable<Option> GetDataProviderCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IDataProvider>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
        t => new Option(t.FullName)
      );
    }

    private IEnumerable<dynamic> GetDataProviders()
    {
      return ExtensionManager.GetInstances<IDataProvider>().Where(dp => !dp.GetType().GetTypeInfo().IsAbstract).Select(
        dp => new {
          cSharpClassName = dp.GetType().FullName,
          parameterGroups = dp.ParameterGroups.Select(
            dppg => new
            {
              name = dppg.Name,
              parameters = dppg.Parameters.Select(
                dpp => new
                {
                  code = dpp.Code,
                  name = dpp.Name,
                  javaScriptEditorClassName = dpp.JavaScriptEditorClassName,
                  options = dpp.Options == null ? null : dpp.Options.Select(
                    o => new { text = o.Text, value = o.Value }
                  ),
                  defaultValue = dpp.DefaultValue,
                  isRequired = dpp.IsRequired
                }
              )
            }
          ),
          description = dp.Description
        }
      );
    }
  }
}