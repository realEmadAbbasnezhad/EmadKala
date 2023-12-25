// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)
// This file is a part of EmadKala, Licenced under http://www.gnu.org/licenses/

using Microsoft.AspNetCore.Identity;

namespace EmadKalaWeb.Models;

public class EmadKalaUser : IdentityUser<int>
{
}

public class EmadKalaRole : IdentityRole<int>
{
}