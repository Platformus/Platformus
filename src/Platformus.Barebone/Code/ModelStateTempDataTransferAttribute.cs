// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Platformus.Barebone.Controllers;

namespace Platformus
{
  public abstract class ModelStateTempDataTransferAttribute : ActionFilterAttribute
  {
    protected static readonly string Key = typeof(ModelStateTempDataTransferAttribute).FullName;
  }

  public class ExportModelStateToTempDataAttribute : ModelStateTempDataTransferAttribute
  {
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      ControllerBase controller = filterContext.Controller as ControllerBase;

      if (controller != null && !controller.ViewData.ModelState.IsValid)
      {
        if ((filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult))
        {
          IEnumerable<ModelStateWrapper> modelStateWrappers = controller.ViewData.ModelState.Select(ms => new ModelStateWrapper() { Key = ms.Key, ValidationState = ms.Value.ValidationState, Value = ms.Value.AttemptedValue  });

          controller.TempData[Key] = JsonConvert.SerializeObject(modelStateWrappers, Formatting.None, new JsonSerializerSettings() { ContractResolver = new NoCultureInfoResolver() });
        }
      }

      base.OnActionExecuted(filterContext);
    }
  }

  public class ImportModelStateFromTempDataAttribute : ModelStateTempDataTransferAttribute
  {
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      ControllerBase controller = filterContext.Controller as ControllerBase;

      if (controller != null)
      {
        string serializedModelState = controller.TempData[Key] as string;

        if (!string.IsNullOrEmpty(serializedModelState))
        {
          IEnumerable<ModelStateWrapper> modelStateWrappers = JsonConvert.DeserializeObject<IEnumerable<ModelStateWrapper>>(serializedModelState, new JsonSerializerSettings() { Error = DeserializationErrorHandler });
          
          if (modelStateWrappers != null)
          {
            ModelStateDictionary modelState = new ModelStateDictionary();

            foreach (ModelStateWrapper modelStateWrapper in modelStateWrappers)
            {
              ModelStateEntry ms = new ModelStateEntry();

              ms.ValidationState = modelStateWrapper.ValidationState;
              ms.AttemptedValue = modelStateWrapper.Value;
              modelState.Add(modelStateWrapper.Key, ms);
            }

            if (filterContext.Result is ViewResult)
              controller.ViewData.ModelState.Merge(modelState);

            else controller.TempData.Remove(Key);
          }
        }
      }

      base.OnActionExecuted(filterContext);
    }

    private void DeserializationErrorHandler(object sender, ErrorEventArgs errorArgs)
    {
      errorArgs.ErrorContext.Handled = true;
    }
  }

  public class ModelStateWrapper
  {
    public string Key { get; set; }
    public ModelValidationState ValidationState { get; set; }
    public string Value { get; set; }
  }

  public class NoCultureInfoResolver : DefaultContractResolver
  {
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
      IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

      return properties.Where(p => p.PropertyType != typeof(CultureInfo)).ToList();
    }
  }
}