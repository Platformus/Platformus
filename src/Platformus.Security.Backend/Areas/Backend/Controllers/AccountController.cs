// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Security.Backend.ViewModels.Account;
using Platformus.Security.Data.Abstractions;

namespace Platformus.Security.Backend.Controllers
{
  [Area("Backend")]
  public class AccountController : Barebone.Backend.Controllers.ControllerBase
  {
    private IEmailSender emailSender;

    public AccountController(IStorage storage, IEmailSender emailSender)
      : base(storage)
    {
      this.emailSender = emailSender;
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    [AllowAnonymous]
    public IActionResult ResetPassword()
    {
      return this.View();
    }

    [HttpPost]
    [ExportModelStateToTempData]
    [AllowAnonymous]
    public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
    {
      ICredentialRepository credentialRepository = this.Storage.GetRepository<ICredentialRepository>();

      if (this.ModelState.IsValid)
      {
        string newPassword = new Random().Next(10000000, 99999999).ToString();

        UserManager.ChangeSecretResult changeSecretResult = new UserManager(this).ChangeSecret("Email", resetPassword.Email, newPassword);

        if (changeSecretResult.Success)
        {
          this.emailSender.SendEmail(resetPassword.Email, "New password", newPassword, null);
          return this.RedirectToAction("PasswordSent");
        }

        else this.ModelState.AddModelError(nameof(resetPassword.Email), string.Empty);
      }

      return this.CreateRedirectToSelfResult();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult PasswordSent()
    {
      return this.View();
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    [AllowAnonymous]
    public IActionResult SignIn()
    {
      return this.View();
    }

    [HttpPost]
    [ExportModelStateToTempData]
    [AllowAnonymous]
    public IActionResult SignIn(SignInViewModel signIn)
    {
      if (this.ModelState.IsValid)
      {
        UserManager userManager = new UserManager(this);
        UserManager.ValidateResult validateResult = userManager.Validate("Email", signIn.Email, signIn.Password);

        if (validateResult.Success)
        {
          userManager.SignIn(validateResult.User, BackendCookieAuthenticationDefaults.AuthenticationScheme, signIn.RememberMe);

          if (!string.IsNullOrEmpty(signIn.Next))
            return this.Redirect(signIn.Next);

          return this.RedirectToAction("Index", "Default");
        }

        else this.ModelState.AddModelError(nameof(signIn.Email), string.Empty);
      }

      return this.CreateRedirectToSelfResult();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult SignOut()
    {
      new UserManager(this).SignOut(BackendCookieAuthenticationDefaults.AuthenticationScheme);
      return this.RedirectToAction("SignIn");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
      return this.View();
    }
  }
}