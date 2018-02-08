using System.Web.Mvc;

namespace NParty.Www.Areas.Multi
{
    public class MultiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Multi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Multi_default",
                "Multi/{controller}/{action}/{id}/{desc}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, desc = UrlParameter.Optional },
                new[] { "NParty.Www.Areas.Multi.Controllers" }
            );
        }
    }
}
