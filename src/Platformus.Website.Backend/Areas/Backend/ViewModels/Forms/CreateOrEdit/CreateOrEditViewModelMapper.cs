// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Forms;

public static class CreateOrEditViewModelMapper
{
  public static Form Map(Form form, CreateOrEditViewModel createOrEdit)
  {
    form.Code = createOrEdit.Code;
    form.ProduceCompletedForms = createOrEdit.ProduceCompletedForms;
    form.FormHandlerCSharpClassName = createOrEdit.FormHandlerCSharpClassName;
    form.FormHandlerParameters = createOrEdit.FormHandlerParameters;
    return form;
  }
}