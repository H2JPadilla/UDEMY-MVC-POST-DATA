using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public class LayoutBLL
  {
    CategoryDAO catdao = new CategoryDAO();
    SocialMediaDAO socdao = new SocialMediaDAO();
    FavDAO favdao = new FavDAO();
    MetaDAO metadao = new MetaDAO();

    public HomeLayoutDTO GetLayoutData()
    {
      HomeLayoutDTO dto = new HomeLayoutDTO();
      dto.Categories = catdao.GetCategory();

      List<SocialMediaDTO> socialList = new List<SocialMediaDTO>(); ;
      socialList = socdao.GetSocialMedia();

      dto.Facebook = socialList.First(x=>x.Link.Contains("facebook"));
      dto.Twitter = socialList.First(x => x.Link.Contains("twitter"));
      dto.Instagram = socialList.First(x => x.Link.Contains("instagram"));
      dto.Youtube = socialList.First(x => x.Link.Contains("youtube"));
      dto.LinkedIn = socialList.First(x => x.Link.Contains("linkedin"));
      dto.FavDTO = favdao.GetFav();
      dto.MetaList = metadao.GetMetaData(); 
      
      return dto;
    }
  }
}
