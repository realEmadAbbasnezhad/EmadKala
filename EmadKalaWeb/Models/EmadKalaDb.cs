// This file is a part of EmadKala, Licenced under https://www.gnu.org/licenses/gpl-3.0.html
// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmadKalaWeb.Models;

public class EmadKalaDbContext(DbContextOptions<EmadKalaDbContext> opt)
    : IdentityDbContext<EmadKalaUser, EmadKalaRole, int>(opt)
{
}