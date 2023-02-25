// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Images;

public class Size
{
  public int Width { get; set; }
  public int Height { get; set; }

  public Size()
  {
  }

  public bool IsEmpty()
  {
    return this.Width == 0 && this.Height == 0;
  }
}