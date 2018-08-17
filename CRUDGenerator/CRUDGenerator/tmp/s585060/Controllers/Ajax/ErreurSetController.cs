using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class ErreurSetController : Controller
    {
        
        
        [HttpPost]
        public string create(ErreurSet erreurSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ErreurSetDao.create(erreurSet);
            

                dic.Add("erreurSet", erreurSet);
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
            return View("~/Views/Ajax/ErreurSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = ErreurSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(ErreurSet erreurSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ErreurSet erreurSet_fromdb = ErreurSetDao.find(erreurSet.Id_Erreur);
            
                erreurSet_fromdb.NumLigne = erreurSet.NumLigne;
                erreurSet_fromdb.DetailsErreur = erreurSet.DetailsErreur;
                erreurSet_fromdb.IsSuppr = erreurSet.IsSuppr;
                erreurSet_fromdb.Id_Fichier = erreurSet.Id_Fichier;

            ErreurSetDao.update(erreurSet_fromdb);
            
            
                dic.Add("erreurSet", erreurSet_fromdb);
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
            ErreurSet erreurSet = ErreurSetDao.find(Id);
            ViewBag.erreurSet = erreurSet;
            return View("~/Views/Ajax/ErreurSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            ErreurSet obj = ErreurSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<ErreurSet> list = ErreurSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/ErreurSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdFichierSet(int IdFichierSet)
        {
            List<ErreurSet> list = ErreurSetDao.findByIdFichierSet(IdFichierSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/ErreurSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/ErreurSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/ErreurSet/Scripts/listScript.cshtml");
        }
    }
}
