// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Objects;

public static class CreateOrEditViewModelMapper
{
  public static Object Map(ObjectFilter filter, Object @object, CreateOrEditViewModel createOrEdit)
  {
    if (@object.Id == 0)
      @object.ClassId = (int)filter.Class.Id;

    return @object;
  }
}