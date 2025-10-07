using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public class VideoBLL
  {
    VideoDAO dao = new VideoDAO();

    public bool AddVideo(VideoDTO model)
    {
      TBL_VIDEO video = new TBL_VIDEO();
      video.Title = model.Title;
      video.VideoPath = model.VideoPath;
      video.OriginalVideoPath = model.OriginalVideoPath;
      video.AddDate = DateTime.Now;
      video.LastUpdateDate = DateTime.Now;
      video.LastUpdateUserID = UserStatic.UserID;
      int ID = dao.AddVideo(video);
      LogDAO.AddLog(General.ProcessType.VideoAdd, General.TableName.Video, model.ID);
      return true;
    }

    public void DeleteVideo(int iD)
    {
      dao.DeleteVideo(iD);
      LogDAO.AddLog(General.ProcessType.VideoDelete, General.TableName.Video, iD);

    }

    public List<VideoDTO> GetVideo()
    {
      return dao.GetVideo();

    }

    public VideoDTO GetVideoID(int id)
    {
      return dao.GetVideoByID(id);
    }

    public bool UpdateVideo(VideoDTO model)
    {
      dao.UpdateVideo(model);
      LogDAO.AddLog(General.ProcessType.VideoUpdate, General.TableName.Video, model.ID);
      return true;
    }
  }
}
