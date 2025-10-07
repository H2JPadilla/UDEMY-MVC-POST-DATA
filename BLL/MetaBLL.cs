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
  public class MetaBLL
  {
    public MetaDAO dao = new MetaDAO();
    public bool AddMeta(MetaDTO model)
    {
      TBL_META meta = new TBL_META();
      meta.Name = model.Name;
      meta.MetaContent = model.MetaContent;
      meta.AddDate = DateTime.Now;
      meta.LastUpdateUserID = UserStatic.UserID;
      meta.LastUpdateDate = DateTime.Now;
      int MetaID = dao.AddMeta(meta);
      LogDAO.AddLog(General.ProcessType.MetaAdd, General.TableName.Meta, MetaID);
      return true;

    }

    public void DeleteMeta(int iD)
    {
      dao.DeleteMeta(iD);
      LogDAO.AddLog(General.ProcessType.MetaDelete, General.TableName.Meta, iD);
    }

    public List<MetaDTO> GetMetaData()
    {
      List<MetaDTO> dtolist = new List<MetaDTO>();
      dtolist = dao.GetMetaData();
      return dtolist;

    }

    public MetaDTO GetMetaID(int ID)
    {
      MetaDTO dto = new MetaDTO();
      dto = dao.GetMetaID(ID);
      return dto;
    }

    public bool UpdateMeta(MetaDTO model)
    {
      dao.UpdateMeta(model);
      LogDAO.AddLog(General.ProcessType.MetaUpdate, General.TableName.Meta, model.MetaID);
      return true;
    }
  }
}
