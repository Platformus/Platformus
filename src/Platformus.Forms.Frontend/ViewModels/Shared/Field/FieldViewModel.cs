// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Forms.Frontend.ViewModels.Shared
{
  public class FieldViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public FieldTypeViewModel FieldType { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsRequired { get; set; }
    public int? MaxLength { get; set; }
    public IEnumerable<FieldOptionViewModel> FieldOptions { get; set; }
  }
}