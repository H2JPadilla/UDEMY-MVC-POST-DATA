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
  public class UserController : BaseController
  {
    public readonly UserBLL bll = new UserBLL();
    public ActionResult UserList()
    {
      List<UserDTO> model = new List<UserDTO>();
      model = bll.GetUser();
      return View(model);

    }
    public ActionResult AddUser()
    {
      UserDTO model = new UserDTO();
      return View(model);
    }

    [HttpPost]
    public ActionResult AddUser(UserDTO model)
    {
      if (model.UserImage == null)
      {
        ViewBag.ProcessState = General.Messages.ImageMissing;
      }
      else if (ModelState.IsValid)
      {
        string filename = "";

        HttpPostedFileBase postedfile = model.UserImage;
        Bitmap SocialMedia = new Bitmap(postedfile.InputStream);
        string ext = Path.GetExtension(postedfile.FileName);

        if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
        {
          string uniquenumber = Guid.NewGuid().ToString();
          filename = uniquenumber + postedfile.FileName;
          SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/UserImages/" + filename));
          model.ImagePath = filename;
          bll.AddUser(model);
          ViewBag.ProcessState = General.Messages.AddSuccess;
          ModelState.Clear();
          model = new UserDTO();
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

    public ActionResult UpdateUser(int id)
    {
      UserDTO dto = new UserDTO();
      dto = bll.GetUserByID(id);

      return View(dto);
    }

    [HttpPost]
    public ActionResult UpdateUser(UserDTO model)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.ProcessState = General.Messages.EmptyArea;
      }

      else
      {
        if (model.UserImage != null)
        {
          HttpPostedFileBase postedfile = model.UserImage;
          Bitmap User = new Bitmap(postedfile.InputStream);
          string ext = Path.GetExtension(postedfile.FileName);
          string filename = "";

          if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
          {
            string uniquenumber = Guid.NewGuid().ToString();
            filename = uniquenumber + postedfile.FileName;
            User.Save(Server.MapPath("~/Areas/Admin/Content/UserImages/" + filename));
            model.ImagePath = filename;
          }
        }
        string oldImagePath = bll.updateUser(model);

        if (model.UserImage != null)
        {
          if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImages/" + oldImagePath)))
          {
            System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImages/" + oldImagePath));
          }
        }
        ViewBag.ProcessState = General.Messages.UpdateSuccess;
      }
      return View(model);
    }

    public JsonResult DeleteUser(int ID)
    {
      string imagepath = bll.DeleteUser(ID);
      if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImages/" + imagepath)))
      {
        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImages/" + imagepath));
      }
      return Json("");
    }
  }
}