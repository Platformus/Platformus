// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Platformus.Website.Data.Entities;
using Platformus.Website.FieldValidators;

namespace Platformus.Website.Frontend.FieldValidators
{
  public class VerificationResult
  {
    public bool Success { get; set; }
  }

  public class ReCaptchaFieldValidator : IFieldValidator
  {
    public async Task<bool> ValidateAsync(HttpContext httpContext, Form form, Field field, string value)
    {
      ReCaptchaOptions options = httpContext.RequestServices.GetService<IOptions<ReCaptchaOptions>>()?.Value;

      if (options == null)
        return false;

      string url = $"https://www.google.com/recaptcha/api/siteverify?secret={options.Secret}&response={value}";

      using (HttpClient httpClient = new HttpClient())
      {
        try
        {
          string responseString = await httpClient.GetStringAsync(url);

          return JsonConvert.DeserializeObject<VerificationResult>(responseString).Success;
        }

        catch
        {
          return false;
        }
      }
    }
  }
}