// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone;

namespace Platformus.Domain.DataSources
{
  public interface ISingleObjectDataSource : IDataSource
  {
    dynamic GetSerializedObject(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args);
  }
}