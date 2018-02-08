using System.Web.Mvc;

namespace NParty.Www.Areas.Nintendo
{
    public class NintendoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Nintendo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Pokemon_default",
                "Nintendo/{controller}/{action}/{id}/{desc}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, desc = UrlParameter.Optional },
                new[] { "NParty.Www.Areas.Nintendo.Controllers" }
            );
        }
    }
}
