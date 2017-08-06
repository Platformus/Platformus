// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Primitives;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Credentials
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
          CredentialTypeOptions = this.GetCredentialTypeOptions()
        };

      Credential credential = this.RequestHandler.Storage.GetRepository<ICredentialRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = credential.Id,
        CredentialTypeId = credential.CredentialTypeId,
        CredentialTypeOptions = this.GetCredentialTypeOptions(),
        Identifier = credential.Identifier
      };
    }

    private IEnumerable<Option> GetCredentialTypeOptions()
    {
      return this.RequestHandler.Storage.GetRepository<ICredentialTypeRepository>().All().Select(
        ct => new Option(ct.Name, ct.Id.ToString())
      );
    }
  }
}