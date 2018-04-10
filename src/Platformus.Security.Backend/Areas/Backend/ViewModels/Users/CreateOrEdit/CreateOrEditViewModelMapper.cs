// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Users
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public User Map(CreateOrEditViewModel createOrEdit)
    {
      User user = new User();

      if (createOrEdit.Id != null)
        user = this.RequestHandler.Storage.GetRepository<IUserRepository>().WithKey((int)createOrEdit.Id);

      else user.Created = DateTime.Now;

      user.Name = createOrEdit.Name;
      return user;
    }
  }
}