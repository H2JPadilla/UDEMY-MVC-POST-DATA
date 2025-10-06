using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class VideoDAO : PostContext
  {
    public int AddVideo(TBL_VIDEO video)
    {
      try
      {
        db.TBL_VIDEO.Add(video);
        db.SaveChanges();
        return video.ID;
      }
      catch (Exception ex)
      {
        throw ex;
      }

    }

    public List<VideoDTO> GetVideo()
    {
      List<TBL_VIDEO> list = db.TBL_VIDEO.Where(x => x.isDeleted == false || x.isDeleted == null).OrderByDescending(x => x.AddDate).ToList();
      List<VideoDTO> videolist = new List<VideoDTO>();

      foreach (var item in list)
      {
        VideoDTO dto = new VideoDTO();
        dto.Title = item.Title;
        dto.VideoPath = item.VideoPath;
        dto.OriginalVideoPath = item.OriginalVideoPath;
        dto.ID = item.ID;
        dto.AddDate = item.AddDate;
        videolist.Add(dto);
      }
      return videolist;
    }

    public VideoDTO GetVideoByID(int id)
    {
      TBL_VIDEO video = db.TBL_VIDEO.First(x => x.ID == id);
      VideoDTO dto = new VideoDTO();
      dto.ID = video.ID;
      dto.Title = video.Title;
      dto.OriginalVideoPath = video.OriginalVideoPath;
      return dto;
    }

    public void UpdateVideo(VideoDTO model)
    {
      try
      {
        TBL_VIDEO video = db.TBL_VIDEO.First(x => x.ID == model.ID);
        video.Title = model.Title;
        video.VideoPath = model.VideoPath;
        video.OriginalVideoPath = model.OriginalVideoPath;
        video.LastUpdateDate = DateTime.Now;
        video.LastUpdateUserID = UserStatic.UserID;
        video.AddDate = DateTime.Now;
        db.SaveChanges();
      }
      catch (Exception ex)
      {

        throw ex;
      }

    }
  }
}
