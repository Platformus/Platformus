﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platformus.Core.Services.Abstractions
{
  public class SendEmailResult
  {
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }

    public SendEmailResult(bool success, string errorMessage = null)
    {
      this.Success = success;
      this.ErrorMessage = errorMessage;
    }
  }

  public interface IEmailSender
  {
    Task<SendEmailResult> SendEmailAsync(string to, string subject, string body, IDictionary<string, byte[]> attachmentsByFilenames);
  }
}