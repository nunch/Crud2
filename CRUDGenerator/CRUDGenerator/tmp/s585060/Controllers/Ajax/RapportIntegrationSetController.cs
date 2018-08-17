using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class RapportIntegrationSetController : Controller
    {
        
        
        [HttpPost]
        public string create(RapportIntegrationSet rapportIntegrationSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            RapportIntegrationSetDao.create(rapportIntegrationSet);
            

                dic.Add("rapportIntegrationSet", rapportIntegrationSet);
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
            return View("~/Views/Ajax/RapportIntegrationSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = RapportIntegrationSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(RapportIntegrationSet rapportIntegrationSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            RapportIntegrationSet rapportIntegrationSet_fromdb = RapportIntegrationSetDao.find(rapportIntegrationSet.Id_RapportIntegration);
            
                rapportIntegrationSet_fromdb.NbFichiers = rapportIntegrationSet.NbFichiers;
                rapportIntegrationSet_fromdb.NbTotalSucces = rapportIntegrationSet.NbTotalSucces;
                rapportIntegrationSet_fromdb.NbTotalErreur = rapportIntegrationSet.NbTotalErreur;
                rapportIntegrationSet_fromdb.DateImport = rapportIntegrationSet.DateImport;
                rapportIntegrationSet_fromdb.IsSuppr = rapportIntegrationSet.IsSuppr;

            RapportIntegrationSetDao.update(rapportIntegrationSet_fromdb);
            
            
                dic.Add("rapportIntegrationSet", rapportIntegrationSet_fromdb);
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
            RapportIntegrationSet rapportIntegrationSet = RapportIntegrationSetDao.find(Id);
            ViewBag.rapportIntegrationSet = rapportIntegrationSet;
            return View("~/Views/Ajax/RapportIntegrationSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            RapportIntegrationSet obj = RapportIntegrationSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<RapportIntegrationSet> list = RapportIntegrationSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/RapportIntegrationSet/list.cshtml");
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/RapportIntegrationSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/RapportIntegrationSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/RapportIntegrationSet/Scripts/listScript.cshtml");
        }
    }
}
