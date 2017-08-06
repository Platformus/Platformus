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
  public class FormEditedEventHandler : IFormEditedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, Form oldForm, Form newForm)
    {
      new SerializationManager(requestHandler).SerializeForm(newForm);

      if (oldForm != null)
        foreach (Culture culture in CultureManager.GetNotNeutralCultures(requestHandler.Storage))
          requestHandler.HttpContext.RequestServices.GetService<ICache>().RemoveFormViewComponentResult(oldForm.Code, culture.Code);

      foreach (Culture culture in CultureManager.GetNotNeutralCultures(requestHandler.Storage))
        requestHandler.HttpContext.RequestServices.GetService<ICache>().RemoveFormViewComponentResult(newForm.Code, culture.Code);
    }
  }
}