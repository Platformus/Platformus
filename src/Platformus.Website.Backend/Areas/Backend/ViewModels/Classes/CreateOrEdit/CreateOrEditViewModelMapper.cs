// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Classes
{
  public static class CreateOrEditViewModelMapper
  {
    public static Class Map(Class @class, CreateOrEditViewModel createOrEdit)
    {
      @class.ClassId = createOrEdit.ClassId;
      @class.Code = createOrEdit.Code;
      @class.Name = createOrEdit.Name;
      @class.PluralizedName = createOrEdit.PluralizedName;
      @class.IsAbstract = createOrEdit.IsAbstract;
      return @class;
    }
  }
}