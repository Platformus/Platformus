// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;
using Platformus.Barebone;
using Platformus.Barebone.Backend;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Domain.DataSources;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataSources
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
          CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
          DataSources = this.GetDataSources()
        };

      DataSource dataSource = this.RequestHandler.Storage.GetRepository<IDataSourceRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = dataSource.Id,
        Code = dataSource.Code,
        CSharpClassName = dataSource.CSharpClassName,
        CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
        Parameters = dataSource.Parameters,
        DataSources = this.GetDataSources()
      };
    }

    private IEnumerable<Option> GetCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IDataSource>().Where(t => t != typeof(DataSourceBase)).Select(
        t => new Option(t.FullName)
      );
    }

    private IEnumerable<dynamic> GetDataSources()
    {
      return ExtensionManager.GetInstances<IDataSource>().Where(ds => ds.GetType() != typeof(DataSourceBase)).Select(
        ds => new {
          cSharpClassName = ds.GetType().FullName,
          dataSourceParameters = ds.DataSourceParameters.Select(
            dsp => new {
              code = dsp.Code,
              name = dsp.Name,
              javaScriptEditorClassName = dsp.JavaScriptEditorClassName
            }
          )
        }
      );
    }
  }
}