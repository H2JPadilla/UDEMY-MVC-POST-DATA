using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DTO
{
  public class SocialMediaDTO
  {
    public int SocialMediaID { get; set; }
    [Required(ErrorMessage = "Field is required")]
    public string Name { get; set; }
    public string ImagePath { get; set; }
    [Required(ErrorMessage = "Field is required")]
    public string Link { get; set; }
    [Display(Name = "Image")]
    public HttpPostedFileBase SocialImage { get; set; }
  }
}
