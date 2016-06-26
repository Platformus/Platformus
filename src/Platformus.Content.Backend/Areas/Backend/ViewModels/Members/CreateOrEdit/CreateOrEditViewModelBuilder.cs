// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Members
{
  public class CreateOrEditViewModelBuilder : ViewModelBuilderBase
  {
    public CreateOrEditViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public CreateOrEditViewModel Build(int? id, int? classId)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          TabOptions = this.GetTabOptions((int)classId),
          PropertyDataTypeOptions = this.GetPropertyDataTypeOptions(),
          RelationClassOptions = this.GetRelationClassOptions()
        };

      Member member = this.handler.Storage.GetRepository<IMemberRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = member.Id,
        TabId = member.TabId,
        TabOptions = this.GetTabOptions(member.ClassId),
        Code = member.Code,
        Name = member.Name,
        Position = member.Position,
        PropertyDataTypeId = member.PropertyDataTypeId,
        PropertyDataTypeOptions = this.GetPropertyDataTypeOptions(),
        IsPropertyLocalizable = member.IsPropertyLocalizable == true,
        IsPropertyVisibleInList = member.IsPropertyVisibleInList == true,
        RelationClassId = member.RelationClassId,
        RelationClassOptions = this.GetRelationClassOptions(),
        IsRelationSingleParent = member.IsRelationSingleParent == true
      };
    }

    private IEnumerable<Option> GetTabOptions(int classId)
    {
      List<Option> options = new List<Option>();

      options.Add(new Option("Tab not specified", string.Empty));
      options.AddRange(
        this.handler.Storage.GetRepository<ITabRepository>().FilteredByClassId(classId).Select(
          t => new Option(t.Name, t.Id.ToString())
        )
      );

      return options;
    }

    private IEnumerable<Option> GetPropertyDataTypeOptions()
    {
      List<Option> options = new List<Option>();

      options.Add(new Option("Property data type not specified", string.Empty));
      options.AddRange(
        this.handler.Storage.GetRepository<IDataTypeRepository>().All().Select(
          dt => new Option(dt.Name, dt.Id.ToString())
        )
      );

      return options;
    }

    private IEnumerable<Option> GetRelationClassOptions()
    {
      List<Option> options = new List<Option>();

      options.Add(new Option("Relation class not specified", string.Empty));
      options.AddRange(
        this.handler.Storage.GetRepository<IClassRepository>().All().Select(
          c => new Option(c.Name, c.Id.ToString())
        )
      );

      return options;
    }
  }
}