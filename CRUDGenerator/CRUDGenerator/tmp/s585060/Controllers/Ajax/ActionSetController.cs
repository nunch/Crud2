using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class ActionSetController : Controller
    {
        
        
        [HttpPost]
        public string create(ActionSet actionSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ActionSetDao.create(actionSet);
            

                dic.Add("actionSet", actionSet);
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
            return View("~/Views/Ajax/ActionSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = ActionSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(ActionSet actionSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ActionSet actionSet_fromdb = ActionSetDao.find(actionSet.Id_Action);
            
                actionSet_fromdb.Priorite = actionSet.Priorite;
                actionSet_fromdb.DateCreation = actionSet.DateCreation;
                actionSet_fromdb.DateRealisation = actionSet.DateRealisation;
                actionSet_fromdb.DateModification = actionSet.DateModification;
                actionSet_fromdb.IsSuppr = actionSet.IsSuppr;
                actionSet_fromdb.Id_Entite = actionSet.Id_Entite;
                actionSet_fromdb.Etat = actionSet.Etat;
                actionSet_fromdb.Titre = actionSet.Titre;
                actionSet_fromdb.Description = actionSet.Description;

            ActionSetDao.update(actionSet_fromdb);
            
            
                dic.Add("actionSet", actionSet_fromdb);
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
            ActionSet actionSet = ActionSetDao.find(Id);
            ViewBag.actionSet = actionSet;
            return View("~/Views/Ajax/ActionSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            ActionSet obj = ActionSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<ActionSet> list = ActionSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/ActionSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdEntiteSet(int IdEntiteSet)
        {
            List<ActionSet> list = ActionSetDao.findByIdEntiteSet(IdEntiteSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/ActionSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/ActionSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/ActionSet/Scripts/listScript.cshtml");
        }
    }
}
