// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Objects
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(int? classId, string orderBy, string direction, int skip, int take)
    {
      IClassRepository classRepository = this.RequestHandler.Storage.GetRepository<IClassRepository>();
      IObjectRepository objectRepository = this.RequestHandler.Storage.GetRepository<IObjectRepository>();
      
      return new IndexViewModel()
      {
        Class = classId == null ? null : new ClassViewModelFactory(this.RequestHandler).Create(
          classRepository.WithKey((int)classId)
        ),
        ClassesByAbstractClasses = this.GetClassesByAbstractClasses(),
        Grid = classId == null ? null : new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, objectRepository.CountByClassId((int)classId),
          this.GetGridColumns((int)classId),
          objectRepository.FilteredByClassIdRange((int)classId, orderBy, direction, skip, take).Select(o => new ObjectViewModelFactory(this.RequestHandler).Create(o)),
          "_Object"
        )
      };
    }

    private IDictionary<ClassViewModel, IEnumerable<ClassViewModel>> GetClassesByAbstractClasses()
    {
      Dictionary<ClassViewModel, IEnumerable<ClassViewModel>> classesByAbstractClasses = new Dictionary<ClassViewModel, IEnumerable<ClassViewModel>>();

      foreach (Class abstractClass in this.RequestHandler.Storage.GetRepository<IClassRepository>().Abstract())
        classesByAbstractClasses.Add(
          new ClassViewModelFactory(this.RequestHandler).Create(abstractClass),
          this.RequestHandler.Storage.GetRepository<IClassRepository>().FilteredByClassId(abstractClass.Id).Select(
            c => new ClassViewModelFactory(this.RequestHandler).Create(c)
          )
        );

      classesByAbstractClasses.Add(
        new ClassViewModel() { PluralizedName = "Others" },
        this.RequestHandler.Storage.GetRepository<IClassRepository>().FilteredByClassId(null).Select(
          c => new ClassViewModelFactory(this.RequestHandler).Create(c)
        )
      );

      return classesByAbstractClasses;
    }

    private IEnumerable<GridColumnViewModel> GetGridColumns(int classId)
    {
      List<GridColumnViewModel> gridColumns = new List<GridColumnViewModel>();
      IMemberRepository memberRepository = this.RequestHandler.Storage.GetRepository<IMemberRepository>();

      gridColumns.AddRange(
        memberRepository.FilteredByClassIdInlcudingParentPropertyVisibleInList(classId).Select(m => new GridColumnViewModelFactory(this.RequestHandler).Create(m.Name))
      );

      foreach (Member member in memberRepository.FilteredByRelationClassIdRelationSingleParent(classId))
      {
        Class @class = this.RequestHandler.Storage.GetRepository<IClassRepository>().WithKey(member.ClassId);

        gridColumns.Add(new GridColumnViewModelFactory(this.RequestHandler).Create(@class.PluralizedName));
      }

      gridColumns.Add(new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty());
      return gridColumns;
    }
  }
}