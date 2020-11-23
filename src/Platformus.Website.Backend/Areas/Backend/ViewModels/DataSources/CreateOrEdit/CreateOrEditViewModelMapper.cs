// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.DataSources
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public DataSource Map(DataSourceFilter filter, DataSource dataSource, CreateOrEditViewModel createOrEdit)
    {
      if (dataSource.Id == 0)
        dataSource.EndpointId = (int)filter.Endpoint.Id;

      dataSource.Code = createOrEdit.Code;
      dataSource.CSharpClassName = createOrEdit.CSharpClassName;
      dataSource.Parameters = createOrEdit.Parameters;
      return dataSource;
    }
  }
}