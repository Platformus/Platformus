// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Parameters;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.FormHandlers
{
  /// <summary>
  /// Describes a form handler. Users specify the form handlers for the forms.
  /// A form handler takes the information entered into a form by a user and handles it in some way
  /// (sends somewhere, creates a database record etc.).
  /// </summary>
  public interface IFormHandler
  {
    /// <summary>
    /// Gets the parameter groups with the parameters the form handler needs from the users.
    /// </summary>
    IEnumerable<ParameterGroup> ParameterGroups { get; }

    /// <summary>
    /// Gets description that is shown to a user to describe the form handler.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Handles the form.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the required services from.</param>
    /// <param name="origin">An URL of the page where the form is located.</param>
    /// <param name="form">A form that is posted.</param>
    /// <param name="valuesByFields">The values entered by a user by the form fields.</param>
    /// <param name="attachmentsByFilenames">The attached files.</param>
    Task<IActionResult> HandleAsync(HttpContext httpContext, string origin, Form form, IDictionary<Field, string> valuesByFields, IDictionary<string, byte[]> attachmentsByFilenames);
  }
}