using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public class FavBLL
  {
    FavDAO dao = new FavDAO();
    public FavDTO GetFav()
    {
      FavDTO dto = dao.GetFav();
      return dto;
    }

    public FavDTO UpdateFav(FavDTO model)
    {
      FavDTO dto = new FavDTO();
      dto = dao.UpdateFav(model);
      LogDAO.AddLog(General.ProcessType.IconUpdate, General.TableName.Icon, model.ID);
      return dto;
    }
  }
}
