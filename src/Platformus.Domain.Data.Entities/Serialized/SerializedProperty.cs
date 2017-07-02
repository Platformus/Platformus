// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Platformus.Domain.Data.Entities
{
  public class SerializedProperty
  {
    public SerializedMember Member { get; set; }
    public int? IntegerValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public string StringValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
  }
}