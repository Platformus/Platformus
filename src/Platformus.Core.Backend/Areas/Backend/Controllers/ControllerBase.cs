// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ExtCore.Data.Entities.Abstractions;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Area("Backend")]
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
      this.ConvertDateTimeToUniversalTime(actionExecutingContext);
      this.HandleViewModelMultilingualProperties(actionExecutingContext);
      base.OnActionExecuting(actionExecutingContext);
    }

    protected async Task MergeEntityLocalizationsAsync(IEntity entity)
    {
      foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(Dictionary)))
      {
        Dictionary dictionary = propertyInfo.GetValue(entity) as Dictionary;

        if (dictionary == null)
        {
          dictionary = new Dictionary();
          this.DictionaryRepository.Create(dictionary);
          propertyInfo.SetValue(entity, dictionary);
        }

        await this.MergeDictionaryLocalizationsAsync(dictionary, propertyInfo);
      }
    }

    private async Task MergeDictionaryLocalizationsAsync(Dictionary dictionary, PropertyInfo propertyInfo)
    {
      foreach (Culture culture in await this.HttpContext.GetCultureManager().GetNotNeutralCulturesAsync())
      {
        string identity = propertyInfo.Name + culture.Id;
        string value = this.Request.Form[identity];

        Localization localization = dictionary.Localizations?.FirstOrDefault(l => l.CultureId == culture.Id);

        if (localization == null)
        {
          localization = new Localization();
          localization.Dictionary = dictionary.Id == 0 ? dictionary : null;
          localization.DictionaryId = dictionary.Id;
          localization.CultureId = culture.Id;
          localization.Value = value;
          this.LocalizationRepository.Create(localization);
        }

        else if (localization.Value != value)
        {
          localization.Value = value;
          this.LocalizationRepository.Edit(localization);
        }
      }
    }

    private void ConvertDateTimeToUniversalTime(ActionExecutingContext actionExecutingContext)
    {
      foreach (string key in actionExecutingContext.ActionArguments.Keys)
      {
        if (actionExecutingContext.ActionArguments[key] is DateTime?)
        {
          DateTime? value = actionExecutingContext.ActionArguments[key] as DateTime?;

          if (value != null)
            actionExecutingContext.ActionArguments[key] = value.Value.ToUniversalTime();
        }

        else this.ProcessArgument(actionExecutingContext.ActionArguments[key]);
      }
    }

    private void ProcessArgument(object argument)
    {
      if (argument == null) return;

      if (argument.GetType().IsPrimitive || argument is string) return;

      if (argument is IEnumerable arguments)
      {
        foreach (object a in arguments)
          this.ProcessArgument(a);

        return;
      }

      foreach (PropertyInfo propertyInfo in argument.GetType().GetProperties().Where(p => p.GetIndexParameters().Length == 0))
      {
        if (propertyInfo.PropertyType.IsAssignableTo(typeof(DateTime?)))
        {
          DateTime? value = (DateTime?)propertyInfo.GetValue(argument);

          if (value != null)
            propertyInfo.SetValue(argument, value.Value.ToUniversalTime());
        }

        else this.ProcessArgument(propertyInfo.GetValue(argument));
      }
    }

    private void HandleViewModelMultilingualProperties(ActionExecutingContext actionExecutingContext)
    {
      ViewModelBase viewModel = this.GetViewModelFromActionExecutingContext(actionExecutingContext);

      if (viewModel == null) return;

      try
      {
        foreach (PropertyInfo propertyInfo in this.GetMultilingualPropertiesFromViewModel(viewModel))
        {
          this.ModelState.Remove(propertyInfo.Name);

          foreach (Culture culture in this.HttpContext.GetCultureManager().GetNotNeutralCulturesAsync().Result)
          {
            string identity = propertyInfo.Name + culture.Id;
            string value = this.Request.Form[identity];

            this.ModelState.SetModelValue(identity, value, value);
            this.ModelState[identity].ValidationState = ModelValidationState.Valid;

            foreach (ValidationAttribute validationAttribute in propertyInfo.GetCustomAttributes<ValidationAttribute>())
            {
              if (!validationAttribute.IsValid(value))
              {
                ValidationAttributeLocalizer.Localize(validationAttribute, this.HttpContext.GetStringLocalizer<SharedResource>());
                this.ModelState[identity].ValidationState = ModelValidationState.Invalid;
                this.ModelState[identity].Errors.Add(validationAttribute.ErrorMessage);
              }
            }
          }
        }
      }

      catch { }
    }

    private ViewModelBase GetViewModelFromActionExecutingContext(ActionExecutingContext actionExecutingContext)
    {
      return actionExecutingContext.ActionArguments.Values.FirstOrDefault(v => v is ViewModelBase) as ViewModelBase;
    }

    private IEnumerable<PropertyInfo> GetMultilingualPropertiesFromViewModel(ViewModelBase viewModel)
    {
      return viewModel
        .GetType()
        .GetProperties()
        .Where(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(MultilingualAttribute)))
        .ToList();
    }
  }
}
