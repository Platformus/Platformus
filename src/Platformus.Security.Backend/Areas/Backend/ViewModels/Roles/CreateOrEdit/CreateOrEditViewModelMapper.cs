// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Roles
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Role Map(CreateOrEditViewModel createOrEdit)
    {
      Role role = new Role();

      if (createOrEdit.Id != null)
        role = this.RequestHandler.Storage.GetRepository<IRoleRepository>().WithKey((int)createOrEdit.Id);

      role.Code = createOrEdit.Code;
      role.Name = createOrEdit.Name;
      role.Position = createOrEdit.Position;
      return role;
    }
  }
}