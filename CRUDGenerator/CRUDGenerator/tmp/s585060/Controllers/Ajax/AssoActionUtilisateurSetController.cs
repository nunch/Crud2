using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class AssoActionUtilisateurSetController : Controller
    {
        
        
        [HttpPost]
        public string create(AssoActionUtilisateurSet assoActionUtilisateurSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            AssoActionUtilisateurSetDao.create(assoActionUtilisateurSet);
            

                dic.Add("assoActionUtilisateurSet", assoActionUtilisateurSet);
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
            return View("~/Views/Ajax/AssoActionUtilisateurSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = AssoActionUtilisateurSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(AssoActionUtilisateurSet assoActionUtilisateurSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            AssoActionUtilisateurSet assoActionUtilisateurSet_fromdb = AssoActionUtilisateurSetDao.find(assoActionUtilisateurSet.Id_AssoActionUtilisateur);
            
                assoActionUtilisateurSet_fromdb.Type = assoActionUtilisateurSet.Type;
                assoActionUtilisateurSet_fromdb.IsSuppr = assoActionUtilisateurSet.IsSuppr;
                assoActionUtilisateurSet_fromdb.Id_Action = assoActionUtilisateurSet.Id_Action;
                assoActionUtilisateurSet_fromdb.Id_Utilisateur = assoActionUtilisateurSet.Id_Utilisateur;

            AssoActionUtilisateurSetDao.update(assoActionUtilisateurSet_fromdb);
            
            
                dic.Add("assoActionUtilisateurSet", assoActionUtilisateurSet_fromdb);
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
            AssoActionUtilisateurSet assoActionUtilisateurSet = AssoActionUtilisateurSetDao.find(Id);
            ViewBag.assoActionUtilisateurSet = assoActionUtilisateurSet;
            return View("~/Views/Ajax/AssoActionUtilisateurSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            AssoActionUtilisateurSet obj = AssoActionUtilisateurSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<AssoActionUtilisateurSet> list = AssoActionUtilisateurSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/AssoActionUtilisateurSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdActionSet(int IdActionSet)
        {
            List<AssoActionUtilisateurSet> list = AssoActionUtilisateurSetDao.findByIdActionSet(IdActionSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdUtilisateurSet(int IdUtilisateurSet)
        {
            List<AssoActionUtilisateurSet> list = AssoActionUtilisateurSetDao.findByIdUtilisateurSet(IdUtilisateurSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdUtilisateurSet(int IdActionSet, int IdUtilisateurSet)
        {
            List<AssoActionUtilisateurSet> list = AssoActionUtilisateurSetDao.findByIdActionSet_IdUtilisateurSet(IdActionSet, IdUtilisateurSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/AssoActionUtilisateurSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/AssoActionUtilisateurSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/AssoActionUtilisateurSet/Scripts/listScript.cshtml");
        }
    }
}
