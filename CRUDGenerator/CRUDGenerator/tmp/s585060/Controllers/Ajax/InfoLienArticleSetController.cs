using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class InfoLienArticleSetController : Controller
    {
        
        
        [HttpPost]
        public string create(InfoLienArticleSet infoLienArticleSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            InfoLienArticleSetDao.create(infoLienArticleSet);
            

                dic.Add("infoLienArticleSet", infoLienArticleSet);
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
            return View("~/Views/Ajax/InfoLienArticleSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = InfoLienArticleSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(InfoLienArticleSet infoLienArticleSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            InfoLienArticleSet infoLienArticleSet_fromdb = InfoLienArticleSetDao.find(infoLienArticleSet.Id_InfoLienArticle);
            
                infoLienArticleSet_fromdb.SourceFichier = infoLienArticleSet.SourceFichier;
                infoLienArticleSet_fromdb.IsSuppr = infoLienArticleSet.IsSuppr;
                infoLienArticleSet_fromdb.Id_LienArticle = infoLienArticleSet.Id_LienArticle;
                infoLienArticleSet_fromdb.DateDebut = infoLienArticleSet.DateDebut;
                infoLienArticleSet_fromdb.DateFin = infoLienArticleSet.DateFin;
                infoLienArticleSet_fromdb.Champ1 = infoLienArticleSet.Champ1;
                infoLienArticleSet_fromdb.Champ2 = infoLienArticleSet.Champ2;
                infoLienArticleSet_fromdb.QuantiteLiee = infoLienArticleSet.QuantiteLiee;

            InfoLienArticleSetDao.update(infoLienArticleSet_fromdb);
            
            
                dic.Add("infoLienArticleSet", infoLienArticleSet_fromdb);
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
            InfoLienArticleSet infoLienArticleSet = InfoLienArticleSetDao.find(Id);
            ViewBag.infoLienArticleSet = infoLienArticleSet;
            return View("~/Views/Ajax/InfoLienArticleSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            InfoLienArticleSet obj = InfoLienArticleSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<InfoLienArticleSet> list = InfoLienArticleSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/InfoLienArticleSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdLienArticlesSet(int IdLienArticlesSet)
        {
            List<InfoLienArticleSet> list = InfoLienArticleSetDao.findByIdLienArticlesSet(IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/InfoLienArticleSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/InfoLienArticleSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/InfoLienArticleSet/Scripts/listScript.cshtml");
        }
    }
}
