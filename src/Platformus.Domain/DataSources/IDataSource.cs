// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.DataSources
{
  public interface IDataSource
  {
    IEnumerable<DataSourceParameter> DataSourceParameters { get; }
    IEnumerable<SerializedObject> GetSerializedObjects(IRequestHandler requestHandler, SerializedObject serializedPage, params KeyValuePair<string, string>[] args);
    IEnumerable<Object> GetObjects(IRequestHandler requestHandler, Object page, params KeyValuePair<string, string>[] args);
  }
}