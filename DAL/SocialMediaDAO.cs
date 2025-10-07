using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class SocialMediaDAO : PostContext
  {
    public int AddSocialMedia(TBL_SOCIAL_MEDIA social)
    {
      try
      {
        db.TBL_SOCIAL_MEDIA.Add(social);
        db.SaveChanges();
        return social.ID;
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public string DeleteSocialMedia(int iD)
    {
      try
      {
        TBL_SOCIAL_MEDIA social = db.TBL_SOCIAL_MEDIA.First(x => x.ID == iD);
        string imagepath = social.ImagePath;
        social.isDeleted = true;
        social.DeletedDate = DateTime.Now;
        social.LastUpdateDate = DateTime.Now;
        social.LastUpdateUserID = UserStatic.UserID;
        return imagepath;
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public List<SocialMediaDTO> GetSocialMedia()
    {
      List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
      List<TBL_SOCIAL_MEDIA> list = db.TBL_SOCIAL_MEDIA.Where(x => x.isDeleted == false || x.isDeleted == null).ToList();

      foreach (var item in list)
      {
        SocialMediaDTO dto = new SocialMediaDTO();
        dto.Name = item.Name;
        dto.Link = item.Link;
        dto.ImagePath = item.ImagePath;
        dto.SocialMediaID = item.ID;
        dtolist.Add(dto);
      }
      return dtolist;
    }

    public SocialMediaDTO getSocMedID(int ID)
    {
      TBL_SOCIAL_MEDIA socialmedia = db.TBL_SOCIAL_MEDIA.First(x => x.ID == ID);
      SocialMediaDTO dto = new SocialMediaDTO();

      dto.SocialMediaID = socialmedia.ID;
      dto.Name = socialmedia.Name;
      dto.Link = socialmedia.Link;
      dto.ImagePath = socialmedia.ImagePath;
      return dto;
    }

    public string updateSocialMedia(SocialMediaDTO model)
    {
      try
      {
        TBL_SOCIAL_MEDIA social = db.TBL_SOCIAL_MEDIA.FirstOrDefault(x => x.ID == model.SocialMediaID);
        string oldImagePath = social.ImagePath;
        social.Name = model.Name;
        social.Link = model.Link;
        if (model.ImagePath != null)
          social.ImagePath = model.ImagePath;

        social.LastUpdateDate = DateTime.Now;
        social.LastUpdateUserID = UserStatic.UserID;
        db.SaveChanges();
        return oldImagePath;
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }
  }
}
