using System.ComponentModel.DataAnnotations;
using EmadKala.Database;
using EmadKala.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618
namespace EmadKala.WebApp.Controllers;

// ajax calls from js that request data or want to change it
[Route("Api/Account"), ApiController]
public class AccountApiController(
    UserManager<EmadKalaUser> userManager,
    SignInManager<EmadKalaUser> signInManager,
    RoleManager<EmadKalaRole> roleManager,
    ISmsService smsService,
    EmadKalaAccountDbContext accountDbContext) : ControllerBase
{
    [HttpGet("Login"), AllowAnonymous]
    public IActionResult Login(string returnUrl)
    {
        return RedirectToAction("Portal");
    }

    #region Portal

    public class PortalRequestModel
    {
        [Required, RegularExpression("^(?:0)?(9\\d{9})$")]
        public string PhoneNumber { set; get; }

        public bool? RequestOtp { get; set; }
    }

    public class PortalRespondModel
    {
        public bool? Exist { get; set; }
        public bool? HasPassword { get; set; }
        public bool? OptCooldown { get; set; }
    }

    [HttpGet("Portal"), AllowAnonymous]
    public async Task<PortalRespondModel> Portal([FromQuery] PortalRequestModel request)
    {
        var respond = new PortalRespondModel { Exist = null, HasPassword = null, OptCooldown = null };
        if (User.Identity is { IsAuthenticated: true }) return respond;
        var claimedUser = new EmadKalaUser
        {
            PhoneNumber = request.PhoneNumber,
            UserName = request.PhoneNumber
        };
        
        respond.Exist = await userManager.Users.AnyAsync(x => x.UserName == claimedUser.UserName);

        if (!respond.Exist.Value)
        {
            var result = await userManager.CreateAsync(claimedUser);
            if (!result.Succeeded)
                throw new Exception($"user creation failed: phoneNumber={request.PhoneNumber}");
            await accountDbContext.SaveChangesAsync();
        }

        claimedUser = await accountDbContext.Users.FirstAsync(x => x.UserName == request.PhoneNumber);
        if (!await roleManager.RoleExistsAsync("admin"))
        {
            await roleManager.CreateAsync(new EmadKalaRole { Name = "admin" });
            await accountDbContext.SaveChangesAsync();
        }
        if (claimedUser.PhoneNumber is "09039861338")
            await userManager.AddToRoleAsync(claimedUser, "admin");
        
        respond.HasPassword = claimedUser.PasswordHash != null;

        if (request.RequestOtp.HasValue && request.RequestOtp.Value)
        {
            var codeGenerateResult = await OtpTokenProvider.GenerateOtpAsync(
                userManager, claimedUser, accountDbContext);
            respond.OptCooldown = codeGenerateResult == null;
            if (!respond.OptCooldown.Value)
                smsService.Otp(claimedUser.PhoneNumber!, codeGenerateResult!);
        }

        await accountDbContext.SaveChangesAsync();
        return respond;
    }

    #endregion

    #region SigninOtp

    public class SigninOtpRequestModel
    {
        public enum Respond
        {
            // user is already authenticated
            Authenticated,

            // user is not exist
            UserNotExist,

            // code age is more than 15min
            Expired,

            // code enter was failed more than 5 time
            CoolDown,

            // code is wrong
            WrongCode,

            // a code never requested
            NeverRequested,

            // user signin was successful
            Ok,
        }

        [Required, RegularExpression("^(?:0)?(9\\d{9})$")]
        public string PhoneNumber { set; get; }

        [Required, RegularExpression("^(\\d{6})$")]
        public string Code { set; get; }
    }

    [HttpPost("SigninOtp"), AllowAnonymous]
    public async Task<SigninOtpRequestModel.Respond> SigninOtp(
        [FromQuery] SigninOtpRequestModel requestModel)
    {
        if (User.Identity is { IsAuthenticated: true }) return SigninOtpRequestModel.Respond.Authenticated;
        var claimedUser = new EmadKalaUser
        {
            PhoneNumber = requestModel.PhoneNumber,
            UserName = requestModel.PhoneNumber
        };

        if (!await userManager.Users.AnyAsync(x => x.UserName == claimedUser.UserName))
            return SigninOtpRequestModel.Respond.UserNotExist;
        claimedUser = await accountDbContext.Users.FirstAsync(
            x => x.UserName == requestModel.PhoneNumber);

        var codeGenerateResult = await OtpTokenProvider.ValidateOtpAsync(
            userManager, claimedUser, requestModel.Code, accountDbContext);
        switch (codeGenerateResult)
        {
            case OtpTokenProvider.VerifyResult.Success:
                await signInManager.SignInAsync(claimedUser, false);
                await accountDbContext.SaveChangesAsync();
                return SigninOtpRequestModel.Respond.Ok;

            case OtpTokenProvider.VerifyResult.WrongCode:
                await accountDbContext.SaveChangesAsync();
                return SigninOtpRequestModel.Respond.WrongCode;

            case OtpTokenProvider.VerifyResult.NoRequest:
                return SigninOtpRequestModel.Respond.NeverRequested;

            case OtpTokenProvider.VerifyResult.Expired:
                return SigninOtpRequestModel.Respond.Expired;

            case OtpTokenProvider.VerifyResult.Cooldown:
                return SigninOtpRequestModel.Respond.CoolDown;

            default:
                throw new Exception("Impossible at SigninOpt");
        }
    }

    #endregion
}

// ajax and redirects from frontend necessary to have a functional app.
[Route("Account")]
public class AccountAppController : Controller
{
    #region Portal

    public class PortalViewModel
    {
        public string? ReturnUrl { get; set; }
    }

    [HttpGet("Portal"), AllowAnonymous]
    public ActionResult Portal(string? returnUrl)
    {
        if (User.Identity is not { IsAuthenticated: true })
            return View("Portal", new PortalViewModel { ReturnUrl = returnUrl });

        if (returnUrl == null) return RedirectToAction("Index");
        if (Url.IsLocalUrl(returnUrl)) return LocalRedirect(returnUrl);

        return RedirectToAction("Index", "AccountApp");
    }

    #endregion

    #region Index

    [HttpGet(""), AllowAnonymous]
    public ActionResult Index()
    {
        return View("Index");
    }

    #endregion
}