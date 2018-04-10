// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Forms
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Form Map(CreateOrEditViewModel createOrEdit)
    {
      Form form = new Form();

      if (createOrEdit.Id != null)
        form = this.RequestHandler.Storage.GetRepository<IFormRepository>().WithKey((int)createOrEdit.Id);

      form.Code = createOrEdit.Code;
      form.ProduceCompletedForms = createOrEdit.ProduceCompletedForms;
      form.CSharpClassName = createOrEdit.CSharpClassName;
      form.Parameters = createOrEdit.Parameters;
      return form;
    }
  }
}