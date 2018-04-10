// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Backend.ViewModels.Endpoints
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Endpoint Map(CreateOrEditViewModel createOrEdit)
    {
      Endpoint endpoint = new Endpoint();

      if (createOrEdit.Id != null)
        endpoint = this.RequestHandler.Storage.GetRepository<IEndpointRepository>().WithKey((int)createOrEdit.Id);

      endpoint.Name = createOrEdit.Name;
      endpoint.UrlTemplate = createOrEdit.UrlTemplate;
      endpoint.Position = createOrEdit.Position;
      endpoint.DisallowAnonymous = createOrEdit.DisallowAnonymous;
      endpoint.SignInUrl = createOrEdit.SignInUrl;
      endpoint.CSharpClassName = createOrEdit.CSharpClassName;
      endpoint.Parameters = createOrEdit.Parameters;
      return endpoint;
    }
  }
}