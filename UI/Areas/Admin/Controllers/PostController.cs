using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.WebPages.Instrumentation;

namespace UI.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
    public readonly PostBLL bll = new PostBLL();
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult PostList()
    {
      List<PostDTO> postlist = new List<PostDTO>();
      postlist = bll.GetPosts();
      return View(postlist);
    }
    public ActionResult AddPost()
    {
      PostDTO model = new PostDTO();
      model.Categories = CategoryBLL.GetCategoryForDropDown();
      return View(model); 
    }

    [HttpPost]
    [ValidateInput(false)]
    public ActionResult AddPost(PostDTO model)
    {
      if (model.PostImage[0]==null)
      {
        ViewBag.ProcessState = General.Messages.ImageMissing; 
      }

      else if (ModelState.IsValid)
      {
        foreach (var item in model.PostImage)
        {
          Bitmap image = new Bitmap(item.InputStream);
          string ext = Path.GetExtension(item.FileName);
          if (ext!=".png" && ext != ".jpg" && ext != ".jpeg")
          {
            ViewBag.ProcessState = General.Messages.ImageMissing;
            model.Categories = CategoryBLL.GetCategoryForDropDown();
            return View(model);
          }
        }

        List<PostImageDTO>imagelist = new List<PostImageDTO>();
        foreach (var postedfile in model.PostImage)
        {
          Bitmap image = new Bitmap(postedfile.InputStream);
          Bitmap resizeimage = new Bitmap(image, 750, 422);
          string filename = "";
          string uniquenumber = Guid.NewGuid().ToString();
          filename = uniquenumber + postedfile.FileName;
          resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImages/" + filename));
          PostImageDTO dto = new PostImageDTO();
          dto.ImagePath = filename;
          imagelist.Add(dto);
        }
        model.PostImages = imagelist;
        if (bll.AddPost(model))
        {
          ViewBag.ProcessState = General.Messages.AddSuccess;
          ModelState.Clear();
          model = new PostDTO();
        }
      }
      else
      {
        ViewBag.ProcessState = General.Messages.EmptyArea;
      }

      model.Categories = CategoryBLL.GetCategoryForDropDown();
      return View(model); 
    }

    public ActionResult UpdatePost(int ID)
    {
      PostDTO model = new PostDTO();
      model = bll.GetPostByID(ID);
      model.Categories = CategoryBLL.GetCategoryForDropDown();
      model.isUpdate = true;
      return View(model);

    }
    [HttpPost]
    [ValidateInput(false)]
    public ActionResult UpdatePost(PostDTO model)
    {
      IEnumerable<SelectListItem> selectlist = CategoryBLL.GetCategoryForDropDown();
      if (ModelState.IsValid)
      {
        if (model.PostImage[0] != null)
        {
          foreach (var item in model.PostImage)
          {
            Bitmap image = new Bitmap(item.InputStream);
            string ext = Path.GetExtension(item.FileName);
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg")
            {
              ViewBag.ProcessState = General.Messages.ImageMissing;
              model.Categories = CategoryBLL.GetCategoryForDropDown();
              return View(model);
            }
          }

          List<PostImageDTO> imagelist = new List<PostImageDTO>();
          foreach (var postedfile in model.PostImage)
          {
            Bitmap image = new Bitmap(postedfile.InputStream);
            Bitmap resizeimage = new Bitmap(image, 750, 422);
            string filename = "";
            string uniquenumber = Guid.NewGuid().ToString();
            filename = uniquenumber + postedfile.FileName;
            resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImages/" + filename));
            PostImageDTO dto = new PostImageDTO();
            dto.ImagePath = filename;
            imagelist.Add(dto);
          }
          model.PostImages = imagelist;
        }

        if (bll.UpdatePost(model))
        {
          ViewBag.ProcessState = General.Messages.UpdateSuccess;

        }
        else
        {
          ViewBag.ProcessState = General.Messages.EmptyArea;
        }
      }
      else
      {
        ViewBag.ProcessState = General.Messages.GeneralError;
      }
      model = bll.GetPostByID(model.ID);
      model.Categories = selectlist;
      model.isUpdate = true;
      return View(model);

    }
  }

}