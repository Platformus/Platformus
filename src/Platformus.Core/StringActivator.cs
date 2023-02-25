// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;
using ExtCore.Infrastructure;

namespace Platformus;

/// <summary>
/// Creates instances of the types described inside the assemblies discovered and loaded by the ExtCore.
/// </summary>
public class StringActivator
{
  /// <summary>
  /// Creates an instance of the specified type by the full name.
  /// </summary>
  /// <typeparam name="T">A type the created object should be cast to.</typeparam>
  /// <param name="typeFullName">A full type name of an object to create.</param>
  /// <returns></returns>
  public static T CreateInstance<T>(string typeFullName)
  {
    Type type = GetType(typeFullName);

    if (type == null)
      throw new ArgumentException("Type " + typeFullName + " not found");

    return (T)Activator.CreateInstance(type);
  }

  private static Type GetType(string fullName)
  {
    foreach (Assembly assembly in ExtensionManager.Assemblies)
      foreach (Type type in assembly.GetTypes())
        if (type.FullName == fullName)
          return type;

    return null;
  }
}