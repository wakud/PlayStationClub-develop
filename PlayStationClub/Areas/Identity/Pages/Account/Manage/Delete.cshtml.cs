using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PlayStationClub.Data;

namespace PlayStationClub.Areas.Identity.Pages.Account.Manage
{
    public class Delete : PageModel
    {
        private readonly UserManager<PlayStationClubUser> _userManager;
        private readonly SignInManager<PlayStationClubUser> _signInManager;
        private readonly ILogger<Delete> _logger;

        public Delete(
            UserManager<PlayStationClubUser> userManager,
            SignInManager<PlayStationClubUser> signInManager,
            ILogger<Delete> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с идентификатором '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с идентификатором '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Неверный пароль.");
                    return Page();
                }
            }

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Произошла непредвиденная ошибка при удалении пользователя с идентификатором '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("Пользователь с идентификатором '{UserId}' удалил себя.", userId);

            return Redirect("~/");
        }
    }
}
