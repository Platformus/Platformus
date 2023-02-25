// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platformus.Core.Services.Abstractions;

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