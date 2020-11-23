﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Infrastructure;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;
using Platformus.Website.DataSources;

namespace Platformus.Website.Backend.ViewModels.DataSources
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModel Create(DataSource dataSource)
    {
      if (dataSource == null)
        return new CreateOrEditViewModel()
        {
          CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
          DataSources = this.GetDataSources()
        };

      return new CreateOrEditViewModel()
      {
        Id = dataSource.Id,
        Code = dataSource.Code,
        CSharpClassName = dataSource.CSharpClassName,
        CSharpClassNameOptions = this.GetCSharpClassNameOptions(),
        Parameters = dataSource.Parameters,
        DataSources = this.GetDataSources()
      };
    }

    private IEnumerable<Option> GetCSharpClassNameOptions()
    {
      return ExtensionManager.GetImplementations<IDataSource>().Where(t => !t.GetTypeInfo().IsAbstract).Select(
        t => new Option(t.FullName)
      );
    }

    private IEnumerable<dynamic> GetDataSources()
    {
      return ExtensionManager.GetInstances<IDataSource>().Where(ds => !ds.GetType().GetTypeInfo().IsAbstract).Select(
        ds => new {
          cSharpClassName = ds.GetType().FullName,
          parameterGroups = ds.ParameterGroups.Select(
            dspg => new
            {
              name = dspg.Name,
              parameters = dspg.Parameters.Select(
                dsp => new
                {
                  code = dsp.Code,
                  name = dsp.Name,
                  javaScriptEditorClassName = dsp.JavaScriptEditorClassName,
                  options = dsp.Options == null ? null : dsp.Options.Select(
                    o => new { text = o.Text, value = o.Value }
                  ),
                  defaultValue = dsp.DefaultValue,
                  isRequired = dsp.IsRequired
                }
              )
            }
          ),
          description = ds.Description
        }
      );
    }
  }
}