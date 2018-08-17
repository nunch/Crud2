using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class IgnorerSetController : Controller
    {
        
        
        [HttpPost]
        public string create(IgnorerSet ignorerSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            IgnorerSetDao.create(ignorerSet);
            

                dic.Add("ignorerSet", ignorerSet);
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
            return View("~/Views/Ajax/IgnorerSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = IgnorerSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(IgnorerSet ignorerSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            IgnorerSet ignorerSet_fromdb = IgnorerSetDao.find(ignorerSet.Id_Ignorer);
            
                ignorerSet_fromdb.IsIgnored = ignorerSet.IsIgnored;
                ignorerSet_fromdb.IsSuppr = ignorerSet.IsSuppr;
                ignorerSet_fromdb.Id_Ecart = ignorerSet.Id_Ecart;

            IgnorerSetDao.update(ignorerSet_fromdb);
            
            
                dic.Add("ignorerSet", ignorerSet_fromdb);
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
            IgnorerSet ignorerSet = IgnorerSetDao.find(Id);
            ViewBag.ignorerSet = ignorerSet;
            return View("~/Views/Ajax/IgnorerSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            IgnorerSet obj = IgnorerSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<IgnorerSet> list = IgnorerSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/IgnorerSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdEcartSet(int IdEcartSet)
        {
            List<IgnorerSet> list = IgnorerSetDao.findByIdEcartSet(IdEcartSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/IgnorerSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/IgnorerSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/IgnorerSet/Scripts/listScript.cshtml");
        }
    }
}
