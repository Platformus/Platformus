// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using ExtCore.Data.Abstractions;
using ExtCore.Data.Entities.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;
using Platformus.Globalization.Services.Abstractions;

namespace Platformus.Globalization.Backend.Controllers
{
  public abstract class ControllerBase : Platformus.Barebone.Backend.Controllers.ControllerBase
  {
    public ControllerBase(IStorage storage)
      : base(storage)
    {
    }

    public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
    {
      this.HandleViewModelMultilingualProperties(actionExecutingContext);
      base.OnActionExecuting(actionExecutingContext);
    }

    protected void CreateOrEditEntityLocalizations(IEntity entity)
    {
      foreach (PropertyInfo propertyInfo in this.GetDictionaryPropertiesFromEntity(entity))
      {
        Dictionary dictionary = this.GetOrCreateDictionaryForProperty(entity, propertyInfo);

        this.DeleteLocalizations(dictionary);
        this.CreateLocalizations(propertyInfo, dictionary);
      }
    }

    private IEnumerable<PropertyInfo> GetDictionaryPropertiesFromEntity(IEntity entity)
    {
      return entity.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(Dictionary));
    }

    private Dictionary GetOrCreateDictionaryForProperty(IEntity entity, PropertyInfo propertyInfo)
    {
      PropertyInfo dictionaryIdPropertyInfo = entity.GetType().GetProperty(propertyInfo.Name + "Id");
      int? dictionaryId = (int?)dictionaryIdPropertyInfo.GetValue(entity);
      Dictionary dictionary = null;

      if (dictionaryId == null || dictionaryId == 0)
      {
        dictionary = new Dictionary();
        this.Storage.GetRepository<IDictionaryRepository>().Create(dictionary);
        this.Storage.Save();
        dictionaryIdPropertyInfo.SetValue(entity, dictionary.Id);
      }

      else dictionary = this.Storage.GetRepository<IDictionaryRepository>().WithKey((int)dictionaryId);

      return dictionary;
    }

    private void DeleteLocalizations(Dictionary dictionary)
    {
      foreach (Localization localization in this.Storage.GetRepository<ILocalizationRepository>().FilteredByDictionaryId(dictionary.Id))
        this.Storage.GetRepository<ILocalizationRepository>().Delete(localization);

      this.Storage.Save();
    }

    private void CreateLocalizations(PropertyInfo propertyInfo, Dictionary dictionary)
    {
      foreach (Culture culture in this.GetService<ICultureManager>().GetNotNeutralCultures())
      {
        Localization localization = new Localization();

        localization.DictionaryId = dictionary.Id;
        localization.CultureId = culture.Id;

        string identity = propertyInfo.Name + culture.Code;
        string value = this.Request.Form[identity];

        localization.Value = value;
        this.Storage.GetRepository<ILocalizationRepository>().Create(localization);
      }

      this.Storage.Save();
    }

    private void HandleViewModelMultilingualProperties(ActionExecutingContext actionExecutingContext)
    {
      ViewModelBase viewModel = this.GetViewModelFromActionExecutingContext(actionExecutingContext);

      if (viewModel == null)
        return;

      try
      {
        foreach (PropertyInfo propertyInfo in this.GetMultilingualPropertiesFromViewModel(viewModel))
        {
          this.ModelState.Remove(propertyInfo.Name);

          bool hasRequiredAttribute = propertyInfo.CustomAttributes.Any(ca => ca.AttributeType == typeof(RequiredAttribute));

          foreach (Culture culture in this.GetService<ICultureManager>().GetNotNeutralCultures())
          {
            string identity = propertyInfo.Name + culture.Code;
            string value = this.Request.Form[identity];

            this.ModelState.SetModelValue(identity, value, value);

            if (hasRequiredAttribute && string.IsNullOrEmpty(value))
            {
              this.ModelState[identity].ValidationState = ModelValidationState.Invalid;
              this.ModelState[identity].Errors.Add(string.Empty);
            }

            else this.ModelState[identity].ValidationState = ModelValidationState.Valid;
          }
        }
      }

      catch { }
    }

    private ViewModelBase GetViewModelFromActionExecutingContext(ActionExecutingContext actionExecutingContext)
    {
      foreach (KeyValuePair<string, object> actionArgument in actionExecutingContext.ActionArguments)
        if (actionArgument.Value is ViewModelBase)
          return actionArgument.Value as ViewModelBase;

      return null;
    }

    private IEnumerable<PropertyInfo> GetMultilingualPropertiesFromViewModel(ViewModelBase viewModel)
    {
      return viewModel.GetType().GetProperties().Where(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(MultilingualAttribute)));
    }
  }
}