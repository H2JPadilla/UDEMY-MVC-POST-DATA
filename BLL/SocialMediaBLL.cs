using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public class SocialMediaBLL
  {
    SocialMediaDAO dao = new SocialMediaDAO();
    public bool AddSocialMedia(SocialMediaDTO model)
    {
      TBL_SOCIAL_MEDIA social = new TBL_SOCIAL_MEDIA();
      social.Name = model.Name;
      social.Link = model.Link;
      social.ImagePath = model.ImagePath;
      social.AddDate = DateTime.Now;
      social.LastUpdateUserID = UserStatic.UserID;
      social.LastUpdateDate = DateTime.Now;
      int ID = dao.AddSocialMedia(social);
      LogDAO.AddLog(General.ProcessType.SocialAdd, General.TableName.Social, ID);
      return true;
    }

    public List<SocialMediaDTO> GetSocialMedia()
    {
      List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
      dtolist = dao.GetSocialMedia();
      return dtolist;
    }

    public SocialMediaDTO getSocMedID(int id)
    {
      SocialMediaDTO dto = dao.getSocMedID(id);
      return dto;
    }


    public string updateSocialMedia(SocialMediaDTO model)
    {
      string oldImagePath = dao.updateSocialMedia(model);
      LogDAO.AddLog(General.ProcessType.SocialUpdate, General.TableName.Social, model.SocialMediaID);
      return oldImagePath;
    }
  }
}
