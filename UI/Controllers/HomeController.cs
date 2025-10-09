using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
    LayoutBLL bll = new LayoutBLL();
    public ActionResult Index()
    {
      HomeLayoutDTO dto = new HomeLayoutDTO();
      dto = bll.GetLayoutData();
      ViewData["LayoutDTO"] = dto;
      return View();

    }
  }
}