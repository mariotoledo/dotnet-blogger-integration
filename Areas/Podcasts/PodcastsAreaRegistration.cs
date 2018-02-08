using System.Web.Mvc;

namespace NParty.Www.Areas.Podcasts
{
    public class PodcastsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Podcasts";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Podcasts_default",
                "Podcasts/{controller}/{action}/{id}/{desc}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, desc = UrlParameter.Optional },
                new[] { "NParty.Www.Areas.Podcasts.Controllers" }
            );
        }
    }
}
