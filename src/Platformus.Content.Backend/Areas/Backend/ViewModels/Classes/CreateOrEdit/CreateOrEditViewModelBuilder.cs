// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Classes
{
  public class CreateOrEditViewModelBuilder : ViewModelBuilderBase
  {
    public CreateOrEditViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public CreateOrEditViewModel Build(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          ClassOptions = this.GetClassOptions(),
        };

      Class @class = this.handler.Storage.GetRepository<IClassRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = @class.Id,
        ClassId = @class.ClassId,
        ClassOptions = this.GetClassOptions(),
        Name = @class.Name,
        PluralizedName = @class.PluralizedName,
        IsAbstract = @class.IsAbstract == true,
        IsStandalone = @class.IsStandalone == true,
        DefaultViewName = @class.DefaultViewName
      };
    }

    private IEnumerable<Option> GetClassOptions()
    {
      List<Option> options = new List<Option>();

      options.Add(new Option("Parent class not specified", string.Empty));
      options.AddRange(
        this.handler.Storage.GetRepository<IClassRepository>().Abstract().Select(
          c => new Option(c.Name, c.Id.ToString())
        )
      );

      return options;
    }
  }
}