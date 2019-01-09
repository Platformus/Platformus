// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platformus.Barebone;
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
        Parameters = this.GetParameters(member.Id),
        RelationClassId = member.RelationClassId,
        RelationClassOptions = this.GetRelationClassOptions(),
        IsRelationSingleParent = member.IsRelationSingleParent == true,
        MinRelatedObjectsNumber = member.MinRelatedObjectsNumber,
        MaxRelatedObjectsNumber = member.MaxRelatedObjectsNumber,
        DataTypes = this.GetDataTypes()
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

    private string GetParameters(int? memberId)
    {
      if (memberId == null)
        return null;

      StringBuilder sb = new StringBuilder();

      foreach (DataType dataType in this.RequestHandler.Storage.GetRepository<IDataTypeRepository>().All().ToList())
      {
        foreach (DataTypeParameter dataTypeParameter in this.RequestHandler.Storage.GetRepository<IDataTypeParameterRepository>().FilteredByDataTypeId(dataType.Id).ToList())
        {
          DataTypeParameterValue dataTypeParameterValue = this.RequestHandler.Storage.GetRepository<IDataTypeParameterValueRepository>().WithDataTypeParameterIdAndMemberId(dataTypeParameter.Id, (int)memberId);

          if (dataTypeParameterValue != null)
          {
            if (sb.Length != 0)
              sb.Append(';');

            sb.Append($"{dataTypeParameter.Code}={dataTypeParameterValue.Value}");
          }
        }
      }
      

      return sb.ToString();
    }

    private IEnumerable<dynamic> GetDataTypes()
    {
      return this.RequestHandler.Storage.GetRepository<IDataTypeRepository>().All().ToList().Select(
        dt => new
        {
          id = dt.Id,
          parameterGroups = new dynamic[] {
            new {
              parameters = this.RequestHandler.Storage.GetRepository<IDataTypeParameterRepository>().FilteredByDataTypeId(dt.Id).ToList().Select(
                dtp => new
                {
                  code = dtp.Code,
                  name = dtp.Name,
                  javaScriptEditorClassName = dtp.JavaScriptEditorClassName
                }
              )
            }
          }
        }
      );
    }
  }
}