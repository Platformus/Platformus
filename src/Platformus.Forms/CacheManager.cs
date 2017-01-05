// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Models;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Forms
{
  public class CacheManager
  {
    public IRequestHandler handler;

    public CacheManager(IRequestHandler requestHandler)
    {
      this.handler = handler;
    }

    public void CacheForm(Form form)
    {
      foreach (Culture culture in this.handler.Storage.GetRepository<ICultureRepository>().NotNeutral())
      {
        CachedForm cachedForm = this.handler.Storage.GetRepository<ICachedFormRepository>().WithKey(culture.Id, form.Id);

        if (cachedForm == null)
          this.handler.Storage.GetRepository<ICachedFormRepository>().Create(this.CacheForm(culture, form));

        else
        {
          CachedForm temp = this.CacheForm(culture, form);

          cachedForm.Code = temp.Code;
          cachedForm.Name = temp.Name;
          cachedForm.CachedFields = temp.CachedFields;
          this.handler.Storage.GetRepository<ICachedFormRepository>().Edit(cachedForm);
        }
      }

      this.handler.Storage.Save();
    }

    private CachedForm CacheForm(Culture culture, Form form)
    {
      List<CachedField> cachedFields = new List<CachedField>();

      foreach (Field field in this.handler.Storage.GetRepository<IFieldRepository>().FilteredByFormId(form.Id))
        cachedFields.Add(this.CacheField(culture, field));

      CachedForm cachedForm = new CachedForm();

      cachedForm.FormId = form.Id;
      cachedForm.CultureId = culture.Id;
      cachedForm.Code = form.Code;
      cachedForm.Name = this.GetLocalizationValue(culture.Id, form.NameId);

      if (cachedFields.Count != 0)
        cachedForm.CachedFields = this.SerializeObject(cachedFields);

      return cachedForm;
    }

    private CachedField CacheField(Culture culture, Field field)
    {
      List<CachedFieldOption> cachedFieldOptions = new List<CachedFieldOption>();

      foreach (FieldOption fieldOption in this.handler.Storage.GetRepository<IFieldOptionRepository>().FilteredByFieldId(field.Id))
        cachedFieldOptions.Add(this.CacheFieldOption(culture, fieldOption));

      CachedField cachedField = new CachedField();

      cachedField.FieldId = field.Id;
      cachedField.FieldTypeCode = this.handler.Storage.GetRepository<IFieldTypeRepository>().WithKey(field.FieldTypeId).Code;
      cachedField.Name = this.GetLocalizationValue(culture.Id, field.NameId);
      cachedField.Position = field.Position;

      if (cachedFieldOptions.Count != 0)
        cachedField.CachedFieldOptions = this.SerializeObject(cachedFieldOptions);

      return cachedField;
    }

    private CachedFieldOption CacheFieldOption(Culture culture, FieldOption fieldOption)
    {
      CachedFieldOption cachedFieldOption = new CachedFieldOption();

      cachedFieldOption.FieldOptionId = fieldOption.Id;
      cachedFieldOption.Value = this.GetLocalizationValue(culture.Id, fieldOption.ValueId);
      cachedFieldOption.Position = fieldOption.Position;
      return cachedFieldOption;
    }

    private string GetLocalizationValue(int cultureId, int dictionaryId)
    {
      Localization localization = this.handler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId(dictionaryId, cultureId);

      if (localization == null)
        return null;

      return localization.Value;
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