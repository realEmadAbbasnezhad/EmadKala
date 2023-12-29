// This file is a part of EmadKala, Licenced under https://www.gnu.org/licenses/gpl-3.0.html
// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)

using EmadKalaWeb.Models;
using EmadKalaWeb.Services;
using EmadKalaWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmadKalaWeb.Controllers;

/// <summary>
/// sign in, sign up and everything related to users account is handling by this controller.
/// </summary>
[Route("Account")]
public class AccountController(
    ILogger<AccountController> logger,
    IHostEnvironment hostEnvironment,
    UserManager<EmadKalaUser> userManager,
    SignInManager<EmadKalaUser> signInManager,
    IEmadKalaSmsService smsService,
    EmadKalaDbContext dbContext) : Controller
{
    /// <summary>
    /// return signup form in html.
    /// </summary>
    [HttpGet("Signup"), AllowAnonymous]
    public IActionResult Signup(string? returnUrl)
    {
        return View(new SignupViewModel { ReturnUrl = returnUrl });
    }

    /// <summary>
    /// signup a new user.
    /// </summary>
    [HttpPost("Signup"), AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Signup(SignupViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var newUser = new EmadKalaUser { UserName = model.PhoneNumber, PhoneNumber = model.PhoneNumber };
        var result = await userManager.CreateAsync(newUser, model.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);

            return View(model);
        }

        return Ok();
    }

    public IActionResult Signin()
    {
        throw new NotImplementedException();
    }
}