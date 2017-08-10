// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class ObjectViewModelFactory : ViewModelFactoryBase
  {
    public ObjectViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ObjectViewModel Create(int id)
    {
      Object @object = this.RequestHandler.Storage.GetRepository<IObjectRepository>().WithKey(id);
      List<Class> relatedClasses = new List<Class>();

      foreach (Member member in this.RequestHandler.Storage.GetRepository<IMemberRepository>().FilteredByClassIdRelationSingleParent(@object.ClassId).ToList())
      {
        Class @class = this.RequestHandler.Storage.GetRepository<IClassRepository>().WithKey((int)member.RelationClassId);

        relatedClasses.Add(@class);
      }

      return new ObjectViewModel()
      {
        Id = @object.Id,
        Properties = new ObjectManager(this.RequestHandler).GetDisplayProperties(@object),
        RelatedClasses = relatedClasses.Select(
          c => new ClassViewModelFactory(this.RequestHandler).Create(c)
        )
      };
    }
  }
}