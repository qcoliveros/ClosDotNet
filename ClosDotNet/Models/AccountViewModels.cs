namespace ClosDotNet.Models
{
    using ClosDotNet.Resources.Resources;
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "LabelLoginId", ResourceType = typeof(LoginResources))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "LabelPassword", ResourceType = typeof(LoginResources))]
        public string Password { get; set; }
    }
}
