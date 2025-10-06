using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DTO
{
  public class AdsDTO
  {
    public int ID { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    public string ImagePath { get; set; }
    [Required(ErrorMessage = "Link is required")]
    public string Link { get; set; }
    [Required(ErrorMessage = "Image size is required")]
    public string ImageSize { get; set; }
    [Display(Name = "Image")]
    public HttpPostedFileBase AdsImage { get; set; }
  }
}
