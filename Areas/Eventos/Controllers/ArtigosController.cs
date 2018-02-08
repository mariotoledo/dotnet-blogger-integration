using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Eventos.Controllers
{
    public class ArtigosController : EventosController
    {
        //
        // GET: /Eventos/Artigos/

        public ActionResult Ler(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            Article article = Helper.GetSingleArticleFromBlog(EventosBlogId, "Eventos", id);

            if (article == null)
                return Redirect("~/Ops/NaoEncontrado");

            ViewData["article"] = article;

            List<Article> hilights = Helper.GetArticlesFromBlog(EventosBlogId, "Eventos", 5, 0, "Destaque", "/Artigos/Ler/");
            ViewData["hilights"] = hilights;

            return View();
        }

        public JsonResult GetRelatedPosts(string label)
        {
            ArticlesHelper helper = new ArticlesHelper();
            return Json(helper.GetArticlesFromBlog(EventosBlogId, "Eventos", 3, 0, label, "/Artigos/Ler/"), JsonRequestBehavior.AllowGet);
        }

    }
}
