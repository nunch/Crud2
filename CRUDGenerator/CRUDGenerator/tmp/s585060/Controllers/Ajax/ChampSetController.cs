using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class ChampSetController : Controller
    {
        
        
        [HttpPost]
        public string create(ChampSet champSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ChampSetDao.create(champSet);
            

                dic.Add("champSet", champSet);
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
            return View("~/Views/Ajax/ChampSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = ChampSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(ChampSet champSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            ChampSet champSet_fromdb = ChampSetDao.find(champSet.Id_Champ);
            
                champSet_fromdb.Nom = champSet.Nom;

            ChampSetDao.update(champSet_fromdb);
            
            
                dic.Add("champSet", champSet_fromdb);
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
            ChampSet champSet = ChampSetDao.find(Id);
            ViewBag.champSet = champSet;
            return View("~/Views/Ajax/ChampSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            ChampSet obj = ChampSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<ChampSet> list = ChampSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/ChampSet/list.cshtml");
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/ChampSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/ChampSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/ChampSet/Scripts/listScript.cshtml");
        }
    }
}
