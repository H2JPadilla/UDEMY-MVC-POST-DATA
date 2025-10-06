using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class AdsDAO : PostContext
  {
    public int AddAds(TBL_ADS ads)
    {
      try
      {
        db.TBL_ADS.Add(ads);
        db.SaveChanges();
        return ads.ID;
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public List<AdsDTO> GetAds()
    {
      List<TBL_ADS> list = db.TBL_ADS.Where(x => x.isDeleted == false || x.isDeleted == null).OrderBy(x => x.AddDate).ToList();
      List<AdsDTO> dtolist = new List<AdsDTO>();
      foreach (var item in list)
      {
        AdsDTO dto = new AdsDTO();
        dto.ID = item.ID;
        dto.Name = item.Name;
        dto.Link = item.Link;
        dto.ImagePath = item.ImagePath;
        dtolist.Add(dto);
      }
      return dtolist;
    }

    public AdsDTO GetAdsByID(int id)
    {
      TBL_ADS ads = db.TBL_ADS.First(x => x.ID == id);
      AdsDTO dto = new AdsDTO();
      dto.ID = ads.ID;
      dto.Name = ads.Name;
      dto.Link = ads.Link;
      dto.ImagePath = ads.ImagePath;
      dto.ImageSize = ads.Size;
      return dto;



    }

    public string updateAds(AdsDTO model)
    {
      try
      {
        TBL_ADS social = db.TBL_ADS.FirstOrDefault(x => x.ID == model.ID);
        string oldImagePath = social.ImagePath;
        social.Name = model.Name;
        social.Link = model.Link;
        if (model.ImagePath != null)
          social.ImagePath = model.ImagePath;

        social.Size = model.ImageSize;
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
