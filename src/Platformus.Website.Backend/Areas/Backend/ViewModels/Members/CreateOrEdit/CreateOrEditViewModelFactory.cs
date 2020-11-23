// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Members
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, MemberFilter filter, Member member)
    {
      if (member == null)
        return new CreateOrEditViewModel()
        {
          TabOptions = await this.GetTabOptionsAsync(httpContext, (int)filter.Class.Id),
          PropertyDataTypeOptions = await this.GetPropertyDataTypeOptionsAsync(httpContext),
          RelationClassOptions = await this.GetRelationClassOptionsAsync(httpContext),
          DataTypes = await this.GetDataTypesAsync(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = member.Id,
        TabId = member.TabId,
        TabOptions = await this.GetTabOptionsAsync(httpContext, member.ClassId),
        Code = member.Code,
        Name = member.Name,
        Position = member.Position,
        PropertyDataTypeId = member.PropertyDataTypeId,
        PropertyDataTypeOptions = await this.GetPropertyDataTypeOptionsAsync(httpContext),
        IsPropertyLocalizable = member.IsPropertyLocalizable == true,
        IsPropertyVisibleInList = member.IsPropertyVisibleInList == true,
        Parameters = await this.GetParametersAsync(httpContext, member.Id),
        RelationClassId = member.RelationClassId,
        RelationClassOptions = await this.GetRelationClassOptionsAsync(httpContext),
        IsRelationSingleParent = member.IsRelationSingleParent == true,
        MinRelatedObjectsNumber = member.MinRelatedObjectsNumber,
        MaxRelatedObjectsNumber = member.MaxRelatedObjectsNumber,
        DataTypes = await this.GetDataTypesAsync(httpContext)
      };
    }

    private async Task<IEnumerable<Option>> GetTabOptionsAsync(HttpContext httpContext, int classId)
    {
      List<Option> options = new List<Option>();

      options.Add(new Option("Tab not specified", string.Empty));
      options.AddRange(
        (await httpContext.GetStorage().GetRepository<int, Tab, TabFilter>().GetAllAsync(new TabFilter() { Class = new ClassFilter() { Id = classId } })).Select(
          t => new Option(t.Name, t.Id.ToString())
        )
      );

      return options;
    }

    private async Task<IEnumerable<Option>> GetPropertyDataTypeOptionsAsync(HttpContext httpContext)
    {
      List<Option> options = new List<Option>();

      options.Add(new Option("Property data type not specified", string.Empty));
      options.AddRange(
        (await httpContext.GetStorage().GetRepository<int, DataType, DataTypeFilter>().GetAllAsync()).Select(
          dt => new Option(dt.Name, dt.Id.ToString())
        )
      );

      return options;
    }

    private async Task<IEnumerable<Option>> GetRelationClassOptionsAsync(HttpContext httpContext)
    {
      List<Option> options = new List<Option>();

      options.Add(new Option("Relation class not specified", string.Empty));
      options.AddRange(
        (await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetAllAsync()).Select(
          c => new Option(c.Name, c.Id.ToString())
        )
      );

      return options;
    }

    private async Task<string> GetParametersAsync(HttpContext httpContext, int? memberId)
    {
      if (memberId == null)
        return null;

      StringBuilder sb = new StringBuilder();

      IEnumerable<DataType> dataTypes = await httpContext.GetStorage().GetRepository<int, DataType, DataTypeFilter>().GetAllAsync(
        inclusions: new Inclusion<DataType>("DataTypeParameters.DataTypeParameterValues")
      );

      foreach (DataType dataType in dataTypes)
      {
        foreach (DataTypeParameter dataTypeParameter in dataType.DataTypeParameters)
        {
          DataTypeParameterValue dataTypeParameterValue = dataTypeParameter.DataTypeParameterValues.FirstOrDefault(dtpv => dtpv.MemberId == memberId);

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

    private async Task<IEnumerable<dynamic>> GetDataTypesAsync(HttpContext httpContext)
    {
      IEnumerable<DataType> dataTypes = await httpContext.GetStorage().GetRepository<int, DataType, DataTypeFilter>().GetAllAsync(
        inclusions: new Inclusion<DataType>(dt => dt.DataTypeParameters)
      );

      return dataTypes.Select(
        dt => new
        {
          id = dt.Id,
          storageDataType = dt.StorageDataType,
          parameterGroups = new dynamic[] {
            new {
              parameters = dt.DataTypeParameters.Select(
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