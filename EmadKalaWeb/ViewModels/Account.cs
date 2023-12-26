// This file is a part of EmadKala, Licenced under https://www.gnu.org/licenses/gpl-3.0.html
// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmadKalaWeb.ViewModels;
#pragma warning disable CS8618
// [Quick Start] model views are a good place to start localization.

public class SignupViewModel
{
    [DisplayName("شماره همراه"), DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "شماره همراه وارد نشده")
     , RegularExpression(@"^(?:0)?(9\d{9})$", ErrorMessage = "شماره همراه معتبر نیست")]
    public string PhoneNumber { get; set; }

    [DisplayName("رمز عبور"), DataType(DataType.Password)]
    [Required(ErrorMessage = "رمز عبور وارد نشده")]
    public string Password { get; set; }
}

public class SigninPasswordViewModel
{
    [DisplayName("شماره همراه"), DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "شماره همراه وارد نشده")
     , RegularExpression(@"^(?:0)?(9\d{9})$", ErrorMessage = "شماره همراه معتبر نیست")]
    public string PhoneNumber { get; set; }

    [DisplayName("رمز عبور"), DataType(DataType.Password)]
    [Required(ErrorMessage = "رمز عبور وارد نشده")]
    public string Password { get; set; }
}

public class PhoneNumberSigninViewModel
{
    [DisplayName("شماره همراه"), DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "شماره همراه وارد نشده")
     , RegularExpression(@"^(?:0)?(9\d{9})$", ErrorMessage = "شماره همراه معتبر نیست")]
    public string PhoneNumber { get; set; }
}

public class PhoneNumberConfirmationCodeSigninViewModel
{
    [DisplayName("کد یک بار مصرف"), DataType(DataType.Text)]
    [Required(ErrorMessage = "کد یک بار وارد نشده")]
    public string Code { get; set; }
}

public class PhoneNumberConfirmationViewModel
{
    [DisplayName("شماره همراه"), DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "شماره همراه وارد نشده")
     , RegularExpression(@"^(?:0)?(9\d{9})$", ErrorMessage = "شماره همراه معتبر نیست")]
    public string PhoneNumber { get; set; }
}

public class PhoneNumberConfirmationCodeViewModel
{
    [DisplayName("کد یک بار مصرف"), DataType(DataType.Text)]
    [Required(ErrorMessage = "کد یک بار وارد نشده")]
    public string Code { get; set; }
}


#pragma warning restore CS8618