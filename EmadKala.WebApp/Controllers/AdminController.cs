using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EmadKala.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable CS8618
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace EmadKala.WebApp.Controllers;

[Route("Admin"), AllowAnonymous]
public class AdminController(
    EmadKalaStoreDbContext storeDbContext) : Controller
{
    [HttpGet("media"), AllowAnonymous]
    public async Task<ActionResult> Media(int id)
    {
        var retVal0 = await EmadKalaFileDbContext.ReadUserFileAsync(id);
        if (retVal0 == null) return NotFound();
        return File(retVal0, "image/jpg", $"real-emad-abbasnezhad.ir_{id}.jpg");
    }
    
    #region SubCategory

    public class SubmitSubCategoryViewModel
    {
        [DataType(DataType.Text)] public string? ReturnUrl { set; get; }

        [Required(ErrorMessage = "فیلد دسته خالی است!"), DisplayName("دسته")]
        [Range(1, 5, ErrorMessage = "فیلد دسته خالی است!")]
        public short Category { get; set; }

        [Required(ErrorMessage = "فیلد نام محصول خالی است!"), DisplayName("نام محصول")]
        [MaxLength(100)]
        public string ProductName { get; set; }
    }

    [HttpGet("submitSubCategory"), Authorize(Roles = "admin")]
    public IActionResult SubmitSubCategory(string? returnUrl)
    {
        return View(new SubmitSubCategoryViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost("submitSubCategory"), Authorize(Roles = "admin")]
    public ActionResult SubmitSubCategory(SubmitSubCategoryViewModel viewModel)
    {
        //==> generate an unique id
        short newId;
        while (true)
        {
            newId = (short)Random.Shared.Next(0, short.MaxValue);
            if (!storeDbContext.SubCategories.Any(x => x.Id == newId)) break;
        }

        //==> save the new boulevard to database
        storeDbContext.SubCategories.Add(new CategoryModel
        {
            Id = newId,
            Name = viewModel.ProductName,
            Parent = viewModel.Category
        });
        storeDbContext.SaveChanges();

        //==> redirect to return url
        if (Url.IsLocalUrl(viewModel.ReturnUrl)) return LocalRedirect(viewModel.ReturnUrl);
        return RedirectToAction("Index", "Home");
    }


    //===========================
    //            API
    
    [HttpGet("subcategories"), AllowAnonymous]
    public ActionResult SubCategories(short category)
    {
        List<string> names = [];
        List<int> ids = [];
        storeDbContext.SubCategories.Where(x => x.Parent == category)
            .ToList().ForEach(x =>
            {
                names.Add(x.Name);
                ids.Add(x.Id);
            });
        return Json(new { names, ids });
    }

    #endregion
}