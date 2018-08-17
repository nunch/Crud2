using System.Web.Mvc;

namespace CRUDGenerator.Areas.SQLManagement
{
    public class SQLManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SQLManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "SQLManagement_default",
                "SQLManagement/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "CRUDGenerator.Areas.SQLManagement.Controllers" }
            );
        }
    }
}
