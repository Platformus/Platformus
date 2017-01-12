// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization.Data.Abstractions
{
  public interface ICultureRepository : IRepository
  {
    Culture WithKey(int id);
    Culture WithCode(string code);
    Culture Neutral();
    Culture Default();
    IEnumerable<Culture> All();
    IEnumerable<Culture> NotNeutral();
    IEnumerable<Culture> Range(string orderBy, string direction, int skip, int take, string filter);
    void Create(Culture culture);
    void Edit(Culture culture);
    void Delete(int id);
    void Delete(Culture culture);
    int Count(string filter);
  }
}