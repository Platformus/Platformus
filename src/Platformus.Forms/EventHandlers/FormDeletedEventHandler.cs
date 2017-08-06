// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;
using Platformus.Forms.Data.Entities;
using Platformus.Forms.Events;
using Platformus.Globalization;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Forms.EventHandlers
{
  public class FormDeletedEventHandler : IFormDeletedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, Form form)
    {
      foreach (Culture culture in CultureManager.GetNotNeutralCultures(requestHandler.Storage))
        requestHandler.HttpContext.RequestServices.GetService<ICache>().RemoveFormViewComponentResult(form.Code, culture.Code);
    }
  }
}