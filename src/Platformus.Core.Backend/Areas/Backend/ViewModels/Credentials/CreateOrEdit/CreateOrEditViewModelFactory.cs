// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend.ViewModels.Credentials
{
  public static class CreateOrEditViewModelFactory
  {
    public static async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Credential credential)
    {
      if (credential == null)
        return new CreateOrEditViewModel()
        {
          CredentialTypeOptions = await GetCredentialTypeOptionsAsync(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = credential.Id,
        CredentialTypeId = credential.CredentialTypeId,
        CredentialTypeOptions = await GetCredentialTypeOptionsAsync(httpContext),
        Identifier = credential.Identifier
      };
    }

    private static async Task<IEnumerable<Option>> GetCredentialTypeOptionsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, CredentialType, CredentialTypeFilter>().GetAllAsync()).Select(
        ct => new Option(ct.Name, ct.Id.ToString())
      );
    }
  }
}