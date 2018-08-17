using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class EcartSetController : Controller
    {
        
        
        [HttpPost]
        public string create(EcartSet ecartSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            EcartSetDao.create(ecartSet);
            

                dic.Add("ecartSet", ecartSet);
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
            return View("~/Views/Ajax/EcartSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = EcartSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(EcartSet ecartSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            EcartSet ecartSet_fromdb = EcartSetDao.find(ecartSet.Id_Ecart);
            
                ecartSet_fromdb.NumImport = ecartSet.NumImport;
                ecartSet_fromdb.IsBoxarr = ecartSet.IsBoxarr;
                ecartSet_fromdb.IsBaan = ecartSet.IsBaan;
                ecartSet_fromdb.IsTIP = ecartSet.IsTIP;
                ecartSet_fromdb.IsSuppr = ecartSet.IsSuppr;
                ecartSet_fromdb.Id_Cle = ecartSet.Id_Cle;
                ecartSet_fromdb.NumAxe = ecartSet.NumAxe;
                ecartSet_fromdb.LienArticles_Id_LienArticles = ecartSet.LienArticles_Id_LienArticles;
                ecartSet_fromdb.Article_Id_Article = ecartSet.Article_Id_Article;
                ecartSet_fromdb.AssoArticleSource_Id_AssoArticleSource = ecartSet.AssoArticleSource_Id_AssoArticleSource;

            EcartSetDao.update(ecartSet_fromdb);
            
            
                dic.Add("ecartSet", ecartSet_fromdb);
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
            EcartSet ecartSet = EcartSetDao.find(Id);
            ViewBag.ecartSet = ecartSet;
            return View("~/Views/Ajax/EcartSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            EcartSet obj = EcartSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<EcartSet> list = EcartSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/EcartSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdArticleSet(int IdArticleSet)
        {
            List<EcartSet> list = EcartSetDao.findByIdArticleSet(IdArticleSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdAssoArticleSourceSet(int IdAssoArticleSourceSet)
        {
            List<EcartSet> list = EcartSetDao.findByIdAssoArticleSourceSet(IdAssoArticleSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdAssoArticleSourceSet(int IdArticleSet, int IdAssoArticleSourceSet)
        {
            List<EcartSet> list = EcartSetDao.findByIdArticleSet_IdAssoArticleSourceSet(IdArticleSet, IdAssoArticleSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdLienArticlesSet(int IdLienArticlesSet)
        {
            List<EcartSet> list = EcartSetDao.findByIdLienArticlesSet(IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdLienArticlesSet(int IdArticleSet, int IdLienArticlesSet)
        {
            List<EcartSet> list = EcartSetDao.findByIdArticleSet_IdLienArticlesSet(IdArticleSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdAssoArticleSourceSet_IdLienArticlesSet(int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<EcartSet> list = EcartSetDao.findByIdAssoArticleSourceSet_IdLienArticlesSet(IdAssoArticleSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdAssoArticleSourceSet_IdLienArticlesSet(int IdArticleSet, int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<EcartSet> list = EcartSetDao.findByIdArticleSet_IdAssoArticleSourceSet_IdLienArticlesSet(IdArticleSet, IdAssoArticleSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/EcartSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/EcartSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/EcartSet/Scripts/listScript.cshtml");
        }
    }
}
