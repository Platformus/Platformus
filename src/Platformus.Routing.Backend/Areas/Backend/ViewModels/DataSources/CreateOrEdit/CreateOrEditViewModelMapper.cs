// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Backend.ViewModels.DataSources
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DataSource Map(CreateOrEditViewModel createOrEdit)
    {
      DataSource dataSource = new DataSource();

      if (createOrEdit.Id != null)
        dataSource = this.RequestHandler.Storage.GetRepository<IDataSourceRepository>().WithKey((int)createOrEdit.Id);

      else dataSource.EndpointId = createOrEdit.EndpointId;

      dataSource.Code = createOrEdit.Code;
      dataSource.CSharpClassName = createOrEdit.CSharpClassName;
      dataSource.Parameters = createOrEdit.Parameters;
      return dataSource;
    }
  }
}