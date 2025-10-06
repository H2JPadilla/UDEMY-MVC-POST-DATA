using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class AddressDAO : PostContext
  {
    public int AddAddress(TBL_ADDRESS address)
    {
      try
      {
        db.TBL_ADDRESS.Add(address);
        db.SaveChanges();
        return address.ID;
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public AddressDTO GetAddressID(int iD)
    {
      TBL_ADDRESS meta = db.TBL_ADDRESS.First(x => x.ID == iD);
      AddressDTO dto = new AddressDTO();
      dto.ID = meta.ID;
      dto.AddressContent = meta.Address;
      dto.Email = meta.Email;
      dto.Phone1 = meta.Phone;
      dto.Phone2 = meta.Phone2;
      dto.Fax = meta.Fax;
      dto.LargeMapPath = meta.MapPathLarge;
      dto.SmallMapPath = meta.MapPathSmall;

      return dto;
    }

    public List<AddressDTO> GetAds()
    {
      List<TBL_ADDRESS> list = db.TBL_ADDRESS.Where(x => x.isDeleted == false || x.isDeleted == null).OrderBy(x => x.addDate).ToList();
      List<AddressDTO> dtolist = new List<AddressDTO>();
      foreach (var item in list)
      {
        AddressDTO dto = new AddressDTO();
        dto.ID = item.ID;
        dto.AddressContent = item.Address;
        dto.Email = item.Email;
        dto.Phone1 = item.Phone;
        dto.Phone2 = item.Phone2;
        dto.LargeMapPath = item.MapPathLarge;
        dto.SmallMapPath = item.MapPathSmall;
        dtolist.Add(dto);
      }
      return dtolist;
    }

    public void UpdateAddress(AddressDTO model)
    {
      try
      {
        TBL_ADDRESS address = db.TBL_ADDRESS.First(x => x.ID == model.ID);
        address.Address = model.AddressContent;
        address.Email = model.Email;
        address.Phone = model.Phone1;
        address.Phone2 = model.Phone2;
        address.Fax = model.Fax;
        address.MapPathLarge = model.LargeMapPath;
        address.MapPathSmall = model.SmallMapPath;
        address.addDate = DateTime.Now;
        address.LastUpdateDate = DateTime.Now;
        address.LastUpdateUserID = UserStatic.UserID;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
