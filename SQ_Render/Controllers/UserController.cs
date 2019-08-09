
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace SQ_Render.Controllers
{
  
    public class UserController : Controller
    {

       [HttpPost]
        public string Login(  Dictionary<string, string> entityInfoDic)
        {


            SQDbSet<Users> sQDbSet = new SQDbSet<Users>();

            var user = sQDbSet.GetModelByConditions(t => t.UserName.Equals(entityInfoDic["UserName"])&& 
            t.PassWord.Equals(entityInfoDic["PassWord"]));
            
            if (user != null)
            {
                return "成功";
            }
            return user.ToJSON();
        }
    }
}