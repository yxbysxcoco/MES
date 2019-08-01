
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace SQ_Render.Controllers
{
  
    public class UserController : Controller
    {

       
        public string Login( [FromBody] Dictionary<string, string> entityInfoDic)
        {


            SQDbSet<Users> sQDbSet = new SQDbSet<Users>();

            var user = sQDbSet.GetEntitiesByContion(entityInfoDic);
            
            if (user.Count != 0)
            {
                return "成功";
            }
            return user.ToJSON();
        }
    }
}