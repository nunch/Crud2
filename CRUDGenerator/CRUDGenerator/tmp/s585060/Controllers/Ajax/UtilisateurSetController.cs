using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class UtilisateurSetController : Controller
    {
        
        
        [HttpPost]
        public string create(UtilisateurSet utilisateurSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            UtilisateurSetDao.create(utilisateurSet);
            

                dic.Add("utilisateurSet", utilisateurSet);
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
            return View("~/Views/Ajax/UtilisateurSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = UtilisateurSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(UtilisateurSet utilisateurSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            UtilisateurSet utilisateurSet_fromdb = UtilisateurSetDao.find(utilisateurSet.Id_Utilisateur);
            
                utilisateurSet_fromdb.Nom = utilisateurSet.Nom;
                utilisateurSet_fromdb.Prenom = utilisateurSet.Prenom;
                utilisateurSet_fromdb.Matricule = utilisateurSet.Matricule;
                utilisateurSet_fromdb.Mail = utilisateurSet.Mail;
                utilisateurSet_fromdb.Role = utilisateurSet.Role;
                utilisateurSet_fromdb.IsSuppr = utilisateurSet.IsSuppr;
                utilisateurSet_fromdb.Id_Entite = utilisateurSet.Id_Entite;
                utilisateurSet_fromdb.DateDerniereConnexion = utilisateurSet.DateDerniereConnexion;

            UtilisateurSetDao.update(utilisateurSet_fromdb);
            
            
                dic.Add("utilisateurSet", utilisateurSet_fromdb);
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
            UtilisateurSet utilisateurSet = UtilisateurSetDao.find(Id);
            ViewBag.utilisateurSet = utilisateurSet;
            return View("~/Views/Ajax/UtilisateurSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            UtilisateurSet obj = UtilisateurSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<UtilisateurSet> list = UtilisateurSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/UtilisateurSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdEntiteSet(int IdEntiteSet)
        {
            List<UtilisateurSet> list = UtilisateurSetDao.findByIdEntiteSet(IdEntiteSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/UtilisateurSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/UtilisateurSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/UtilisateurSet/Scripts/listScript.cshtml");
        }
    }
}
