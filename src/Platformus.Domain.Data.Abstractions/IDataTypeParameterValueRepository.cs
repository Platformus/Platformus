// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.Abstractions
{
  public interface IDataTypeParameterValueRepository : IRepository
  {
    DataTypeParameterValue WithDataTypeParameterIdAndMemberId(int dataTypeParameterId, int memberId);
    void Create(DataTypeParameterValue dataTypeParameterValue);
    void Edit(DataTypeParameterValue dataTypeParameterValue);
  }
}