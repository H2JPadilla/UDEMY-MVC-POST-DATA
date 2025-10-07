using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class PostDAO:PostContext
  {
    public int AddImage(TBL_POST_IMAGE item)
    {
      db.TBL_POST_IMAGE.Add(item);
      db.SaveChanges();
      return item.ID;
    }

    public int AddPost(TBL_POST post)
    {
			try
			{
				db.TBL_POST.Add(post);
        db.SaveChanges();
        return post.ID;
      }
			catch (Exception ex)
			{

				throw ex;
			}
    }

    public int AddTag(TBL_POST_TAG item)
    {
      db.TBL_POST_TAG.Add(item);
      db.SaveChanges();
      return item.ID;
    }

    public void DeleteTags(int PostID)
    {
      List<TBL_POST_TAG>list = db.TBL_POST_TAG.Where(x=> x.isDeleted == false || x.isDeleted == null).ToList();
      foreach (var item in list)
      {
        item.isDeleted = true;
        item.DeletedDate = DateTime.Now;
        item.LastUpdateDate = DateTime.Now;
        item.LastUpdateUserID = UserStatic.UserID;
      }
      db.SaveChanges ();
    }

    public List<PostDTO> GetPost()
    {
      var postlist = (from p in db.TBL_POST.Where(x=> x.isDeleted == false || x.isDeleted == null)
                      join c in db.TBL_CATEGORY on p.CategoryID equals c.ID
                      select new
                      {
                        ID = p.ID,
                        Title = p.Title,
                        CategoryName = c.CategoryName,
                        AddDate = p.AddDate
                      }).OrderByDescending(x => x.AddDate).ToList();
      List<PostDTO> dtolist = new List<PostDTO>();
      foreach(var item in postlist)
      {
        PostDTO dto = new PostDTO()
        {
          Title = item.Title,
          ID = item.ID,
          CategoryName = item.CategoryName,
          AddDate = item.AddDate
        };
        dtolist.Add(dto);
      }
      return dtolist;
    }

    public PostDTO GetPostByID(int iD)
    {
      TBL_POST post = db.TBL_POST.First(x => x.ID == iD);
      PostDTO dto = new PostDTO()
      {
        ID = post.ID,
        Title = post.Title,
        ShortContent = post.ShortContent ,
        PostContent = post.PostContent,
        Language = post.LanguageName,
        Notification = post.Notification, 
        SeoLink = post.SeoLink ,
        Slider = post.Slider ,
        Area1 = post.Area1 ,
        Area2 = post.Area2 ,  
        Area3 = post.Area3 ,  
        CategoryID = post.CategoryID
      };
      return dto;
    }

    public List<PostImageDTO> GetPostImageByID(int PostID)
    {
      List<TBL_POST_IMAGE>list = db.TBL_POST_IMAGE.Where(x=>x.isDeleted == false ||  x.isDeleted == null).Where(x=>x.PostID == PostID).ToList();
      List<PostImageDTO> dtolist = new List<PostImageDTO>();
    
      foreach (var item in list)
      {
        PostImageDTO dto = new PostImageDTO()
        {
          ID = item.ID, 
          ImagePath = item.ImagePath
        };
        dtolist.Add(dto);
      }

      return dtolist;

    }

    public List<TBL_POST_TAG> GetPostTagByID(int PostID)
    {
      return db.TBL_POST_TAG.Where(x => x.isDeleted == false || x.isDeleted == null).ToList();
    }

    public void UpdatePost(PostDTO model)
    {
      TBL_POST post = db.TBL_POST.First(x => x.ID == model.ID);
      post.Title = model.Title;
      post.Area1 = model.Area1;
      post.Area2 = model.Area2;
      post.Area3 = model.Area3 ;
      post.CategoryID = model.CategoryID;
      post.LanguageName = model.Language;
      post.LastUpdateDate = DateTime.Now;
      post.LastUpdateUserID = UserStatic.UserID;
      post.Notification= model.Notification;
      post.PostContent = model.PostContent;
      post.SeoLink = model.SeoLink;
      post.ShortContent = model.ShortContent;
      post.Slider = model.Slider;
      db.SaveChanges();

    }
  }
}
