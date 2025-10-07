using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL
{
  public class PostBLL
  {
    PostDAO dao = new PostDAO();
    public bool AddPost(PostDTO model)
    {
      TBL_POST post = new TBL_POST();
      post.Title = model.Title;
      post.PostContent = model.PostContent;
      post.ShortContent = model.ShortContent; 
      post.Slider  =model.Slider;
      post.Area1 = model.Area1;
      post.Area2 = model.Area2;
      post.Area3 = model.Area3;
      post.Notification = model.Notification;
      post.CategoryID = model.CategoryID;
      post.SeoLink = SeoLink.GenerateUrl(model.Title);
      post.LanguageName = model.Language;
      post.AddDate = DateTime.Now;
      post.AddUserID = UserStatic.UserID;
      post.LastUpdateUserID = UserStatic.UserID;
      post.LastUpdateDate = DateTime.Now;

      int ID = dao.AddPost(post);
      LogDAO.AddLog(General.ProcessType.PostAdd, General.TableName.Post, ID);
      SavePostImage(model.PostImages, ID);
      AddTag(model.TagText, ID);
      return true;  
    }

    public PostDTO GetPostByID(int iD)
    {
      PostDTO dto = new PostDTO();
      dto = dao.GetPostByID(iD);
      dto.PostImages = dao.GetPostImageByID(iD);
      List<TBL_POST_TAG> taglist = dao.GetPostTagByID(iD);
      string tagvalue = "";
      foreach (var item in taglist)
      {
        tagvalue += item.TagContent;
        tagvalue += ",";
      }
      return dto;
    }

    public List<PostDTO> GetPosts()
    {
      return dao.GetPost();
    }

    public bool UpdatePost(PostDTO model)
    {
      model.SeoLink = SeoLink.GenerateUrl(model.Title);
      dao.UpdatePost(model);
      LogDAO.AddLog(General.ProcessType.PostUpdate, General.TableName.Post, model.ID);

      if (model.PostImages != null)
      {
        SavePostImage(model.PostImages, model.ID);
      }
      dao.DeleteTags(model.ID);
      AddTag(model.TagText, model.ID);
      return true;
    }

    private void AddTag(string tagText, int PostID)
    {
      string[] tags;
      tags = tagText.Split(',');
      List<TBL_POST_TAG> taglist = new List<TBL_POST_TAG>();
      foreach (var item in tags)
      {
        TBL_POST_TAG tag = new TBL_POST_TAG();
        tag.PostID = PostID;
        tag.TagContent = item;
        tag.AddDate = DateTime.Now;
        tag.LastUpdateDate = DateTime.Now;
        tag.LastUpdateUserID = UserStatic.UserID;
        taglist.Add(tag);
      }

      foreach (var item in taglist)
      {
        int tagID = dao.AddTag(item);
        LogDAO.AddLog(General.ProcessType.TagAdd, General.TableName.Tag, tagID);
      }

    }

    void SavePostImage(List<PostImageDTO> list, int PostID)
    {
      List<TBL_POST_IMAGE> imagelist = new List<TBL_POST_IMAGE>();
      foreach (var item in list)
      {
        TBL_POST_IMAGE img = new TBL_POST_IMAGE();
        img.PostID = PostID;
        img.ImagePath = item.ImagePath;
        img.AddDate = DateTime.Now;
        img.LastUpdateDate = DateTime.Now;
        img.LastUpdateUserID = UserStatic.UserID;
        imagelist.Add(img);
      }

      foreach (var item in imagelist)
      {
        int imageID = dao.AddImage(item);
        LogDAO.AddLog(General.ProcessType.ImageAdd, General.TableName.Image, imageID);
      }
    }
    }
  }

