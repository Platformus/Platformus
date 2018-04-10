// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Permissions
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Permission Map(CreateOrEditViewModel createOrEdit)
    {
      Permission permission = new Permission();

      if (createOrEdit.Id != null)
        permission = this.RequestHandler.Storage.GetRepository<IPermissionRepository>().WithKey((int)createOrEdit.Id);

      permission.Code = createOrEdit.Code;
      permission.Name = createOrEdit.Name;
      permission.Position = createOrEdit.Position;
      return permission;
    }
  }
}