using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
  public class AddressDTO
  {
    public int ID { get; set; }
    [Required(ErrorMessage = "Address Content is required")]
    public string AddressContent { get; set; }
    [Required(ErrorMessage = "Email Content is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Phone Content is required")]
    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public string Fax { get; set; }
    [Required(ErrorMessage = "Map Content is required")]
    public string LargeMapPath { get; set; }
    [Required(ErrorMessage = "Map Content is required")]
    public string SmallMapPath { get; set; }
  }
}
