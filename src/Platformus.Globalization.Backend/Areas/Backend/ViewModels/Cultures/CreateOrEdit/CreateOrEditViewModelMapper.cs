// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Backend.ViewModels.Cultures
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Culture Map(CreateOrEditViewModel createOrEdit)
    {
      Culture culture = new Culture();

      if (createOrEdit.Id != null)
        culture = this.RequestHandler.Storage.GetRepository<ICultureRepository>().WithKey((int)createOrEdit.Id);

      culture.Code = createOrEdit.Code;
      culture.Name = createOrEdit.Name;
      culture.IsFrontendDefault = createOrEdit.IsFrontendDefault;
      culture.IsBackendDefault = createOrEdit.IsBackendDefault;
      return culture;
    }
  }
}