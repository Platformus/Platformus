// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Objects
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public Object Map(ObjectFilter filter, Object @object, CreateOrEditViewModel createOrEdit)
    {
      if (@object.Id == 0)
        @object.ClassId = (int)filter.Class.Id;

      return @object;
    }
  }
}