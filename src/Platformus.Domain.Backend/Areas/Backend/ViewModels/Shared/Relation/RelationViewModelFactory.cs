// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class RelationViewModelFactory : ViewModelFactoryBase
  {
    public RelationViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public RelationViewModel Create(Relation relation)
    {
      return new RelationViewModel()
      {
        Id = relation.Id,
        PrimaryId = relation.PrimaryId
      };
    }
  }
}