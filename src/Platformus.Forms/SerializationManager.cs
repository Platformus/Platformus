// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Forms
{
  public class SerializationManager
  {
    public IRequestHandler requestHandler;

    public SerializationManager(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
    }

    public void SerializeForm(Form form)
    {
      foreach (Culture culture in this.requestHandler.Storage.GetRepository<ICultureRepository>().NotNeutral().ToList())
      {
        SerializedForm serializedForm = this.requestHandler.Storage.GetRepository<ISerializedFormRepository>().WithKey(culture.Id, form.Id);

        if (serializedForm == null)
          this.requestHandler.Storage.GetRepository<ISerializedFormRepository>().Create(this.SerializeForm(culture, form));

        else
        {
          SerializedForm temp = this.SerializeForm(culture, form);

          serializedForm.Code = temp.Code;
          serializedForm.Name = temp.Name;
          serializedForm.SubmitButtonTitle = temp.SubmitButtonTitle;
          serializedForm.SerializedFields = temp.SerializedFields;
          this.requestHandler.Storage.GetRepository<ISerializedFormRepository>().Edit(serializedForm);
        }
      }

      this.requestHandler.Storage.Save();
    }

    private SerializedForm SerializeForm(Culture culture, Form form)
    {
      List<SerializedField> serializedFields = new List<SerializedField>();

      foreach (Field field in this.requestHandler.Storage.GetRepository<IFieldRepository>().FilteredByFormId(form.Id).ToList())
        serializedFields.Add(this.SerializeField(culture, field));

      SerializedForm serializedForm = new SerializedForm();

      serializedForm.FormId = form.Id;
      serializedForm.CultureId = culture.Id;
      serializedForm.Code = form.Code;
      serializedForm.Name = this.requestHandler.GetLocalizationValue(form.NameId, culture.Id);
      serializedForm.SubmitButtonTitle = this.requestHandler.GetLocalizationValue(form.SubmitButtonTitleId, culture.Id);

      if (serializedFields.Count != 0)
        serializedForm.SerializedFields = this.SerializeObject(serializedFields);

      return serializedForm;
    }

    private SerializedField SerializeField(Culture culture, Field field)
    {
      List<SerializedFieldOption> serializedFieldOptions = new List<SerializedFieldOption>();

      foreach (FieldOption fieldOption in this.requestHandler.Storage.GetRepository<IFieldOptionRepository>().FilteredByFieldId(field.Id).ToList())
        serializedFieldOptions.Add(this.SerializeFieldOption(culture, fieldOption));

      SerializedField serializedField = new SerializedField();

      serializedField.FieldId = field.Id;
      serializedField.FieldTypeCode = this.requestHandler.Storage.GetRepository<IFieldTypeRepository>().WithKey(field.FieldTypeId).Code;
      serializedField.Code = field.Code;
      serializedField.Name = this.requestHandler.GetLocalizationValue(field.NameId, culture.Id);
      serializedField.IsRequired = field.IsRequired;
      serializedField.MaxLength = field.MaxLength;
      serializedField.Position = field.Position;

      if (serializedFieldOptions.Count != 0)
        serializedField.SerializedFieldOptions = this.SerializeObject(serializedFieldOptions);

      return serializedField;
    }

    private SerializedFieldOption SerializeFieldOption(Culture culture, FieldOption fieldOption)
    {
      SerializedFieldOption serializedFieldOption = new SerializedFieldOption();

      serializedFieldOption.FieldOptionId = fieldOption.Id;
      serializedFieldOption.Value = this.requestHandler.GetLocalizationValue(fieldOption.ValueId, culture.Id);
      serializedFieldOption.Position = fieldOption.Position;
      return serializedFieldOption;
    }

    private string SerializeObject(object value)
    {
      string result = JsonConvert.SerializeObject(value);

      if (string.IsNullOrEmpty(result))
        return null;

      return result;
    }
  }
}