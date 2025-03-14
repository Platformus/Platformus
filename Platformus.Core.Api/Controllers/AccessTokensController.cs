// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Api.Services.Abstractions;

namespace Platformus.Core.Api.Controllers;

[Route("api/v1/access-tokens")]
public class AccessTokensController : Controller
{
  private readonly IEnumerable<IAccessTokenService> accessTokenServices;

  public AccessTokensController(IEnumerable<IAccessTokenService> accessTokenServices)
  {
    this.accessTokenServices = accessTokenServices;
  }

  [HttpPost]
  public async Task<ActionResult<Dto.AccessToken.Get.AccessToken>> PostAsync([FromBody] Dto.AccessToken.Post.AccessToken accessToken)
  {
    Dto.AccessToken.Get.AccessToken? result = null;

    foreach (IAccessTokenService accessTokenService in accessTokenServices)
    {
      result = await accessTokenService.CreateAsync(accessToken);

      if (result != null) break;
    }  

    if (result == null)
      return this.BadRequest();

    return this.CreatedAtAction(null, result);
  }
}
