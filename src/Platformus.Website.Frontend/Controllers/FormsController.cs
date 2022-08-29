// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Data.Entities;
using Platformus.Website.FieldValidators;
using Platformus.Website.Filters;
using Platformus.Website.FormHandlers;

namespace Platformus.Website.Frontend.Controllers
{
  public class FormsController : Core.Frontend.Controllers.ControllerBase
  {
    public FormsController(IStorage storage)
      : base(storage)
    {
    }
    
    // TODO: do not return bad request
    [HttpPost]
    public async Task<IActionResult> SendAsync(int formId, string origin)
    {
      Form form = await this.Storage.GetRepository<int, Form, FormFilter>().GetByIdAsync(
        formId,
        new Inclusion<Form>(f => f.Name.Localizations),
        new Inclusion<Form>("Fields.FieldType"),
        new Inclusion<Form>("Fields.Name.Localizations")
      );

      IDictionary<Field, string> valuesByFields = this.GetValuesByFields(form);

      foreach (Field field in valuesByFields.Keys)
        if (!string.IsNullOrEmpty(field.FieldType.ValidatorCSharpClassName))
          if (!await this.CreateFieldValidator(field.FieldType).ValidateAsync(this.HttpContext, form, field, valuesByFields[field]))
            return this.BadRequest();

      IDictionary<string, byte[]> attachmentsByFilenames = this.GetAttachmentsByFilenames(form);

      if (form.ProduceCompletedForms)
        await this.ProduceCompletedForms(form, valuesByFields, attachmentsByFilenames);

      IFormHandler formHandler = StringActivator.CreateInstance<IFormHandler>(form.FormHandlerCSharpClassName);

      if (formHandler != null)
        return await formHandler.HandleAsync(this.HttpContext, origin, form, valuesByFields, attachmentsByFilenames);

      return this.NotFound();
    }

    private IDictionary<Field, string> GetValuesByFields(Form form)
    {
      IDictionary<Field, string> valuesByFields = new Dictionary<Field, string>();

      foreach (Field field in form.Fields)
        if (field.FieldType.Code != "FileUpload")
          valuesByFields.Add(field, this.Request.Form[string.Format("field{0}", field.Code)]);

      return valuesByFields;
    }

    private IFieldValidator CreateFieldValidator(FieldType fieldType)
    {
      return StringActivator.CreateInstance<IFieldValidator>(fieldType.ValidatorCSharpClassName);
    }

    private IDictionary<string, byte[]> GetAttachmentsByFilenames(Form form)
    {
      IDictionary<string, byte[]> attachmentsByFilenames = new Dictionary<string, byte[]>();

      foreach (Field field in form.Fields)
      {
        if (field.FieldType.Code == "FileUpload")
        {
          IFormFile file = this.Request.Form.Files[string.Format("field{0}", field.Code)];

          if (file != null && !string.IsNullOrEmpty(file.FileName))
          {
            string filename = file.FileName;

            if (!string.IsNullOrEmpty(filename) && filename.Contains(Path.DirectorySeparatorChar.ToString()))
              filename = Path.GetFileName(filename);

            attachmentsByFilenames.Add(filename, this.GetBytesFromStream(file.OpenReadStream()));
          }
        }
      }

      return attachmentsByFilenames;
    }

    private async Task ProduceCompletedForms(Form form, IDictionary<Field, string> valuesByFields, IDictionary<string, byte[]> attachmentsByFilenames)
    {
      CompletedForm completedForm = new CompletedForm();

      completedForm.FormId = form.Id;
      completedForm.Created = DateTime.Now.ToUniversalTime();
      this.Storage.GetRepository<int, CompletedForm, CompletedFormFilter>().Create(completedForm);
      await this.Storage.SaveAsync();

      foreach (KeyValuePair<Field, string> valueByField in valuesByFields)
      {
        CompletedField completedField = new CompletedField();

        completedField.CompletedFormId = completedForm.Id;
        completedField.FieldId = valueByField.Key.Id;
        completedField.Value = valueByField.Value;
        this.Storage.GetRepository<int, CompletedField, CompletedFieldFilter>().Create(completedField);
      }

      await this.Storage.SaveAsync();
    }

    private byte[] GetBytesFromStream(Stream input)
    {
      using (MemoryStream output = new MemoryStream())
      {
        input.CopyTo(output);
        return output.ToArray();
      }
    }
  }
}