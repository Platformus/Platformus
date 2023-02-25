// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Endpoints;

public static class CreateOrEditViewModelMapper
{
  public static Endpoint Map(Endpoint endpoint, CreateOrEditViewModel createOrEdit)
  {
    endpoint.Name = createOrEdit.Name;
    endpoint.UrlTemplate = createOrEdit.UrlTemplate;
    endpoint.Position = createOrEdit.Position;
    endpoint.DisallowAnonymous = createOrEdit.DisallowAnonymous;
    endpoint.SignInUrl = createOrEdit.DisallowAnonymous ? createOrEdit.SignInUrl : null;
    endpoint.RequestProcessorCSharpClassName = createOrEdit.RequestProcessorCSharpClassName;
    endpoint.RequestProcessorParameters = createOrEdit.RequestProcessorParameters;
    endpoint.ResponseCacheCSharpClassName = createOrEdit.ResponseCacheCSharpClassName;
    endpoint.ResponseCacheParameters = createOrEdit.ResponseCacheParameters;
    return endpoint;
  }
}