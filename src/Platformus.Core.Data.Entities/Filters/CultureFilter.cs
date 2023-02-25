// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters;

public class CultureFilter : IFilter
{
  public string Id { get; set; }
  public StringFilter Name { get; set; }
  public bool? IsNeutral { get; set; }
  public bool? IsFrontendDefault { get; set; }
  public bool? IsBackendDefault { get; set; }

  public CultureFilter() { }

  public CultureFilter(string id = null, StringFilter name = null, bool? isNeutral = null, bool? isFrontendDefault = null, bool? isBackendDefault = null)
  {
    Id = id;
    Name = name;
    IsNeutral = isNeutral;
    IsFrontendDefault = isFrontendDefault;
    IsBackendDefault = isBackendDefault;
  }
}