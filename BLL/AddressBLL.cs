using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public class AddressBLL
  {
    AddressDAO dao = new AddressDAO();
    public bool AddAddress(AddressDTO model)
    {
      TBL_ADDRESS address = new TBL_ADDRESS()
      {
        Address = model.AddressContent,
        Email = model.Email,
        Phone = model.Phone1,
        Phone2 = model.Phone2,
        Fax = model.Fax,
        MapPathLarge = model.LargeMapPath,
        MapPathSmall = model.SmallMapPath,
        addDate = DateTime.Now,
        LastUpdateDate = DateTime.Now,
        LastUpdateUserID = UserStatic.UserID
      };

      int id = dao.AddAddress(address);
      LogDAO.AddLog(General.ProcessType.AddressAdd, General.TableName.Address, id);
      return true;

    }

    public void DeleteAddress(int iD)
    {
      dao.DeleteAddress(iD);
      LogDAO.AddLog(General.ProcessType.AddressDelete, General.TableName.Address, iD);

    }

    public List<AddressDTO> GetAddress()
    {
      return dao.GetAds();
    }

    public AddressDTO GetAddressID(int ID)
    {
      AddressDTO dto = new AddressDTO();
      dto = dao.GetAddressID(ID);
      return dto;
    }

    public bool UpdateMeta(AddressDTO model)
    {
      dao.UpdateAddress(model);
      LogDAO.AddLog(General.ProcessType.AddressUpdate, General.TableName.Address, model.ID);
      return true;
    }
  }
}
