// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.DataSources;

public static class CreateOrEditViewModelMapper
{
  public static DataSource Map(DataSourceFilter filter, DataSource dataSource, CreateOrEditViewModel createOrEdit)
  {
    if (dataSource.Id == 0)
      dataSource.EndpointId = (int)filter.Endpoint.Id;

    dataSource.Code = createOrEdit.Code;
    dataSource.DataProviderCSharpClassName = createOrEdit.DataProviderCSharpClassName;
    dataSource.DataProviderParameters = createOrEdit.DataProviderParameters;
    return dataSource;
  }
}