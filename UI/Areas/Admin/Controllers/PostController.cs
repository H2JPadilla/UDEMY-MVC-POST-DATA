using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
    public readonly PostBLL bll = new PostBLL();
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult AddPost()
    {
      PostDTO model = new PostDTO();
      model.Categories = CategoryBLL.GetCategoryForDropDown();
      return View(model); 
    }
  }

}