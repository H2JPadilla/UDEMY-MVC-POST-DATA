using DTO;
using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace DAL
{
  public class UserDAO : PostContext
  {

    public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
    {
      UserDTO dto = new UserDTO();
      TBL_USER user = db.TBL_USER.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
      if (user != null && user.ID != 0)
      {
        dto.ID = user.ID;
        dto.Username = user.Username;
        dto.Name = user.NameSurname;
        dto.ImagePath = user.ImagePath;
        dto.isAdmin = user.isAdmin;
      }
      return dto;
    }



    public int AddUser(TBL_USER user)
    {
      try
      {
        db.TBL_USER.Add(user);
        db.SaveChanges();
        return user.ID;
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public List<UserDTO> GetUser()
    {
      List<TBL_USER> list = db.TBL_USER.Where(x => x.isDeleted == false || x.isDeleted == null).OrderBy(x => x.AddDate).ToList();
      List<UserDTO> userlist = new List<UserDTO>();

      foreach (var item in list)
      {
        UserDTO dto = new UserDTO();
        dto.ID = item.ID;
        dto.Name = item.NameSurname;
        dto.Username = item.Username;
        dto.ImagePath = item.ImagePath;
        userlist.Add(dto);
      }
      return userlist;

    }

    public UserDTO GetUserByID(int ID)
    {
      TBL_USER user = db.TBL_USER.First(x => x.ID == ID);
      UserDTO dto = new UserDTO();
      dto.ID = user.ID;
      dto.Name = user.NameSurname;
      dto.Username = user.Username;
      dto.Password = user.Password;
      dto.isAdmin = user.isAdmin;
      dto.Email = user.Email;
      dto.ImagePath = user.ImagePath;
      return dto;
    }

    public string updateUser(UserDTO model)
    {
      try
      {
        TBL_USER user = db.TBL_USER.FirstOrDefault(x => x.ID == model.ID);
        string oldImagePath = user.ImagePath;
        user.NameSurname = model.Name;
        user.Username = model.Username;
        if (model.ImagePath != null)
          user.ImagePath = model.ImagePath;

        user.Email = model.Email;
        user.Password = model.Password;
        user.LastUpdateDate = DateTime.Now;
        user.LastUpdateUserID = UserStatic.UserID;
        user.isAdmin = model.isAdmin;
        db.SaveChanges();
        return oldImagePath;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string DeleteUser(int iD)
    {
      try
      {
        TBL_USER user = db.TBL_USER.First(x => x.ID == iD);
        string imagepath = user.ImagePath;
        user.isDeleted = true;
        user.DeletedDate = DateTime.Now;
        user.LastUpdateDate = DateTime.Now;
        user.LastUpdateUserID = UserStatic.UserID;
        db.SaveChanges();

        return imagepath;
      }
      catch (Exception ex)
      {

        throw ex;
      }      
    }
  }
}
