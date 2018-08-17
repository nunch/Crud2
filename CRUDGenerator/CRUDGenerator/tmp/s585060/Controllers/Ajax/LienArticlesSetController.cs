using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class LienArticlesSetController : Controller
    {
        
        
        [HttpPost]
        public string create(LienArticlesSet lienArticlesSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            LienArticlesSetDao.create(lienArticlesSet);
            

                dic.Add("lienArticlesSet", lienArticlesSet);
                dic.Add("success", "true");
            }
            catch (Exception e)
            {
                dic.Add("success", "false");
                dic.Add("errorMessage", e.Message);
            }
            return JsonConvert.SerializeObject(dic);
        }
        
        [HttpGet]
        public ActionResult create()
        {
            return View("~/Views/Ajax/LienArticlesSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = LienArticlesSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(LienArticlesSet lienArticlesSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            LienArticlesSet lienArticlesSet_fromdb = LienArticlesSetDao.find(lienArticlesSet.Id_LienArticles);
            
                lienArticlesSet_fromdb.Id_ArticleParent = lienArticlesSet.Id_ArticleParent;
                lienArticlesSet_fromdb.Id_ArticleEnfant = lienArticlesSet.Id_ArticleEnfant;
                lienArticlesSet_fromdb.Programme = lienArticlesSet.Programme;
                lienArticlesSet_fromdb.IsSuppr = lienArticlesSet.IsSuppr;

            LienArticlesSetDao.update(lienArticlesSet_fromdb);
            
            
                dic.Add("lienArticlesSet", lienArticlesSet_fromdb);
                dic.Add("success", "true");
            }
            catch (Exception e)
            {
                dic.Add("success", "false");
                dic.Add("errorMessage", e.Message);
            }
            return JsonConvert.SerializeObject(dic);
        }

        [HttpPost]
        public ActionResult modify(int Id)
        {
            LienArticlesSet lienArticlesSet = LienArticlesSetDao.find(Id);
            ViewBag.lienArticlesSet = lienArticlesSet;
            return View("~/Views/Ajax/LienArticlesSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            LienArticlesSet obj = LienArticlesSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<LienArticlesSet> list = LienArticlesSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/LienArticlesSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdArticleSet(int IdArticleSet)
        {
            List<LienArticlesSet> list = LienArticlesSetDao.findByIdArticleSet(IdArticleSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet(int IdArticleSet)
        {
            List<LienArticlesSet> list = LienArticlesSetDao.findByIdArticleSet(IdArticleSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdArticleSet(int IdArticleSet, int IdArticleSet)
        {
            List<LienArticlesSet> list = LienArticlesSetDao.findByIdArticleSet_IdArticleSet(IdArticleSet, IdArticleSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/LienArticlesSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/LienArticlesSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/LienArticlesSet/Scripts/listScript.cshtml");
        }
    }
}
