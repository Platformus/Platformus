// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Content.Backend.ViewModels.Shared;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Objects
{
  public class IndexViewModelBuilder : ViewModelBuilderBase
  {
    public IndexViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Build(int? classId, string orderBy, string direction, int skip, int take)
    {
      IClassRepository classRepository = this.handler.Storage.GetRepository<IClassRepository>();
      IObjectRepository objectRepository = this.handler.Storage.GetRepository<IObjectRepository>();
      
      return new IndexViewModel()
      {
        Class = classId == null ? null : new ClassViewModelBuilder(this.handler).Build(
          classRepository.WithKey((int)classId)
        ),
        StandaloneClasses = classRepository.Standalone().Select(
          c => new ClassViewModelBuilder(this.handler).Build(c)
        ),
        EmbeddedClasses = classRepository.Embedded().Select(
          c => new ClassViewModelBuilder(this.handler).Build(c)
        ),
        Grid = classId == null ? null : new GridViewModelBuilder(this.handler).Build(
          orderBy, direction, skip, take, objectRepository.CountByClassId((int)classId),
          this.GetGridColumns((int)classId),
          objectRepository.FilteredByClassIdRange((int)classId, orderBy, direction, skip, take).Select(o => new ObjectViewModelBuilder(this.handler).Build(o)),
          "_Object"
        )
      };
    }

    private IEnumerable<GridColumnViewModel> GetGridColumns(int classId)
    {
      List<GridColumnViewModel> gridColumns = new List<GridColumnViewModel>();
      IMemberRepository memberRepository = this.handler.Storage.GetRepository<IMemberRepository>();

      if (this.IsClassStandalone(classId))
        gridColumns.Add(new GridColumnViewModelBuilder(this.handler).Build("URL", "Url"));

      gridColumns.AddRange(
        memberRepository.FilteredByClassIdInlcudingParentPropertyVisibleInList(classId).Select(m => new GridColumnViewModelBuilder(this.handler).Build(m.Name))
      );

      foreach (Member member in memberRepository.FilteredByRelationClassIdRelationSingleParent(classId))
      {
        Class @class = this.handler.Storage.GetRepository<IClassRepository>().WithKey(member.ClassId);

        gridColumns.Add(new GridColumnViewModelBuilder(this.handler).Build(@class.PluralizedName));
      }

      gridColumns.Add(new GridColumnViewModelBuilder(this.handler).BuildEmpty());
      return gridColumns;
    }

    private bool IsClassStandalone(int classId)
    {
      return this.handler.Storage.GetRepository<IClassRepository>().WithKey(classId).IsStandalone == true;
    }
  }
}