// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)
// This file is a part of EmadKala, Licenced under http://www.gnu.org/licenses/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmadKalaWeb.ViewModels;

#pragma warning disable CS8618
//TODO: Hare you can change texts shows in the form
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
#pragma warning restore CS8618