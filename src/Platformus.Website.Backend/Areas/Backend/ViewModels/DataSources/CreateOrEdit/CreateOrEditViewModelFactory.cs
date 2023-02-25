// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Infrastructure;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;
using Platformus.Website.DataProviders;

namespace Platformus.Website.Backend.ViewModels.DataSources;

public static class CreateOrEditViewModelFactory
{
  public static CreateOrEditViewModel Create(DataSource dataSource)
  {
    if (dataSource == null)
      return new CreateOrEditViewModel()
      {
        DataProviderCSharpClassNameOptions = GetDataProviderCSharpClassNameOptions()
      };

    return new CreateOrEditViewModel()
    {
      Id = dataSource.Id,
      Code = dataSource.Code,
      DataProviderCSharpClassName = dataSource.DataProviderCSharpClassName,
      DataProviderCSharpClassNameOptions = GetDataProviderCSharpClassNameOptions(),
      DataProviderParameters = dataSource.DataProviderParameters
    };
  }

  private static IEnumerable<Option> GetDataProviderCSharpClassNameOptions()
  {
    return ExtensionManager.GetImplementations<IDataProvider>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
      t => new Option(t.FullName)
    ).ToList();
  }
}