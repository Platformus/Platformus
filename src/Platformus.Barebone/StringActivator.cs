// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;
using ExtCore.Infrastructure;

namespace Platformus.Barebone
{
  public class StringActivator
  {
    public static T CreateInstance<T>(string typeFullName)
    {
      Type type = StringActivator.GetType(typeFullName);

      if (type == null)
        throw new System.ArgumentException("Type " + typeFullName + " not found");

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
}