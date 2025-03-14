// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq.Expressions;

namespace Platformus.Core.Admin.ApiServices;

public static class ExpressionExtensions
{
  public static string GetPropertyPath(this Expression? property)
  {
    IList<string> propertyNames = [];

    if (property is LambdaExpression lambdaExpression)
      property = lambdaExpression.Body;

    while (property is MemberExpression expression)
    {
      propertyNames.Insert(0, expression.Member.Name);
      property = expression.Expression;
    }

    return string.Join(".", propertyNames);
  }
}

public abstract class PropertyPathBuilderBase<TObject, TResult>
  where TObject : class
  where TResult : class
{
  protected readonly Func<string, TResult> resultFactory;
  protected readonly IList<Expression> propertyPath;

  public PropertyPathBuilderBase(Func<string, TResult> resultFactory, IList<Expression>? propertyPath = null)
  {
    this.resultFactory = resultFactory;
    this.propertyPath = propertyPath ?? [];
  }

  public TResult Build()
  {
    return this.resultFactory(string.Join(".", this.propertyPath.Select(e => e.GetPropertyPath())));
  }
}

public class PropertyPathBuilder<TObject, TResult> : PropertyPathBuilderBase<TObject, TResult>
  where TObject : class
  where TResult : class
{
  public PropertyPathBuilder(Func<string, TResult> resultFactory) : base(resultFactory)
  {
  }

  public virtual NestedPropertyPathBuilder<TObject, TProperty, TResult> Add<TProperty>(Expression<Func<TObject, TProperty?>> property)
    where TProperty : class
  {
    this.propertyPath.Add(property.Body);
    return new NestedPropertyPathBuilder<TObject, TProperty, TResult>(this.resultFactory, this.propertyPath);
  }

  public NestedPropertyPathBuilder<TObject, TProperty, TResult> Add<TProperty>(Expression<Func<TObject, IList<TProperty>?>> property)
    where TProperty : class
  {
    this.propertyPath.Add(property.Body);
    return new NestedPropertyPathBuilder<TObject, TProperty, TResult>(this.resultFactory, this.propertyPath);
  }

  public NestedPropertyPathBuilder<TObject, TProperty, TResult> Add<TProperty>(Expression<Func<TObject, ICollection<TProperty>?>> property)
    where TProperty : class
  {
    this.propertyPath.Add(property.Body);
    return new NestedPropertyPathBuilder<TObject, TProperty, TResult>(this.resultFactory, this.propertyPath);
  }

  public virtual NestedPropertyPathBuilder<TObject, TProperty, TResult> Add<TProperty>(Expression<Func<TObject, IEnumerable<TProperty>?>> property)
    where TProperty : class
  {
    this.propertyPath.Add(property.Body);
    return new NestedPropertyPathBuilder<TObject, TProperty, TResult>(this.resultFactory, this.propertyPath);
  }
}

public class NestedPropertyPathBuilder<TOridinalTObject, TObject, TResult> : PropertyPathBuilderBase<TOridinalTObject, TResult>
  where TOridinalTObject : class
  where TObject : class
  where TResult : class
{
  public NestedPropertyPathBuilder(Func<string, TResult> resultFactory, IList<Expression> propertyPath) : base(resultFactory, propertyPath)
  {
  }

  public virtual NestedPropertyPathBuilder<TOridinalTObject, TProperty, TResult> ThenAdd<TProperty>(Expression<Func<TObject, TProperty?>> property)
    where TProperty : class
  {
    this.propertyPath.Add(property.Body);
    return new NestedPropertyPathBuilder<TOridinalTObject, TProperty, TResult>(this.resultFactory, this.propertyPath);
  }

  public NestedPropertyPathBuilder<TOridinalTObject, TProperty, TResult> ThenAdd<TProperty>(Expression<Func<TObject, IList<TProperty>?>> property)
    where TProperty : class
  {
    this.propertyPath.Add(property.Body);
    return new NestedPropertyPathBuilder<TOridinalTObject, TProperty, TResult>(this.resultFactory, this.propertyPath);
  }

  public NestedPropertyPathBuilder<TOridinalTObject, TProperty, TResult> ThenAdd<TProperty>(Expression<Func<TObject, ICollection<TProperty>?>> property)
    where TProperty : class
  {
    this.propertyPath.Add(property.Body);
    return new NestedPropertyPathBuilder<TOridinalTObject, TProperty, TResult>(this.resultFactory, this.propertyPath);
  }

  public virtual NestedPropertyPathBuilder<TOridinalTObject, TProperty, TResult> ThenAdd<TProperty>(Expression<Func<TObject, IEnumerable<TProperty>?>> property)
    where TProperty : class
  {
    this.propertyPath.Add(property.Body);
    return new NestedPropertyPathBuilder<TOridinalTObject, TProperty, TResult>(this.resultFactory, this.propertyPath);
  }
}