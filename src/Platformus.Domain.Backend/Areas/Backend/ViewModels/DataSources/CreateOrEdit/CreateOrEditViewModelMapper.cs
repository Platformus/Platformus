// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataSources
{
  public class CreateOrEditViewModelMapper : ViewModelFactoryBase
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

      else dataSource.MicrocontrollerId = createOrEdit.MicrocontrollerId;

      dataSource.Code = createOrEdit.Code;
      dataSource.CSharpClassName = createOrEdit.CSharpClassName;
      dataSource.Parameters = createOrEdit.Parameters;
      return dataSource;
    }
  }
}