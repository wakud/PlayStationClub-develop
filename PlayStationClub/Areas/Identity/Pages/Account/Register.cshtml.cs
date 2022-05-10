using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlayStationClub.Data;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PlayStationClub.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<PlayStationClubUser> _signInManager;
        private readonly UserManager<PlayStationClubUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly PlayStationClubDbContext _context;

        public RegisterModel(
            UserManager<PlayStationClubUser> userManager,
            SignInManager<PlayStationClubUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            PlayStationClubDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [MaxLength(20)]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [MaxLength(30)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            //TODO: добавив пошук телефонів напряму з бази, як зробити через UserManager хйз
            var phoneInBase = _context.Users.ToList();
            var dictionary = new Dictionary<string, string>();
            foreach (var p in phoneInBase)
            {
                dictionary.Add(p.PhoneNumber, p.Id);
            }
            if (dictionary.ContainsKey(Input.PhoneNumber))
            {
                string err = "Такой номер телефона уже зарегистрирован!";
                ModelState.AddModelError(string.Empty, err);
                return Page();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var user = new PlayStationClubUser
                    {
                        UserName = Input.Email,
                        Email = Input.Email,
                        PhoneNumber = Input.PhoneNumber,
                        FullName = Input.FirstName.Trim().ToLower() + " " + Input.LastName.Trim().ToLower()
                    };

                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {

                        _logger.LogInformation("Пользователь создал новую учетную запись с паролем.");
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Подтверждение электронной почты",
                            $"Для завершения регистрации перейдите по ссылке: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>завершить регистрацию</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, "Такая электронная почта зарегистрирована!");
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
