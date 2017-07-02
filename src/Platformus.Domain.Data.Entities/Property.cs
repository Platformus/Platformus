// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Domain.Data.Entities
{
  public class Property : IEntity
  {
    public int Id { get; set; }
    public int ObjectId { get; set; }
    public int MemberId { get; set; }
    public int? IntegerValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public int? StringValueId { get; set; }
    public DateTime? DateTimeValue { get; set; }

    public virtual Object Object { get; set; }
    public virtual Member Member { get; set; }
    public virtual Dictionary StringValue { get; set; }
  }
}