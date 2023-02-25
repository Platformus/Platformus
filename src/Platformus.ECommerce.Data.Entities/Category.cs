// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;
using Platformus.Core.Data.Entities;

namespace Platformus.ECommerce.Data.Entities;

/// <summary>
/// Represents a category.
/// </summary>
public class Category : IEntity<int>
{
  /// <summary>
  /// Gets or sets the unique identifier of the category.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Gets or sets the category identifier this category belongs to (as a parent category).
  /// </summary>
  public int? CategoryId { get; set; }

  /// <summary>
  /// Gets or sets the unique URL of the category.
  /// </summary>
  public string Url { get; set; }

  /// <summary>
  /// Gets or sets the dictionary identifier this category is related to. It is used to store the localizable category name.
  /// </summary>
  public int NameId { get; set; }

  /// <summary>
  /// Gets or sets the dictionary identifier this category is related to. It is used to store the localizable category description.
  /// </summary>
  public int DescriptionId { get; set; }

  /// <summary>
  /// Gets or sets the category position. Position is used to sort the categories inside the parent category (smallest to largest).
  /// </summary>
  public int? Position { get; set; }

  /// <summary>
  /// Gets or sets the dictionary identifier this category is related to. It is used to store the localizable category title.
  /// </summary>
  public int TitleId { get; set; }

  /// <summary>
  /// Gets or sets the dictionary identifier this category is related to. It is used to store the localizable category META description.
  /// </summary>
  public int MetaDescriptionId { get; set; }

  /// <summary>
  /// Gets or sets the dictionary identifier this category is related to. It is used to store the localizable category META keywords.
  /// </summary>
  public int MetaKeywordsId { get; set; }

  /// <summary>
  /// Gets or sets the name (including namespace) of the product provider C# class which will be instantiated each time
  /// when the category content is requested by a user.
  /// </summary>
  public string ProductProviderCSharpClassName { get; set; }

  /// <summary>
  /// Gets or sets the parameters (key=value pairs separated by commas) for the product provider C# class instances.
  /// </summary>
  public string ProductProviderParameters { get; set; }

  public virtual Category Owner { get; set; }
  public virtual Dictionary Name { get; set; }
  public virtual Dictionary Description { get; set; }
  public virtual Dictionary Title { get; set; }
  public virtual Dictionary MetaDescription { get; set; }
  public virtual Dictionary MetaKeywords { get; set; }
  public virtual ICollection<Category> Categories { get; set; }
}