using System.Web.Mvc;

namespace NParty.Www.Areas.Eventos
{
    public class EventosAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Eventos";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Eventos_default",
                "Eventos/{controller}/{action}/{id}/{desc}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, desc = UrlParameter.Optional },
                new[] { "NParty.Www.Areas.Eventos.Controllers" }
            );
        }
    }
}
