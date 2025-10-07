using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public class AdsBLL
  {
    AdsDAO dao = new AdsDAO();
    public bool AddAds(AdsDTO model)
    {
      TBL_ADS ads = new TBL_ADS();
      ads.Name = model.Name;
      ads.Link = model.Link;
      ads.ImagePath = model.ImagePath;
      ads.Size = model.ImageSize;
      ads.AddDate = DateTime.Now;
      ads.LastUpdateUserID = UserStatic.UserID;
      ads.LastUpdateDate = DateTime.UtcNow;
      int ID = dao.AddAds(ads);
      LogDAO.AddLog(General.ProcessType.AdsAdd, General.TableName.Ads, ID);
      return true;
    }

    public string DeleteAds(int iD)
    {
      string imagepath = dao.DeleteAds(iD);
      return imagepath;

    }

    public List<AdsDTO> GetAds()
    {
      return dao.GetAds();
    }

    public AdsDTO GetAdsByID(int id)
    {
      return dao.GetAdsByID(id);
    }

    public string updateAds(AdsDTO model)
    {
      string oldImagePath = dao.updateAds(model);
      LogDAO.AddLog(General.ProcessType.AdsUpdate, General.TableName.Ads, model.ID);
      return oldImagePath;
    }
  }
}
