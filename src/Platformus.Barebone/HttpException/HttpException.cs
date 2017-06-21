// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net;

namespace Platformus.Barebone
{
  public class HttpException : Exception
  {
    private readonly int httpStatusCode;

    public int StatusCode
    {
      get
      {
        return this.httpStatusCode;
      }
    }

    public HttpException(int httpStatusCode)
    {
      this.httpStatusCode = httpStatusCode;
    }

    public HttpException(HttpStatusCode httpStatusCode)
    {
      this.httpStatusCode = (int)httpStatusCode;
    }

    public HttpException(int httpStatusCode, string message) : base(message)
    {
      this.httpStatusCode = httpStatusCode;
    }

    public HttpException(HttpStatusCode httpStatusCode, string message) : base(message)
    {
      this.httpStatusCode = (int)httpStatusCode;
    }

    public HttpException(int httpStatusCode, string message, Exception innerException) : base(message, innerException)
    {
      this.httpStatusCode = httpStatusCode;
    }

    public HttpException(HttpStatusCode httpStatusCode, string message, Exception innerException) : base(message, innerException)
    {
      this.httpStatusCode = (int)httpStatusCode;
    }
  }
}