using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public enum MessageType
    {
        Success, Info, Error
    }

    public class NPartyController : Controller
    {
        protected void FlashMessage(string message, MessageType type)
        {
            TempData["FlashMessage"] = message;

            switch (type)
            {
                case MessageType.Success:
                    TempData["FlashMessageType"] = "success";
                    break;
                case MessageType.Info:
                    TempData["FlashMessageType"] = "info";
                    break;
                case MessageType.Error:
                    TempData["FlashMessageType"] = "danger";
                    break;
            }
        }

        public string NintendoBlogId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["NintendoBlogId"];
            }
        }

        public string ESportsBlogId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["eSportsBlogId"];
            }
        }

        public string EventosBlogId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["EventosBlogId"];
            }
        }

        public string MainBlogId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MainBlogId"];
            }
        }

        public int MaxPosts
        {
            get
            {
                try
                {
                    return Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxPosts"]);
                } catch(Exception)
                {
                    return 10;
                }                
            }
        }
        
        public string[] NintendoSpecialTags
        {
            get
            {
                try
                {
                    string unsplitedSpecialTags = System.Configuration.ConfigurationManager.AppSettings["NintendoSpecialTags"];
                    return unsplitedSpecialTags.Split(',').Select(p => p.Trim()).ToArray();
                }
                catch (Exception)
                {
                    return new string[] { };
                }
            }
        }

        public string[] PodcastsSpecialTags
        {
            get
            {
                try
                {
                    string unsplitedSpecialTags = System.Configuration.ConfigurationManager.AppSettings["PodcastsSpecialTags"];
                    return unsplitedSpecialTags.Split(',').Select(p => p.Trim()).ToArray();
                }
                catch (Exception)
                {
                    return new string[] { };
                }
            }
        }

        public string[] ESportsSpecialTags
        {
            get
            {
                try
                {
                    string unsplitedSpecialTags = System.Configuration.ConfigurationManager.AppSettings["ESportsSpecialTags"];
                    return unsplitedSpecialTags.Split(',').Select(p => p.Trim()).ToArray();
                }
                catch (Exception)
                {
                    return new string[] { };
                }
            }
        }

        public string[] EventosSpecialTags
        {
            get
            {
                try
                {
                    string unsplitedSpecialTags = System.Configuration.ConfigurationManager.AppSettings["EventosSpecialTags"];
                    return unsplitedSpecialTags.Split(',').Select(p => p.Trim()).ToArray();
                }
                catch (Exception)
                {
                    return new string[] { };
                }
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["NintendoSpecialTags"] = this.NintendoSpecialTags;
            ViewData["ESportsSpecialTags"] = this.ESportsSpecialTags;
            ViewData["EventosSpecialTags"] = this.EventosSpecialTags;
            ViewData["PodcastsSpecialTags"] = this.PodcastsSpecialTags;
            ViewData["MaxPosts"] = MaxPosts;

            base.OnActionExecuting(filterContext);
        }
    }
}