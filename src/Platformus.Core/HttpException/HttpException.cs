// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net;

namespace Platformus.Core;

/// <summary>
/// Represents HTTP(S) errors.
/// </summary>
public class HttpException : Exception
{
  private readonly int httpStatusCode;

  /// <summary>
  /// An HTTP(S) status code.
  /// </summary>
  public int StatusCode
  {
    get
    {
      return this.httpStatusCode;
    }
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="HttpException"/> class.
  /// </summary>
  /// <param name="httpStatusCode">An HTTP(S) status code.</param>
  public HttpException(int httpStatusCode)
  {
    this.httpStatusCode = httpStatusCode;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="HttpException"/> class.
  /// </summary>
  /// <param name="httpStatusCode">An HTTP(S) status code.</param>
  public HttpException(HttpStatusCode httpStatusCode)
  {
    this.httpStatusCode = (int)httpStatusCode;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="HttpException"/> class.
  /// </summary>
  /// <param name="httpStatusCode">An HTTP(S) status code.</param>
  /// <param name="message">A message that describes the error.</param>
  public HttpException(int httpStatusCode, string message) : base(message)
  {
    this.httpStatusCode = httpStatusCode;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="HttpException"/> class.
  /// </summary>
  /// <param name="httpStatusCode">An HTTP(S) status code.</param>
  /// <param name="message">A message that describes the error.</param>
  public HttpException(HttpStatusCode httpStatusCode, string message) : base(message)
  {
    this.httpStatusCode = (int)httpStatusCode;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="HttpException"/> class.
  /// </summary>
  /// <param name="httpStatusCode">An HTTP(S) status code.</param>
  /// <param name="message">A message that describes the error.</param>
  /// <param name="innerException">An exception that is the cause of the current one.</param>
  public HttpException(int httpStatusCode, string message, Exception innerException) : base(message, innerException)
  {
    this.httpStatusCode = httpStatusCode;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="HttpException"/> class.
  /// </summary>
  /// <param name="httpStatusCode">An HTTP(S) status code.</param>
  /// <param name="message">A message that describes the error.</param>
  /// <param name="innerException">An exception that is the cause of the current one.</param>
  public HttpException(HttpStatusCode httpStatusCode, string message, Exception innerException) : base(message, innerException)
  {
    this.httpStatusCode = (int)httpStatusCode;
  }
}