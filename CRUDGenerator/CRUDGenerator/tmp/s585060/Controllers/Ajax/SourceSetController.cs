using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class SourceSetController : Controller
    {
        
        
        [HttpPost]
        public string create(SourceSet sourceSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            SourceSetDao.create(sourceSet);
            

                dic.Add("sourceSet", sourceSet);
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
            return View("~/Views/Ajax/SourceSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = SourceSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(SourceSet sourceSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            SourceSet sourceSet_fromdb = SourceSetDao.find(sourceSet.Id_Source);
            
                sourceSet_fromdb.CodeSource = sourceSet.CodeSource;
                sourceSet_fromdb.CodeSite = sourceSet.CodeSite;
                sourceSet_fromdb.IsSuppr = sourceSet.IsSuppr;
                sourceSet_fromdb.EcartSource_Source_Id_Ecart = sourceSet.EcartSource_Source_Id_Ecart;

            SourceSetDao.update(sourceSet_fromdb);
            
            
                dic.Add("sourceSet", sourceSet_fromdb);
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
            SourceSet sourceSet = SourceSetDao.find(Id);
            ViewBag.sourceSet = sourceSet;
            return View("~/Views/Ajax/SourceSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            SourceSet obj = SourceSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<SourceSet> list = SourceSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/SourceSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdEcartSet(int IdEcartSet)
        {
            List<SourceSet> list = SourceSetDao.findByIdEcartSet(IdEcartSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/SourceSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/SourceSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/SourceSet/Scripts/listScript.cshtml");
        }
    }
}
