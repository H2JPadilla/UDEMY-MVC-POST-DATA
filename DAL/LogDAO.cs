using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
  public class LogDAO : PostContext
  {
    public static void AddLog(int ProcessType, string TableName, int ProcessID)
    {

      TBL_LOG log = new TBL_LOG();
      log.UserID = UserStatic.UserID;
      log.ProcessType = ProcessType;
      log.ProcessID = ProcessID;
      log.ProcessCategoryType = TableName;
      log.ProcessDate = DateTime.Now;
      log.IPAddress = HttpContext.Current.Request.UserHostAddress;
      db.TBL_LOG.Add(log);
      db.SaveChanges();
    }
  }
}
