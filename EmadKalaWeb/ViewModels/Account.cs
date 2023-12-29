// This file is a part of EmadKala, Licenced under https://www.gnu.org/licenses/gpl-3.0.html
// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EmadKalaWeb.ViewModels;
#pragma warning disable CS8618
// [Quick Start] model views are a good place to start localization.

public class SignupViewModel
{
    [HiddenInput, DataType(DataType.Url)]
    public string? ReturnUrl { get; set; }

    [DisplayName("شماره همراه"), DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "شماره همراه وارد نشده")
     , RegularExpression(@"^(?:0)?(9\d{9})$", ErrorMessage = "شماره همراه معتبر نیست")]
    public string PhoneNumber { get; set; }

    [DisplayName("رمز عبور"), DataType(DataType.Password)]
    [Required(ErrorMessage = "رمز عبور وارد نشده")]
    public string Password { get; set; }
}

#pragma warning restore CS8618