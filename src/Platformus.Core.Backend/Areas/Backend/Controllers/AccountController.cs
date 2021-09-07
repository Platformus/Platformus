// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.ViewModels.Account;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Backend.Controllers
{
  [Area("Backend")]
  public class AccountController : ControllerBase
  {
    private IEmailSender emailSender;
    private IUserManager userManager;

    public AccountController(IStorage storage, IEmailSender emailSender, IUserManager userManager)
      : base(storage)
    {
      this.emailSender = emailSender;
      this.userManager = userManager;
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
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordViewModel resetPassword)
    {
      if (this.ModelState.IsValid)
      {
        string newPassword = new Random().Next(10000000, 99999999 + 1).ToString();

        ChangeSecretResult changeSecretResult = await this.userManager.ChangeSecretAsync("Email", resetPassword.Email, newPassword);

        if (changeSecretResult.Success)
        {
          await this.emailSender.SendEmailAsync(resetPassword.Email, "New password", newPassword, null);
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
    public async Task<IActionResult> SignInAsync(SignInViewModel signIn)
    {
      if (this.ModelState.IsValid)
      {
        ValidateResult validateResult = await this.userManager.ValidateAsync("Email", signIn.Email, signIn.Password);

        if (validateResult.Success)
        {
          await this.userManager.SignInAsync(validateResult.User, BackendCookieAuthenticationDefaults.AuthenticationScheme, signIn.RememberMe);

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
    public async Task<IActionResult> SignOutAsync()
    {
      await this.userManager.SignOutAsync(BackendCookieAuthenticationDefaults.AuthenticationScheme);
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