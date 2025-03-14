// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation.AspNetCore;
using Magicalizer.Api.Controllers;
using Magicalizer.Domain.Services.Abstractions;
using Magicalizer.Validators.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Api.Dto;
using Platformus.Core.Api.Services.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Api.Controllers;

[Route("api/v1/credentials")]
public class CredentialsController : DefaultController<int, Domain.Models.Credential, Credential, CredentialFilter>
{
  private readonly IPasswordHasher passwordHasher;

  public CredentialsController(IAuthorizationService authorizationService, IService<int, Domain.Models.Credential, CredentialFilter> service, IPasswordHasher passwordHasher)
    : base(authorizationService, service)
  {
    this.passwordHasher = passwordHasher;
  }

  [HttpPost]
  public override async Task<ActionResult<Credential>> PostAsync([FromBody][CustomizeValidator(RuleSet = RuleSetName.DefaultCreate)] Credential credential)
  {
    if (credential.CredentialType?.Id == Domain.Models.CredentialTypes.EmailAndPassword)
    {
      byte[] salt = this.passwordHasher.GenerateRandomSalt();

      credential.Secret = this.passwordHasher.ComputeHash(credential.Secret!, salt);
      credential.Extra = Convert.ToBase64String(salt);
    }

    return await base.PostAsync(credential);
  }

  [HttpPut]
  public override async Task<IActionResult> PutAsync([FromBody][CustomizeValidator(RuleSet = RuleSetName.DefaultEdit)] Credential credential)
  {
    if (credential.CredentialType?.Id == Domain.Models.CredentialTypes.EmailAndPassword)
    {
      byte[] salt = this.passwordHasher.GenerateRandomSalt();

      credential.Secret = this.passwordHasher.ComputeHash(credential.Secret!, salt);
      credential.Extra = Convert.ToBase64String(salt);
    }

    return await base.PutAsync(credential);
  }
}