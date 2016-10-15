// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Content.Backend.ViewModels.Shared;
using Platformus.Content.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Content
{
  public class ObjectSelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public ObjectSelectorFormViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public ObjectSelectorFormViewModel Create(int classId, string objectIds)
    {
      return new ObjectSelectorFormViewModel()
      {
        Class = new ClassViewModelFactory(this.handler).Create(
          this.handler.Storage.GetRepository<IClassRepository>().WithKey(classId)
        ),
        GridColumns = this.GetGridColumns(classId),
        Objects = this.handler.Storage.GetRepository<IObjectRepository>().FilteredByClassId(classId).Select(
          o => new ObjectViewModelFactory(this.handler).Create(o)
        ),
        ObjectIds = string.IsNullOrEmpty(objectIds) ? new int[] { } : objectIds.Split(',').Select(objectId => int.Parse(objectId))
      };
    }

    private IEnumerable<GridColumnViewModel> GetGridColumns(int classId)
    {
      return this.handler.Storage.GetRepository<IMemberRepository>().FilteredByClassIdPropertyVisibleInList((int)classId).Select(m => new GridColumnViewModelFactory(this.handler).Create(m.Name));
    }
  }
}