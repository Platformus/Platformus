// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend;
using Platformus.Barebone.Primitives;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Members
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id, int? classId)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          TabOptions = this.GetTabOptions((int)classId),
          PropertyDataTypeOptions = this.GetPropertyDataTypeOptions(),
          RelationClassOptions = this.GetRelationClassOptions(),
          DataTypes = this.GetDataTypes()
        };

      Member member = this.RequestHandler.Storage.GetRepository<IMemberRepository>().WithKey((int)id);

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
        IsRelationSingleParent = member.IsRelationSingleParent == true,
        MinRelatedObjectsNumber = member.MinRelatedObjectsNumber,
        MaxRelatedObjectsNumber = member.MaxRelatedObjectsNumber,
        DataTypes = this.GetDataTypes(member.Id)
      };
    }

    private IEnumerable<Option> GetTabOptions(int classId)
    {
      List<Option> options = new List<Option>();

      options.Add(new Option("Tab not specified", string.Empty));
      options.AddRange(
        this.RequestHandler.Storage.GetRepository<ITabRepository>().FilteredByClassId(classId).ToList().Select(
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
        this.RequestHandler.Storage.GetRepository<IDataTypeRepository>().All().ToList().Select(
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
        this.RequestHandler.Storage.GetRepository<IClassRepository>().All().ToList().Select(
          c => new Option(c.Name, c.Id.ToString())
        )
      );

      return options;
    }

    private IEnumerable<dynamic> GetDataTypes(int? memberId = null)
    {
      return this.RequestHandler.Storage.GetRepository<IDataTypeRepository>().All().ToList().Select(
        dt => new
        {
          id = dt.Id,
          storageDataType = dt.StorageDataType,
          dataTypeParameters = this.RequestHandler.Storage.GetRepository<IDataTypeParameterRepository>().FilteredByDataTypeId(dt.Id).ToList().Select(
            dtp => new
            {
              id = dtp.Id,
              javaScriptEditorClassName = dtp.JavaScriptEditorClassName,
              code = dtp.Code,
              name = dtp.Name,
              value = memberId == null ? null : this.RequestHandler.Storage.GetRepository<IDataTypeParameterValueRepository>().WithDataTypeParameterIdAndMemberId(dtp.Id, (int)memberId)?.Value
            }
          )
        }
      );
    }
  }
}