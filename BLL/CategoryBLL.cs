using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL
{
  public class CategoryBLL
  {
    CategoryDAO dao = new CategoryDAO();

    public static List<SelectListItem> GetCategoryForDropDown()
    {
      return (List<SelectListItem>)CategoryDAO.GetCategoryForDropDown();
    }

    public bool AddCategory(CategoryDTO model)
    {
      TBL_CATEGORY cat = new TBL_CATEGORY();
      cat.CategoryName = model.Category;
      cat.AddDate = DateTime.Now;
      cat.LastUpdateUserID = UserStatic.UserID;
      cat.LastUpdateDate = DateTime.Now;
      int ID = dao.AddCategory(cat);

      LogDAO.AddLog(General.ProcessType.CategoryAdd, General.TableName.Category, ID);
      return true;
    }

    public List<CategoryDTO> GetCategory()
    {
      List<CategoryDTO> dtolist = new List<CategoryDTO>();
      dtolist = dao.GetCategory();
      return dtolist;
    }

    public CategoryDTO GetCategoryID(int id)
    {
      CategoryDTO dto = new CategoryDTO();
      dto = dao.GetCategoryID(id);
      return dto;
    }

    public bool UpdateCategory(CategoryDTO model)
    {
      dao.UpdateCategory(model);
      LogDAO.AddLog(General.ProcessType.CategoryUpdate, General.TableName.Category, model.ID);
      return true;
    }
  }
}
