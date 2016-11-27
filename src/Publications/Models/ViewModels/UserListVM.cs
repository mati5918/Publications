using System.ComponentModel.DataAnnotations;

namespace Publications.Models.ViewModels
{
    public class UserListVM
    {
        [Display(Name = "Adres email")]
        public string Email { get; set; }
        [Display(Name = "Jest administratorem?")]
        public bool isAdmin { get; set; }
        public string Id { get; set; }
    }
}
