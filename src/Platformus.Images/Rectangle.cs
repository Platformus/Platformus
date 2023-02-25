// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Images;

public class Rectangle
{
  public int X { get; set; }
  public int Y { get; set; }
  public int Width { get; set; }
  public int Height { get; set; }

  public Rectangle()
  {
  }

  public bool IsEmpty()
  {
    return this.X == 0 && this.Y == 0 && this.Width == 0 && this.Height == 0;
  }
}