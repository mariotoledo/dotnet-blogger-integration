using System.Web.Mvc;

namespace NParty.Www.Areas.ESports
{
    public class ESportsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ESports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ESports_default",
                "ESports/{controller}/{action}/{id}/{desc}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional, desc = UrlParameter.Optional },
                new[] { "NParty.Www.Areas.ESports.Controllers" }
            );
        }
    }
}
