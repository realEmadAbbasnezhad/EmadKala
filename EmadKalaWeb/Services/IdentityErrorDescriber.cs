// This file is a part of EmadKala, Licenced under https://www.gnu.org/licenses/gpl-3.0.html
// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)

using Microsoft.AspNetCore.Identity;

namespace EmadKalaWeb.Services;

// [Quick Start] Hare you can change error messages related to account management
public class EmadKalaIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError()
    {
        var retVal = base.DefaultError();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError ConcurrencyFailure()
    {
        var retVal = base.ConcurrencyFailure();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError PasswordMismatch()
    {
        var retVal = base.PasswordMismatch();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError InvalidToken()
    {
        var retVal = base.InvalidToken();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError RecoveryCodeRedemptionFailed()
    {
        var retVal = base.RecoveryCodeRedemptionFailed();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError LoginAlreadyAssociated()
    {
        var retVal = base.LoginAlreadyAssociated();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError InvalidUserName(string? userName)
    {
        var retVal = base.InvalidUserName(userName);
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError InvalidEmail(string? email)
    {
        var retVal = base.InvalidEmail(email);
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        var retVal = base.DuplicateUserName(userName);
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError DuplicateEmail(string email)
    {
        var retVal = base.DuplicateEmail(email);
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError InvalidRoleName(string? role)
    {
        var retVal = base.InvalidRoleName(role);
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError DuplicateRoleName(string role)
    {
        var retVal = base.DuplicateRoleName(role);
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError UserAlreadyHasPassword()
    {
        var retVal = base.UserAlreadyHasPassword();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError UserLockoutNotEnabled()
    {
        var retVal = base.UserLockoutNotEnabled();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError UserAlreadyInRole(string role)
    {
        var retVal = base.UserAlreadyInRole(role);
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError UserNotInRole(string role)
    {
        var retVal = base.UserNotInRole(role);
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError PasswordTooShort(int length)
    {
        var retVal = base.PasswordTooShort(length);
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
    {
        var retVal = base.PasswordRequiresUniqueChars(uniqueChars);
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        var retVal = base.PasswordRequiresNonAlphanumeric();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError PasswordRequiresDigit()
    {
        var retVal = base.PasswordRequiresDigit();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError PasswordRequiresLower()
    {
        var retVal = base.PasswordRequiresLower();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }

    public override IdentityError PasswordRequiresUpper()
    {
        var retVal = base.PasswordRequiresUpper();
        retVal.Description = "NOT IMPLANTED!";
        return retVal;
    }
}