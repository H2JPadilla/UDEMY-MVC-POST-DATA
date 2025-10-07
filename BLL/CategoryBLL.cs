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

    PostBLL postbll = new PostBLL();
    public List<PostImageDTO> DeleteCategory(int iD)
    {
      List<TBL_POST> postlist = dao.DeleteCategory(iD);
      LogDAO.AddLog(General.ProcessType.CategoryDelete, General.TableName.Category, iD);
      List<PostImageDTO> imagelist = new List<PostImageDTO>(); 

      foreach (var item in postlist)
      {
        List<PostImageDTO> imagelist2 = postbll.DeletePost(item.ID);
        LogDAO.AddLog(General.ProcessType.PostDelete, General.TableName.Post, item.ID);
        foreach(var item2 in imagelist2)
        {
          imagelist.Add(item2);
        }
      }
      return imagelist;
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
