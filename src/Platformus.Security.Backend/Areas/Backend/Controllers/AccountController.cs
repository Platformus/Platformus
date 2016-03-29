// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Platformus.Barebone.Backend.Controllers;
using Platformus.Security.Backend.ViewModels.Account;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Backend.Controllers
{
  [Area("Backend")]
  public class AccountController : ControllerBase
  {
    public AccountController(IStorage storage)
      : base(storage)
    {
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
        User user = userManager.Validate("Email", signIn.Email, signIn.Password);

        if (user != null)
        {
          userManager.SignIn(user);
          return this.RedirectToAction("Index", "Default");
        }
      }

      return this.CreateRedirectToSelfResult();
    }

    [HttpGet]
    public IActionResult SignOut()
    {
      new UserManager(this).SignOut();
      return this.RedirectToAction("SignIn");
    }
  }
}