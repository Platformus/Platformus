// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Forms.FormHandlers;

namespace Platformus.Forms.Frontend.Controllers
{
  [AllowAnonymous]
  public class FormsController : Platformus.Barebone.Frontend.Controllers.ControllerBase
  {
    public FormsController(IStorage storage)
      : base(storage)
    {
    }

    [HttpPost]
    public IActionResult Send(int formId, string formPageUrl)
    {
      Form form = this.Storage.GetRepository<IFormRepository>().WithKey(formId);
      IDictionary<Field, string> valuesByFields = this.GetValuesByFields(form);
      IDictionary<string, byte[]> attachmentsByFilenames = this.GetAttachmentsByFilenames(form);

      if (form.ProduceCompletedForms)
        this.ProduceCompletedForms(form, valuesByFields, attachmentsByFilenames);

      IFormHandler formHandler = StringActivator.CreateInstance<IFormHandler>(form.CSharpClassName);

      if (formHandler != null)
        return formHandler.Handle(this, form, valuesByFields, attachmentsByFilenames, formPageUrl);

      return this.NotFound();
    }

    private IDictionary<Field, string> GetValuesByFields(Form form)
    {
      IDictionary<Field, string> valuesByFields = new Dictionary<Field, string>();

      foreach (Field field in this.Storage.GetRepository<IFieldRepository>().FilteredByFormId(form.Id))
      {
        FieldType fieldType = this.Storage.GetRepository<IFieldTypeRepository>().WithKey(field.FieldTypeId);

        if (fieldType.Code != "FileUpload")
          valuesByFields.Add(field, this.Request.Form[string.Format("field{0}", field.Code)]);
      }

      return valuesByFields;
    }

    private IDictionary<string, byte[]> GetAttachmentsByFilenames(Form form)
    {
      IDictionary<string, byte[]> attachmentsByFilenames = new Dictionary<string, byte[]>();

      foreach (Field field in this.Storage.GetRepository<IFieldRepository>().FilteredByFormId(form.Id))
      {
        FieldType fieldType = this.Storage.GetRepository<IFieldTypeRepository>().WithKey(field.FieldTypeId);

        if (fieldType.Code == "FileUpload")
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

    private void ProduceCompletedForms(Form form, IDictionary<Field, string> valuesByFields, IDictionary<string, byte[]> attachmentsByFilenames)
    {
      CompletedForm completedForm = new CompletedForm();

      completedForm.FormId = form.Id;
      completedForm.Created = DateTime.Now;
      this.Storage.GetRepository<ICompletedFormRepository>().Create(completedForm);
      this.Storage.Save();

      foreach (KeyValuePair<Field, string> valueByField in valuesByFields)
      {
        CompletedField completedField = new CompletedField();

        completedField.CompletedFormId = completedForm.Id;
        completedField.FieldId = valueByField.Key.Id;
        completedField.Value = valueByField.Value;
        this.Storage.GetRepository<ICompletedFieldRepository>().Create(completedField);
      }

      this.Storage.Save();
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