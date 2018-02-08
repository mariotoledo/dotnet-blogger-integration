using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Nintendo.Controllers
{
    public class ArtigosController : NintendoController
    {
        //
        // GET: /Nintendo/Artigos/

        public ActionResult Ler(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            Article article = null;
            
            for(int i = 0; i < 3 && article == null; i++){
                article = Helper.GetSingleArticleFromBlog(NintendoBlogId, "Nintendo", id);

                if (article == null)
                    RotateApiKey();
            }

            if (article == null)
                return Redirect("~/Ops/NaoEncontrado");

            ViewData["article"] = article;

            List<Article> hilights = Helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 5, 0, "Destaque", "/Artigos/Ler/");
            ViewData["hilights"] = hilights;

            return View();
        }

        public JsonResult GetRelatedPosts(string label)
        {
            ArticlesHelper helper = new ArticlesHelper();
            return Json(helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 3, 0, label, "/Artigos/Ler/"), JsonRequestBehavior.AllowGet);
        } 

    }
}
