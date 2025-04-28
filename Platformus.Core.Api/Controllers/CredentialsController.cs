// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation.AspNetCore;
using Magicalizer.Api.Controllers;
using Magicalizer.Domain.Services.Abstractions;
using Magicalizer.Validators.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Api.Dto;
using Platformus.Core.Api.Services.Abstractions;
using Platformus.Core.Filters;
using HttpMethod = Magicalizer.Api.Dto.Abstractions.HttpMethod;

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

  [HttpGet("{id}")]
  [Authorize(Policy = $"{nameof(Credential)}.{nameof(HttpMethod.Get)}")]
  public override async Task<ActionResult<Credential>> GetAsync(int id, string? fields = null)
  {
    return await base.GetAsync(id, fields);
  }

  [HttpGet]
  [Authorize(Policy = $"{nameof(Credential)}.{nameof(HttpMethod.Get)}")]
  public override async Task<ActionResult<IEnumerable<Credential>>> GetAsync([FromQuery] CredentialFilter? filter = null, string? sorting = null, int? offset = null, int? limit = null, string? fields = null)
  {
    return await base.GetAsync(filter, sorting, offset, limit, fields);
  }

  [HttpPost]
  [Authorize(Policy = $"{nameof(Credential)}.{nameof(HttpMethod.Post)}")]
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
  [Authorize(Policy = $"{nameof(Credential)}.{nameof(HttpMethod.Put)}")]
  public override async Task<IActionResult> PutAsync([FromBody][CustomizeValidator(RuleSet = RuleSetName.DefaultEdit)] Credential credential)
  {
    if (credential.CredentialType?.Id == Domain.Models.CredentialTypes.EmailAndPassword)
    {
      if (string.IsNullOrEmpty(credential.Secret))
      {
        Domain.Models.Credential? _credential = await this.service.GetByIdAsync(credential.Id);

        if (_credential == null)
          return this.NotFound();

        credential.Secret = _credential.Secret;
        credential.Extra = _credential.Extra;
      }

      else
      {
        byte[] salt = this.passwordHasher.GenerateRandomSalt();

        credential.Secret = this.passwordHasher.ComputeHash(credential.Secret!, salt);
        credential.Extra = Convert.ToBase64String(salt);
      }
    }

    return await base.PutAsync(credential);
  }

  [HttpPatch("{id}")]
  [Authorize(Policy = $"{nameof(Credential)}.{nameof(HttpMethod.Patch)}")]
  public override async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<Credential> dtoPatch)
  {
    return await base.PatchAsync(id, dtoPatch);
  }

  [HttpDelete("{id}")]
  [Authorize(Policy = $"{nameof(Credential)}.{nameof(HttpMethod.Delete)}")]
  public override async Task<IActionResult> DeleteAsync(int id)
  {
    return await base.DeleteAsync(id);
  }
}