// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)
// This file is a part of EmadKala, Licenced under http://www.gnu.org/licenses/

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmadKalaWeb.Models;

public class EmadKalaDbContext : IdentityDbContext<EmadKalaUser, EmadKalaRole, int>
{
    public EmadKalaDbContext(DbContextOptions<EmadKalaDbContext> opt) : base(opt)
    {
    }
}