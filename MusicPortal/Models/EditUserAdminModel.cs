using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class EditUserAdminModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "NameRequiredView")]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resource))]        
        public string? Name { get; set; }

        [Display(Name = "Registration", ResourceType = typeof(Resources.Resource))]
        public bool Register {  get; set; }

        [Display(Name = "StatusAdmin", ResourceType = typeof(Resources.Resource))]
        public bool StatusAdmin { get; set; }

    }
}
