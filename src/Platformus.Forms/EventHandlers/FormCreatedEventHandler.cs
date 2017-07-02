// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms
{
  public class FormCreatedEventHandler : IFormCreatedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, Form form)
    {
      new SerializationManager(requestHandler).SerializeForm(form);
    }
  }
}