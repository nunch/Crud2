using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class ArticleSetController : Controller
    {
        
        
        [HttpPost]
        public string create(ArticleSet articleSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ArticleSetDao.create(articleSet);
            

                dic.Add("articleSet", articleSet);
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
            return View("~/Views/Ajax/ArticleSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = ArticleSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(ArticleSet articleSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ArticleSet articleSet_fromdb = ArticleSetDao.find(articleSet.Id_Article);
            
                articleSet_fromdb.PN = articleSet.PN;
                articleSet_fromdb.SIN = articleSet.SIN;
                articleSet_fromdb.IsSuppr = articleSet.IsSuppr;

            ArticleSetDao.update(articleSet_fromdb);
            
            
                dic.Add("articleSet", articleSet_fromdb);
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
            ArticleSet articleSet = ArticleSetDao.find(Id);
            ViewBag.articleSet = articleSet;
            return View("~/Views/Ajax/ArticleSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            ArticleSet obj = ArticleSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<ArticleSet> list = ArticleSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/ArticleSet/list.cshtml");
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/ArticleSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/ArticleSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/ArticleSet/Scripts/listScript.cshtml");
        }
    }
}
