// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;
using Platformus.Core.Data.Entities;

namespace Platformus.Website.Data.Entities;

/// <summary>
/// Represents a form. The forms are used to render the forms on the frontend.
/// They group the fields using the unique code.
/// </summary>
public class Form : IEntity<int>
{
  /// <summary>
  /// Gets or sets the unique identifier of the form.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Gets or sets the unique code of the form. It is set by the user and might be used for the form retrieval.
  /// </summary>
  public string Code { get; set; }

  /// <summary>
  /// Gets or sets the dictionary identifier this form is related to. It is used to store the localizable form name.
  /// </summary>
  public int NameId { get; set; }

  /// <summary>
  /// Gets or sets the dictionary identifier this form is related to. It is used to store the localizable submit button title.
  /// </summary>
  public int SubmitButtonTitleId { get; set; }

  /// <summary>
  /// Gets or sets the value indicating whether the completed forms should be produced for the form or not.
  /// Completed forms store all user input for further processing.
  /// </summary>
  public bool ProduceCompletedForms { get; set; }

  /// <summary>
  /// Gets or sets the name (including namespace) of the form handler C# class which will be instantiated each time
  /// when the form is filled by the user to process user input.
  /// </summary>
  public string FormHandlerCSharpClassName { get; set; }

  /// <summary>
  /// Gets or sets the parameters (key=value pairs separated by commas) for the form handler C# class instances.
  /// </summary>
  public string FormHandlerParameters { get; set; }

  public virtual Dictionary Name { get; set; }
  public virtual Dictionary SubmitButtonTitle { get; set; }
  public virtual ICollection<Field> Fields { get; set; }
}