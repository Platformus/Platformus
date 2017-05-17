// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Models;

namespace Platformus.Forms
{
  public class FormEditedEventHandler : IFormEditedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, Form oldForm, Form newForm)
    {
      new SerializationManager(requestHandler).SerializeForm(newForm);
    }
  }
}