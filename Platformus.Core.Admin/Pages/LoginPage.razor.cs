using System.ComponentModel.DataAnnotations;

namespace Platformus.Core.Admin.Pages;

public class LoginPageModel
{
  [Display(Name = "Email")]
  [Required]
  public string? Email { get; set; }

  [Display(Name = "Password")]
  [Required]
  public string? Password { get; set; }
}