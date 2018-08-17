using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class ConditionSetController : Controller
    {
        
        
        [HttpPost]
        public string create(ConditionSet conditionSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ConditionSetDao.create(conditionSet);
            

                dic.Add("conditionSet", conditionSet);
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
            return View("~/Views/Ajax/ConditionSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = ConditionSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(ConditionSet conditionSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ConditionSet conditionSet_fromdb = ConditionSetDao.find(conditionSet.Id_Condition);
            
                conditionSet_fromdb.Champ = conditionSet.Champ;
                conditionSet_fromdb.Operateur = conditionSet.Operateur;
                conditionSet_fromdb.Valeur = conditionSet.Valeur;
                conditionSet_fromdb.IsSuppr = conditionSet.IsSuppr;
                conditionSet_fromdb.Id_ExceptionStatut = conditionSet.Id_ExceptionStatut;

            ConditionSetDao.update(conditionSet_fromdb);
            
            
                dic.Add("conditionSet", conditionSet_fromdb);
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
            ConditionSet conditionSet = ConditionSetDao.find(Id);
            ViewBag.conditionSet = conditionSet;
            return View("~/Views/Ajax/ConditionSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            ConditionSet obj = ConditionSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<ConditionSet> list = ConditionSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/ConditionSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdExceptionStatutSet(int IdExceptionStatutSet)
        {
            List<ConditionSet> list = ConditionSetDao.findByIdExceptionStatutSet(IdExceptionStatutSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/ConditionSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/ConditionSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/ConditionSet/Scripts/listScript.cshtml");
        }
    }
}
