using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class InfoArticleSetController : Controller
    {
        
        
        [HttpPost]
        public string create(InfoArticleSet infoArticleSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            InfoArticleSetDao.create(infoArticleSet);
            

                dic.Add("infoArticleSet", infoArticleSet);
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
            return View("~/Views/Ajax/InfoArticleSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = InfoArticleSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(InfoArticleSet infoArticleSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            InfoArticleSet infoArticleSet_fromdb = InfoArticleSetDao.find(infoArticleSet.Id_InfoArticle);
            
                infoArticleSet_fromdb.SourceFichier = infoArticleSet.SourceFichier;
                infoArticleSet_fromdb.Id_Article = infoArticleSet.Id_Article;
                infoArticleSet_fromdb.IsSuppr = infoArticleSet.IsSuppr;
                infoArticleSet_fromdb.Id_Entite = infoArticleSet.Id_Entite;
                infoArticleSet_fromdb.DesEN = infoArticleSet.DesEN;
                infoArticleSet_fromdb.DesFR = infoArticleSet.DesFR;
                infoArticleSet_fromdb.CategorieSPI = infoArticleSet.CategorieSPI;

            InfoArticleSetDao.update(infoArticleSet_fromdb);
            
            
                dic.Add("infoArticleSet", infoArticleSet_fromdb);
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
            InfoArticleSet infoArticleSet = InfoArticleSetDao.find(Id);
            ViewBag.infoArticleSet = infoArticleSet;
            return View("~/Views/Ajax/InfoArticleSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            InfoArticleSet obj = InfoArticleSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<InfoArticleSet> list = InfoArticleSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/InfoArticleSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdArticleSet(int IdArticleSet)
        {
            List<InfoArticleSet> list = InfoArticleSetDao.findByIdArticleSet(IdArticleSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdEntiteSet(int IdEntiteSet)
        {
            List<InfoArticleSet> list = InfoArticleSetDao.findByIdEntiteSet(IdEntiteSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdEntiteSet(int IdArticleSet, int IdEntiteSet)
        {
            List<InfoArticleSet> list = InfoArticleSetDao.findByIdArticleSet_IdEntiteSet(IdArticleSet, IdEntiteSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/InfoArticleSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/InfoArticleSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/InfoArticleSet/Scripts/listScript.cshtml");
        }
    }
}
