using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
  public class AdsController : BaseController
  {
    public readonly AdsBLL bll = new AdsBLL();

    public ActionResult AdsList()
    {
      List<AdsDTO> ads = new List<AdsDTO>();
      ads = bll.GetAds();
      return View(ads);
    }
    public ActionResult AddAds()
    {
      AdsDTO dto = new AdsDTO();
      return View(dto);
    }

    [HttpPost]
    public ActionResult AddAds(AdsDTO model)
    {
      if (model.AdsImage == null)
      {
        ViewBag.ProcessState = General.Messages.ImageMissing;
      }
      else if (ModelState.IsValid)
      {
        HttpPostedFileBase postedfile = model.AdsImage;
        Bitmap Ads = new Bitmap(postedfile.InputStream);
        string ext = Path.GetExtension(postedfile.FileName);
        string filename = "";

        if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
        {
          string uniquenumber = Guid.NewGuid().ToString();
          filename = uniquenumber + postedfile.FileName;
          Ads.Save(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + filename));
          model.ImagePath = filename;
        }

        if (bll.AddAds(model))
        {
          ViewBag.ProcessState = General.Messages.AddSuccess;
          model = new AdsDTO();
          ModelState.Clear();
        }
        else
        {
          ViewBag.ProcessState = General.Messages.GeneralError;
        }
      }
      else
      {
        ViewBag.ProcessState = General.Messages.ExtensionError;
      }

      return View(model);
    }

    public ActionResult UpdateAds(int id)
    {
      AdsDTO dto = bll.GetAdsByID(id);
      return View(dto);
    }

    [HttpPost]
    public ActionResult UpdateAds(AdsDTO model)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.ProcessState = General.Messages.EmptyArea;
      }

      else
      {
        if (model.AdsImage != null)
        {
          HttpPostedFileBase postedfile = model.AdsImage;
          Bitmap ads = new Bitmap(postedfile.InputStream);
          string ext = Path.GetExtension(postedfile.FileName);
          string filename = "";

          if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
          {
            string uniquenumber = Guid.NewGuid().ToString();
            filename = uniquenumber + postedfile.FileName;
            ads.Save(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + filename));
            model.ImagePath = filename;
          }
        }
        string oldImagePath = bll.updateAds(model);

        if (model.AdsImage != null)
        {
          if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + oldImagePath)))
          {
            System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + oldImagePath));
          }
        }
        ViewBag.ProcessState = General.Messages.UpdateSuccess;
      }

      return View(model);
    }

    public JsonResult DeleteAds(int ID)
    {
      string imagepath = bll.DeleteAds(ID);
      if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + imagepath)))
      {
        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + imagepath));
      }
      return Json("");
    }
  }
}