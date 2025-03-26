using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BLL.DTO
{
    public class GenreDTO
    {
        public int Id { get; set;}

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "NameRequiredView")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string? Name { get; set; }
    }
}
