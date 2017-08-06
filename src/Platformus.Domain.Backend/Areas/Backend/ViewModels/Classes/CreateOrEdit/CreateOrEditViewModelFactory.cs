// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Primitives;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Classes
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          ClassOptions = this.GetClassOptions(),
        };

      Class @class = this.RequestHandler.Storage.GetRepository<IClassRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = @class.Id,
        ClassId = @class.ClassId,
        ClassOptions = this.GetClassOptions(),
        Code = @class.Code,
        Name = @class.Name,
        PluralizedName = @class.PluralizedName,
        IsAbstract = @class.IsAbstract
      };
    }

    private IEnumerable<Option> GetClassOptions()
    {
      List<Option> options = new List<Option>();

      options.Add(new Option("Parent class not specified", string.Empty));
      options.AddRange(
        this.RequestHandler.Storage.GetRepository<IClassRepository>().Abstract().Select(
          c => new Option(c.Name, c.Id.ToString())
        )
      );

      return options;
    }
  }
}