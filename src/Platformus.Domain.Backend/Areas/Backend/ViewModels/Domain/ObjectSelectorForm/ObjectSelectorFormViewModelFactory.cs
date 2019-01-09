// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Domain
{
  public class ObjectSelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public ObjectSelectorFormViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ObjectSelectorFormViewModel Create(int classId, string objectIds)
    {
      return new ObjectSelectorFormViewModel()
      {
        Class = new ClassViewModelFactory(this.RequestHandler).Create(
          this.RequestHandler.Storage.GetRepository<IClassRepository>().WithKey(classId)
        ),
        GridColumns = this.GetGridColumns(classId),
        Objects = this.RequestHandler.Storage.GetRepository<IObjectRepository>().FilteredByClassId(classId).ToList().Select(
          o => new ObjectViewModelFactory(this.RequestHandler).Create(o.Id)
        ),
        ObjectIds = string.IsNullOrEmpty(objectIds) ? new int[] { } : objectIds.Split(',').Select(objectId => int.Parse(objectId))
      };
    }

    private IEnumerable<GridColumnViewModel> GetGridColumns(int classId)
    {
      return this.RequestHandler.Storage.GetRepository<IMemberRepository>().FilteredByClassIdPropertyVisibleInList((int)classId).ToList().Select(m => new GridColumnViewModelFactory(this.RequestHandler).Create(m.Name));
    }
  }
}