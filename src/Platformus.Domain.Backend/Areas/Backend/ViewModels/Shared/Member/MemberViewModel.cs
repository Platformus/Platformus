// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class MemberViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }
    public DataTypeViewModel PropertyDataType { get; set; }
    public ClassViewModel RelationClass { get; set; }
  }
}