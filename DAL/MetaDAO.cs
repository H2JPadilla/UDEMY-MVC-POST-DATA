using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class MetaDAO : PostContext
  {
    public int AddMeta(TBL_META meta)
    {
      try
      {
        db.TBL_META.Add(meta);
        db.SaveChanges();
        return meta.ID;
      }
      catch (Exception)
      {

        throw;
      }
    }

    public void DeleteMeta(int iD)
    {
      try
      {
        TBL_META meta = db.TBL_META.First(x => x.ID == iD);
        meta.isDeleted = true;
        meta.DeletedDate = DateTime.Now;  
        meta.LastUpdateDate = DateTime.Now;
        meta.LastUpdateUserID = UserStatic.UserID;
        db.SaveChanges();
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public List<MetaDTO> GetMetaData()
    {
      List<MetaDTO> dtolist = new List<MetaDTO>();
      List<TBL_META> list = db.TBL_META.Where(x => x.isDeleted == false || x.isDeleted == null).ToList();

      foreach (var item in list)
      {
        MetaDTO dto = new MetaDTO();
        dto.MetaID = item.ID;
        dto.Name = item.Name;
        dto.MetaContent = item.MetaContent;
        dtolist.Add(dto);
      }

      return dtolist;
    }

    public MetaDTO GetMetaID(int ID)
    {
      TBL_META meta = db.TBL_META.First(x => x.ID == ID);
      MetaDTO dto = new MetaDTO();
      dto.MetaID = meta.ID;
      dto.Name = meta.Name;
      dto.MetaContent = meta.MetaContent;
      return dto;

    }

    public void UpdateMeta(MetaDTO model)
    {
      try
      {
        TBL_META meta = db.TBL_META.First(x => x.ID == model.MetaID);
        meta.Name = model.Name;
        meta.MetaContent = model.MetaContent;
        meta.LastUpdateDate = DateTime.Now;
        meta.LastUpdateUserID = UserStatic.UserID;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
