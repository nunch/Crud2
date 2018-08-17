using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class ExceptionStatutSetController : Controller
    {
        
        
        [HttpPost]
        public string create(ExceptionStatutSet exceptionStatutSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ExceptionStatutSetDao.create(exceptionStatutSet);
            

                dic.Add("exceptionStatutSet", exceptionStatutSet);
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
            return View("~/Views/Ajax/ExceptionStatutSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = ExceptionStatutSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(ExceptionStatutSet exceptionStatutSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ExceptionStatutSet exceptionStatutSet_fromdb = ExceptionStatutSetDao.find(exceptionStatutSet.Id_ExceptionStatut);
            
                exceptionStatutSet_fromdb.Titre = exceptionStatutSet.Titre;
                exceptionStatutSet_fromdb.AxeAnalyse = exceptionStatutSet.AxeAnalyse;
                exceptionStatutSet_fromdb.Source = exceptionStatutSet.Source;
                exceptionStatutSet_fromdb.IsSuppr = exceptionStatutSet.IsSuppr;
                exceptionStatutSet_fromdb.Description = exceptionStatutSet.Description;

            ExceptionStatutSetDao.update(exceptionStatutSet_fromdb);
            
            
                dic.Add("exceptionStatutSet", exceptionStatutSet_fromdb);
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
            ExceptionStatutSet exceptionStatutSet = ExceptionStatutSetDao.find(Id);
            ViewBag.exceptionStatutSet = exceptionStatutSet;
            return View("~/Views/Ajax/ExceptionStatutSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            ExceptionStatutSet obj = ExceptionStatutSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<ExceptionStatutSet> list = ExceptionStatutSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/ExceptionStatutSet/list.cshtml");
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/ExceptionStatutSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/ExceptionStatutSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/ExceptionStatutSet/Scripts/listScript.cshtml");
        }
    }
}
