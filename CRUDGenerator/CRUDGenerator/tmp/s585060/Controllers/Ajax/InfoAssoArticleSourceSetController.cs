using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class InfoAssoArticleSourceSetController : Controller
    {
        
        
        [HttpPost]
        public string create(InfoAssoArticleSourceSet infoAssoArticleSourceSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            InfoAssoArticleSourceSetDao.create(infoAssoArticleSourceSet);
            

                dic.Add("infoAssoArticleSourceSet", infoAssoArticleSourceSet);
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
            return View("~/Views/Ajax/InfoAssoArticleSourceSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = InfoAssoArticleSourceSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(InfoAssoArticleSourceSet infoAssoArticleSourceSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            InfoAssoArticleSourceSet infoAssoArticleSourceSet_fromdb = InfoAssoArticleSourceSetDao.find(infoAssoArticleSourceSet.Id_InfoAssoArticleSource);
            
                infoAssoArticleSourceSet_fromdb.Id_AssoArticleSource = infoAssoArticleSourceSet.Id_AssoArticleSource;
                infoAssoArticleSourceSet_fromdb.SourceFichier = infoAssoArticleSourceSet.SourceFichier;
                infoAssoArticleSourceSet_fromdb.IsSuppr = infoAssoArticleSourceSet.IsSuppr;
                infoAssoArticleSourceSet_fromdb.PartMarche = infoAssoArticleSourceSet.PartMarche;
                infoAssoArticleSourceSet_fromdb.PNSuffixe = infoAssoArticleSourceSet.PNSuffixe;

            InfoAssoArticleSourceSetDao.update(infoAssoArticleSourceSet_fromdb);
            
            
                dic.Add("infoAssoArticleSourceSet", infoAssoArticleSourceSet_fromdb);
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
            InfoAssoArticleSourceSet infoAssoArticleSourceSet = InfoAssoArticleSourceSetDao.find(Id);
            ViewBag.infoAssoArticleSourceSet = infoAssoArticleSourceSet;
            return View("~/Views/Ajax/InfoAssoArticleSourceSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            InfoAssoArticleSourceSet obj = InfoAssoArticleSourceSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<InfoAssoArticleSourceSet> list = InfoAssoArticleSourceSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/InfoAssoArticleSourceSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdAssoArticleSourceSet(int IdAssoArticleSourceSet)
        {
            List<InfoAssoArticleSourceSet> list = InfoAssoArticleSourceSetDao.findByIdAssoArticleSourceSet(IdAssoArticleSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/InfoAssoArticleSourceSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/InfoAssoArticleSourceSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/InfoAssoArticleSourceSet/Scripts/listScript.cshtml");
        }
    }
}
