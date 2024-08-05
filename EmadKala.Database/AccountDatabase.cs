using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmadKala.Database;

public class EmadKalaUser : IdentityUser<int>
{
    #region Otp

    [MaxLength(128)] public string? OtpHash { get; set; }

    public DateTime OtpGenerateLastTimeUtc { get; set; }
    public int OtpGenerateCount { get; set; }

    public DateTime OtpValidateLastTimeUtc { get; set; }
    public int OtpValidateFailCount { get; set; }

    #endregion
}

public class EmadKalaRole : IdentityRole<int>
{
}

public class EmadKalaAccountDbContext(DbContextOptions<EmadKalaAccountDbContext> opt) :
    IdentityDbContext<EmadKalaUser, EmadKalaRole, int>(opt)
{
}