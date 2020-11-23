// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Tabs
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public Tab Map(TabFilter filter, Tab tab, CreateOrEditViewModel createOrEdit)
    {
      if (tab.Id == 0)
        tab.ClassId = (int)filter.Class.Id;

      tab.Name = createOrEdit.Name;
      tab.Position = createOrEdit.Position;
      return tab;
    }
  }
}