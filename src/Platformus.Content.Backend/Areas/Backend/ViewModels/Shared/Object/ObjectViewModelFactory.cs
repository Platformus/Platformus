// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Shared
{
  public class ObjectViewModelFactory : ViewModelFactoryBase
  {
    public ObjectViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public ObjectViewModel Create(Object @object)
    {
      List<Class> relatedClasses = new List<Class>();

      foreach (Member member in this.handler.Storage.GetRepository<IMemberRepository>().FilteredByRelationClassIdRelationSingleParent(@object.ClassId))
      {
        Class @class = this.handler.Storage.GetRepository<IClassRepository>().WithKey((int)member.ClassId);

        relatedClasses.Add(@class);
      }

      return new ObjectViewModel()
      {
        Id = @object.Id,
        Url = @object.Url,
        Class = new ClassViewModelFactory(this.handler).Create(
          this.handler.Storage.GetRepository<IClassRepository>().WithKey(@object.ClassId)
        ),
        Properties = new ObjectManager(this.handler).GetDisplayProperties(@object),
        RelatedClasses = relatedClasses.Select(
          c => new ClassViewModelFactory(this.handler).Create(c)
        )
      };
    }
  }
}