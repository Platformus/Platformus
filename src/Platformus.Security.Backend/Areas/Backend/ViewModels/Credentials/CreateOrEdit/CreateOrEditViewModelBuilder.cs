// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Backend.ViewModels.Credentials
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
          CredentialTypeOptions = this.GetCredentialTypeOptions()
        };

      Credential credential = this.handler.Storage.GetRepository<ICredentialRepository>().WithKey((int)id);

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
      return this.handler.Storage.GetRepository<ICredentialTypeRepository>().All().Select(
        ct => new Option(ct.Name, ct.Id.ToString())
      );
    }
  }
}