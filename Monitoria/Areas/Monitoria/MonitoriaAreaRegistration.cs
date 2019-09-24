using System.Web.Mvc;

namespace Monitoria.Areas.Monitoria
{
    public class MonitoriaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Monitoria";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Monitoria_default",
                "Monitoria/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}