// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Account;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Backend.Controllers
{
  public class AccountController : ControllerBase
  {
    private IUserManager userManager;
    private IEmailSender emailSender;
    private IStringLocalizer localizer;

    public AccountController(IStorage storage, IUserManager userManager, IEmailSender emailSender, IStringLocalizer<AccountController> localizer)
      : base(storage)
    {
      this.userManager = userManager;
      this.emailSender = emailSender;
      this.localizer = localizer;
    }

    [HttpGet]
    [AllowAnonymous]
    [ImportModelStateFromTempData]
    public IActionResult ResetPassword()
    {
      return this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ExportModelStateToTempData]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordViewModel resetPassword)
    {
      if (this.ModelState.IsValid)
      {
        string newPassword = Guid.NewGuid().ToString().Replace("-", string.Empty);

        ChangeSecretResult changeSecretResult = await this.userManager.ChangeSecretAsync("Email", resetPassword.Email, newPassword);

        if (changeSecretResult.Success)
        {
          await this.emailSender.SendEmailAsync(resetPassword.Email, this.localizer["New password"], newPassword, null);
          return this.RedirectToAction("PasswordSent");
        }

        else this.ModelState.AddModelError(nameof(resetPassword.Email), this.localizer["Invalid email"]);
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
    [AllowAnonymous]
    [ImportModelStateFromTempData]
    public IActionResult SignIn()
    {
      return this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ExportModelStateToTempData]
    public async Task<IActionResult> SignInAsync(SignInViewModel signIn)
    {
      if (this.ModelState.IsValid)
      {
        ValidateResult validateResult = await this.userManager.ValidateAsync("Email", signIn.Email, signIn.Password);

        if (validateResult.Success)
        {
          await this.userManager.SignInAsync(validateResult.User, BackendCookieAuthenticationDefaults.AuthenticationScheme, signIn.RememberMe);

          if (!string.IsNullOrEmpty(signIn.TargetUrl))
            return this.Redirect(signIn.TargetUrl);

          return this.RedirectToAction("Index", "Default");
        }

        else this.ModelState.AddModelError(nameof(signIn.Password), this.localizer["Invalid email or password"]);
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