// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.FieldValidators
{
  /// <summary>
  /// Describes a field validator. Field validators optionally validate values entered into the forms by the users.
  /// It can be used to implement some external validation like captcha etc.
  /// A field validator can be specified for a field type.
  /// </summary>
  public interface IFieldValidator
  {
    /// <summary>
    /// Validates a value entered by a user into a form field.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the required services from.</param>
    /// <param name="form">A form specified <paramref name="field"/> belongs to.</param>
    /// <param name="field">A field <paramref name="value"/> entered into.</param>
    /// <param name="value">A value entered by a user.</param>
    Task<bool> ValidateAsync(HttpContext httpContext, Form form, Field field, string value);
  }
}