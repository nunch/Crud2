using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class EntiteSetController : Controller
    {
        
        
        [HttpPost]
        public string create(EntiteSet entiteSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            EntiteSetDao.create(entiteSet);
            

                dic.Add("entiteSet", entiteSet);
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
            return View("~/Views/Ajax/EntiteSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = EntiteSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(EntiteSet entiteSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            EntiteSet entiteSet_fromdb = EntiteSetDao.find(entiteSet.Id_Entite);
            
                entiteSet_fromdb.IsSuppr = entiteSet.IsSuppr;
                entiteSet_fromdb.Nom = entiteSet.Nom;
                entiteSet_fromdb.Description = entiteSet.Description;

            EntiteSetDao.update(entiteSet_fromdb);
            
            
                dic.Add("entiteSet", entiteSet_fromdb);
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
            EntiteSet entiteSet = EntiteSetDao.find(Id);
            ViewBag.entiteSet = entiteSet;
            return View("~/Views/Ajax/EntiteSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            EntiteSet obj = EntiteSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<EntiteSet> list = EntiteSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/EntiteSet/list.cshtml");
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/EntiteSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/EntiteSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/EntiteSet/Scripts/listScript.cshtml");
        }
    }
}
