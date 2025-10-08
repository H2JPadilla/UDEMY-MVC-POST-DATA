using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
  public class VideoController : BaseController

  {
    public readonly VideoBLL bll = new VideoBLL();

    public ActionResult VideoList()
    {
      List<VideoDTO> list = new List<VideoDTO>();
      list = bll.GetVideo();

      return View(list);
    }

    public ActionResult AddVideo()
    {
      VideoDTO dto = new VideoDTO();
      return View(dto);
    }

    [HttpPost]
    [ValidateInput(false)]
    public ActionResult AddVideo(VideoDTO model)
    {

      if (ModelState.IsValid)
      {
        string path = model.OriginalVideoPath.Substring(32);
        string mergelink = "https://www.youtube.com/embed/";
        mergelink += path;
        model.VideoPath = String.Format(@"<iframe width = ""300"" height = ""200"" src = ""{0}"" frameborder = ""0""  allowfullscreen ></ iframe >", mergelink);

        if (bll.AddVideo(model))
        {
          ViewBag.ProcessState = General.Messages.AddSuccess;
          ModelState.Clear();
          model = new VideoDTO();
        }
        else
        {
          ViewBag.ProcessState = General.Messages.GeneralError;
        }
      }
      else
      {
        ViewBag.ProcessState = General.Messages.EmptyArea;
      }
      return View(model);
    }

    public ActionResult UpdateVideo(int id)
    {
      VideoDTO dto = bll.GetVideoID(id);
      return View(dto);
    }

    [HttpPost]
    [ValidateInput(false)]
    public ActionResult UpdateVideo(VideoDTO model)
    {
      if (ModelState.IsValid)
      {
        string path = model.OriginalVideoPath.Substring(32);
        string mergelink = "https://www.youtube.com/embed/";
        mergelink += path;
        model.VideoPath = String.Format(@"<iframe width = ""300"" height = ""200"" src = ""{0}"" frameborder = ""0""  allowfullscreen ></ iframe >", mergelink);

        if (bll.UpdateVideo(model))
        {
          ViewBag.ProcessState = General.Messages.UpdateSuccess;
          ModelState.Clear();
          model = new VideoDTO();
        }
        else
        {
          ViewBag.ProcessState = General.Messages.GeneralError;
        }
      }

      else
      {
        ViewBag.ProcessState = General.Messages.EmptyArea;
      }

      return View(model);
    }

    public JsonResult DeleteVideo(int ID)
    {
      bll.DeleteVideo(ID);
      return Json("");
    }
  }
}