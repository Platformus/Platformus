// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Platformus.Core.Backend
{
  public static class ModelExpressionExtensions
  {
    public static string GetLabel(this ModelExpression modelExpression)
    {
      return modelExpression.Metadata.DisplayName;
    }

    public static bool HasRequiredAttribute(this ModelExpression modelExpression)
    {
      return GetRequiredAttribute(modelExpression) != null;
    }

    public static bool HasStringLengthAttribute(this ModelExpression modelExpression)
    {
      return GetStringLengthAttribute(modelExpression) != null;
    }

    public static int? GetMinStringLength(this ModelExpression modelExpression)
    {
      int? minimumLength = GetStringLengthAttribute(modelExpression)?.MinimumLength;

      if (minimumLength == 0)
        minimumLength = null;

      return minimumLength;
    }

    public static int? GetMaxStringLength(this ModelExpression modelExpression)
    {
      int? maximumLength = GetStringLengthAttribute(modelExpression)?.MaximumLength;

      if (maximumLength == 0)
        maximumLength = null;

      return maximumLength;
    }

    private static RequiredAttribute GetRequiredAttribute(ModelExpression modelExpression)
    {
      return modelExpression.Metadata.ValidatorMetadata.FirstOrDefault(vm => vm is RequiredAttribute) as RequiredAttribute;
    }

    private static StringLengthAttribute GetStringLengthAttribute(ModelExpression modelExpression)
    {
      return modelExpression.Metadata.ValidatorMetadata.FirstOrDefault(vm => vm is StringLengthAttribute) as StringLengthAttribute;
    }
  }
}