using DTO;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL
{
  public class CategoryDAO : PostContext
  {
    public static IEnumerable<SelectListItem> GetCategoryForDropDown()
    {
      IEnumerable<SelectListItem> categorylist = db.TBL_CATEGORY.Where(x => x.isDeleted == false || x.isDeleted == null).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
      {
        Text = x.CategoryName,
        Value = SqlFunctions.StringConvert((double)x.ID)
      }).ToList();
      return categorylist;
    }

    public int AddCategory(TBL_CATEGORY cat)
    {
      db.TBL_CATEGORY.Add(cat);
      db.SaveChanges();
      return cat.ID;
    }

    public List<TBL_POST> DeleteCategory(int iD)
    {
      try
      {
        TBL_CATEGORY cat = db.TBL_CATEGORY.First(x => x.ID == iD);
        cat.isDeleted = true;
        cat.DeletedDate = DateTime.Now;
        cat.LastUpdateUserID = UserStatic.UserID;
        cat.LastUpdateDate = DateTime.Now;
        db.SaveChanges();

        List<TBL_POST> postlist = db.TBL_POST.Where(x => x.isDeleted == false || x.isDeleted == null).Where(x => x.CategoryID == iD).ToList();
        return postlist;
      }
      catch (Exception ex)
      {

        throw ex;
      }

    }

    public List<CategoryDTO> GetCategory()
    {
      List<CategoryDTO> categorylist = new List<CategoryDTO>();
      List<TBL_CATEGORY> list = db.TBL_CATEGORY.Where(x => x.isDeleted == false || x.isDeleted == null).OrderBy(x => x.AddDate).ToList();

      foreach (var item in list)
      {
        CategoryDTO dto = new CategoryDTO();
        dto.ID = item.ID;
        dto.Category = item.CategoryName;
        categorylist.Add(dto);
      }
      return categorylist;

    }

    public CategoryDTO GetCategoryID(int id)
    {
      TBL_CATEGORY cat = db.TBL_CATEGORY.First(x => x.ID == id);
      CategoryDTO dto = new CategoryDTO();
      dto.ID = cat.ID;
      dto.Category = cat.CategoryName;
      return dto;
    }



    public void UpdateCategory(CategoryDTO model)
    {
      try
      {
        TBL_CATEGORY cat = db.TBL_CATEGORY.First(x => x.ID == model.ID);
        cat.CategoryName = model.Category;
        cat.LastUpdateDate = DateTime.Now;
        cat.LastUpdateUserID = UserStatic.UserID;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
