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
  public class UserBLL
  {
    UserDAO userdao = new UserDAO();

    public void AddUser(UserDTO model)
    {
      TBL_USER user = new TBL_USER();
      user.Username = model.Username;
      user.Password = model.Password;
      user.Email = model.Email;
      user.ImagePath = model.ImagePath;
      user.NameSurname = model.Name;
      user.isAdmin = model.isAdmin;
      user.AddDate = DateTime.Now;
      user.LastUpdateUserID = UserStatic.UserID;
      int ID = userdao.AddUser(user);
      LogDAO.AddLog(General.ProcessType.UserAdd, General.TableName.User, ID);
    }

    public string DeleteUser(int iD)
    {
      string imagepath = userdao.DeleteUser(iD);
      LogDAO.AddLog(General.ProcessType.UserDelete, General.TableName.User, iD);
      return imagepath;
    }

    public List<UserDTO> GetUser()
    {
      return userdao.GetUser();
    }

    public UserDTO GetUserByID(int id)
    {
      UserDTO dto = userdao.GetUserByID(id);
      return dto;
    }

    public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
    {
      UserDTO dto = new UserDTO();
      dto = userdao.GetUserWithUsernameAndPassword(model);
      return dto;
    }

    public string updateUser(UserDTO model)
    {
      string oldImagePath = userdao.updateUser(model);
      LogDAO.AddLog(General.ProcessType.UserUpdate, General.TableName.User, model.ID);
      return oldImagePath;
    }

  }
}
