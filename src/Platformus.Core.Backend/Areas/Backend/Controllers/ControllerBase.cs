// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ExtCore.Data.Entities.Abstractions;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Data.Entities;
using Platformus.Core.Extensions;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Authorize(AuthenticationSchemes = BackendCookieAuthenticationDefaults.AuthenticationScheme)]
  public abstract class ControllerBase : Core.Controllers.ControllerBase
  {
    private IRepository<int, Dictionary, DictionaryFilter> DictionaryRepository
    {
      get => this.Storage.GetRepository<int, Dictionary, DictionaryFilter>();
    }

    private IRepository<int, Localization, LocalizationFilter> LocalizationRepository
    {
      get => this.Storage.GetRepository<int, Localization, LocalizationFilter>();
    }

    public ControllerBase(IStorage storage)
      : base(storage)
    {
    }

    public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
    {
      this.HandleViewModelMultilingualProperties(actionExecutingContext);
      base.OnActionExecuting(actionExecutingContext);
    }

    protected async Task CreateOrEditEntityLocalizationsAsync(IEntity entity)
    {
      foreach (PropertyInfo propertyInfo in this.GetDictionaryPropertiesFromEntity(entity))
      {
        Dictionary dictionary = await this.GetOrCreateDictionaryForPropertyAsync(entity, propertyInfo);

        await this.DeleteLocalizationsAsync(dictionary);
        await this.CreateLocalizationsAsync(propertyInfo, dictionary);
      }
    }

    private IEnumerable<PropertyInfo> GetDictionaryPropertiesFromEntity(IEntity entity)
    {
      return entity.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(Dictionary));
    }

    private async Task<Dictionary> GetOrCreateDictionaryForPropertyAsync(IEntity entity, PropertyInfo propertyInfo)
    {
      PropertyInfo dictionaryIdPropertyInfo = entity.GetType().GetProperty(propertyInfo.Name + "Id");
      int? dictionaryId = (int?)dictionaryIdPropertyInfo.GetValue(entity);
      Dictionary dictionary = null;

      if (dictionaryId == null || dictionaryId == 0)
      {
        dictionary = new Dictionary();
        this.DictionaryRepository.Create(dictionary);
        await this.Storage.SaveAsync();
        dictionaryIdPropertyInfo.SetValue(entity, dictionary.Id);
      }

      else dictionary = await this.DictionaryRepository.GetByIdAsync((int)dictionaryId);

      return dictionary;
    }

    private async Task DeleteLocalizationsAsync(Dictionary dictionary)
    {
      foreach (Localization localization in await this.LocalizationRepository.GetAllAsync(new LocalizationFilter() { Dictionary = new DictionaryFilter() { Id = dictionary.Id } }))
        this.LocalizationRepository.Delete(localization.Id);

      await this.Storage.SaveAsync();
    }

    private async Task CreateLocalizationsAsync(PropertyInfo propertyInfo, Dictionary dictionary)
    {
      foreach (Culture culture in await this.HttpContext.GetCultureManager().GetNotNeutralCulturesAsync())
      {
        Localization localization = new Localization();

        localization.DictionaryId = dictionary.Id;
        localization.CultureId = culture.Id;

        string identity = propertyInfo.Name + culture.Code;
        string value = this.Request.Form[identity];

        localization.Value = value;
        this.LocalizationRepository.Create(localization);
      }

      await this.Storage.SaveAsync();
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

          foreach (Culture culture in this.HttpContext.GetCultureManager().GetNotNeutralCulturesAsync().Result)
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