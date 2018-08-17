using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class InfoSourceSetController : Controller
    {
        
        
        [HttpPost]
        public string create(InfoSourceSet infoSourceSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            InfoSourceSetDao.create(infoSourceSet);
            

                dic.Add("infoSourceSet", infoSourceSet);
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
            return View("~/Views/Ajax/InfoSourceSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = InfoSourceSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(InfoSourceSet infoSourceSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            InfoSourceSet infoSourceSet_fromdb = InfoSourceSetDao.find(infoSourceSet.Id_InfoSource);
            
                infoSourceSet_fromdb.Id_Source = infoSourceSet.Id_Source;
                infoSourceSet_fromdb.IsSuppr = infoSourceSet.IsSuppr;
                infoSourceSet_fromdb.SourceFichier = infoSourceSet.SourceFichier;
                infoSourceSet_fromdb.NomSource = infoSourceSet.NomSource;
                infoSourceSet_fromdb.NomSite = infoSourceSet.NomSite;
                infoSourceSet_fromdb.AdresseSite = infoSourceSet.AdresseSite;
                infoSourceSet_fromdb.CodePays = infoSourceSet.CodePays;
                infoSourceSet_fromdb.CodeMDM = infoSourceSet.CodeMDM;
                infoSourceSet_fromdb.Champ1 = infoSourceSet.Champ1;
                infoSourceSet_fromdb.Champ2 = infoSourceSet.Champ2;

            InfoSourceSetDao.update(infoSourceSet_fromdb);
            
            
                dic.Add("infoSourceSet", infoSourceSet_fromdb);
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
            InfoSourceSet infoSourceSet = InfoSourceSetDao.find(Id);
            ViewBag.infoSourceSet = infoSourceSet;
            return View("~/Views/Ajax/InfoSourceSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            InfoSourceSet obj = InfoSourceSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<InfoSourceSet> list = InfoSourceSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/InfoSourceSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdSourceSet(int IdSourceSet)
        {
            List<InfoSourceSet> list = InfoSourceSetDao.findByIdSourceSet(IdSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/InfoSourceSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/InfoSourceSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/InfoSourceSet/Scripts/listScript.cshtml");
        }
    }
}
