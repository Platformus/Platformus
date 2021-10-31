// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platformus.Core.Services.Abstractions
{
  /// <summary>
  /// Describes an email sending result.
  /// </summary>
  public class SendEmailResult
  {
    /// <summary>
    /// Indicates if an email sending was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// An email sending error details.
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SendEmailResult"/> class.
    /// </summary>
    /// <param name="success">Indicates if an email sending was successful.</param>
    /// <param name="errorMessage">An email sending error details.</param>
    public SendEmailResult(bool success, string errorMessage = null)
    {
      this.Success = success;
      this.ErrorMessage = errorMessage;
    }
  }

  /// <summary>
  /// Describes a service for sending emails.
  /// </summary>
  public interface IEmailSender
  {
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="to">An email recipient's email address.</param>
    /// <param name="subject">An email subject.</param>
    /// <param name="body">An email body (HTML).</param>
    /// <param name="attachmentsByFilenames">An email attachments (by filenames).</param>
    Task<SendEmailResult> SendEmailAsync(string to, string subject, string body, IDictionary<string, byte[]> attachmentsByFilenames);
  }
}