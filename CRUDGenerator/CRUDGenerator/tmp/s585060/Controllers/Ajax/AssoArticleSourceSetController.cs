using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class AssoArticleSourceSetController : Controller
    {
        
        
        [HttpPost]
        public string create(AssoArticleSourceSet assoArticleSourceSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            AssoArticleSourceSetDao.create(assoArticleSourceSet);
            

                dic.Add("assoArticleSourceSet", assoArticleSourceSet);
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
            return View("~/Views/Ajax/AssoArticleSourceSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = AssoArticleSourceSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(AssoArticleSourceSet assoArticleSourceSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            AssoArticleSourceSet assoArticleSourceSet_fromdb = AssoArticleSourceSetDao.find(assoArticleSourceSet.Id_AssoArticleSource);
            
                assoArticleSourceSet_fromdb.IsSuppr = assoArticleSourceSet.IsSuppr;
                assoArticleSourceSet_fromdb.Id_Article = assoArticleSourceSet.Id_Article;
                assoArticleSourceSet_fromdb.Id_Source = assoArticleSourceSet.Id_Source;

            AssoArticleSourceSetDao.update(assoArticleSourceSet_fromdb);
            
            
                dic.Add("assoArticleSourceSet", assoArticleSourceSet_fromdb);
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
            AssoArticleSourceSet assoArticleSourceSet = AssoArticleSourceSetDao.find(Id);
            ViewBag.assoArticleSourceSet = assoArticleSourceSet;
            return View("~/Views/Ajax/AssoArticleSourceSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            AssoArticleSourceSet obj = AssoArticleSourceSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<AssoArticleSourceSet> list = AssoArticleSourceSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/AssoArticleSourceSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdArticleSet(int IdArticleSet)
        {
            List<AssoArticleSourceSet> list = AssoArticleSourceSetDao.findByIdArticleSet(IdArticleSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdSourceSet(int IdSourceSet)
        {
            List<AssoArticleSourceSet> list = AssoArticleSourceSetDao.findByIdSourceSet(IdSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdSourceSet(int IdArticleSet, int IdSourceSet)
        {
            List<AssoArticleSourceSet> list = AssoArticleSourceSetDao.findByIdArticleSet_IdSourceSet(IdArticleSet, IdSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/AssoArticleSourceSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/AssoArticleSourceSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/AssoArticleSourceSet/Scripts/listScript.cshtml");
        }
    }
}
