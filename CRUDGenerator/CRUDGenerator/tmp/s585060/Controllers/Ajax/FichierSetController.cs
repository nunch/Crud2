using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class FichierSetController : Controller
    {
        
        
        [HttpPost]
        public string create(FichierSet fichierSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            FichierSetDao.create(fichierSet);
            

                dic.Add("fichierSet", fichierSet);
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
            return View("~/Views/Ajax/FichierSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = FichierSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(FichierSet fichierSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            FichierSet fichierSet_fromdb = FichierSetDao.find(fichierSet.Id_Fichier);
            
                fichierSet_fromdb.NomFichier = fichierSet.NomFichier;
                fichierSet_fromdb.NbSucces = fichierSet.NbSucces;
                fichierSet_fromdb.NbErreur = fichierSet.NbErreur;
                fichierSet_fromdb.IsSuppr = fichierSet.IsSuppr;
                fichierSet_fromdb.Id_RapportIntegration = fichierSet.Id_RapportIntegration;

            FichierSetDao.update(fichierSet_fromdb);
            
            
                dic.Add("fichierSet", fichierSet_fromdb);
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
            FichierSet fichierSet = FichierSetDao.find(Id);
            ViewBag.fichierSet = fichierSet;
            return View("~/Views/Ajax/FichierSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            FichierSet obj = FichierSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<FichierSet> list = FichierSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/FichierSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdRapportIntegrationSet(int IdRapportIntegrationSet)
        {
            List<FichierSet> list = FichierSetDao.findByIdRapportIntegrationSet(IdRapportIntegrationSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/FichierSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/FichierSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/FichierSet/Scripts/listScript.cshtml");
        }
    }
}
