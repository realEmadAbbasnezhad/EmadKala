// This file is a part of EmadKala, Licenced under https://www.gnu.org/licenses/gpl-3.0.html
// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)

using Microsoft.AspNetCore.Identity;

namespace EmadKalaWeb.Models;

public class EmadKalaUser : IdentityUser<int>
{
}

public class EmadKalaRole : IdentityRole<int>
{
}