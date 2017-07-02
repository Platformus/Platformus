// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
  public class DataTypeParameterValue : IEntity
  {
    public int Id { get; set; }
    public int DataTypeParameterId { get; set; }
    public int MemberId { get; set; }
    public string Value { get; set; }

    public virtual DataTypeParameter DataTypeParameter { get; set; }
    public virtual Member Member { get; set; }
  }
}
