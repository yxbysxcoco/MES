using SQ_DB_Framework;
using System.Web.Services.Description;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollection = Microsoft.Extensions.DependencyInjection.ServiceCollection;
using Microsoft.EntityFrameworkCore;
using SQ_DB_Framework.EFDbAccess;

namespace SQ_Render.App_Start
{
    public class MyDbConnetion
    {
     
        public static void Initialize(IServiceCollection services)
        {
            MyConnection.Initialize(new ServiceCollection());
        }
    }
}