// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
      Barebone.Controllers.ControllerBase controller = filterContext.Controller as Barebone.Controllers.ControllerBase;

      if (controller != null && !controller.ViewData.ModelState.IsValid)
      {
        if ((filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult))
        {
          IEnumerable<ModelStateWrapper> modelStateWrappers = controller.ViewData.ModelState.Select(
            ms => new ModelStateWrapper() {
              Key = ms.Key,
              Value = ms.Value.AttemptedValue,
              ValidationState = ms.Value.ValidationState
              //Errors = ms.Value.Errors.Select(e => e.ErrorMessage)
            }
          );

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
      Barebone.Controllers.ControllerBase controller = filterContext.Controller as Barebone.Controllers.ControllerBase;

      if (controller != null)
      {
        string serializedModelState = controller.TempData[Key] as string;

        if (!string.IsNullOrEmpty(serializedModelState))
        {
          IEnumerable<ModelStateWrapper> modelStateWrappers = JsonConvert.DeserializeObject<IEnumerable<ModelStateWrapper>>(serializedModelState, new JsonSerializerSettings() { Error = DeserializationErrorHandler });
          
          if (modelStateWrappers != null)
          {
            if (filterContext.Result is ViewResult)
            {
              foreach (ModelStateWrapper modelStateWrapper in modelStateWrappers)
              {
                controller.ViewData.ModelState.SetModelValue(modelStateWrapper.Key, modelStateWrapper.Value, modelStateWrapper.Value);
                controller.ViewData.ModelState[modelStateWrapper.Key].ValidationState = modelStateWrapper.ValidationState;

                //if (modelStateWrapper.ValidationState == ModelValidationState.Invalid)
                //  foreach (string error in modelStateWrapper.Errors)
                //    controller.ViewData.ModelState[modelStateWrapper.Key].Errors.Add(new ModelError(error));
              }
            }

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
    public string Value { get; set; }
    public ModelValidationState ValidationState { get; set; }
    //public IEnumerable<string> Errors { get; set; }
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