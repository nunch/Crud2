using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PerennisationSPI.Models;
using PerennisationSPI.Dao;

namespace PerennisationSPI.Controllers.Ajax
{
    public class AssoCleActionSetController : Controller
    {
        
        
        [HttpPost]
        public string create(AssoCleActionSet assoCleActionSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            AssoCleActionSetDao.create(assoCleActionSet);
            

                dic.Add("assoCleActionSet", assoCleActionSet);
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
            return View("~/Views/Ajax/AssoCleActionSet/create.cshtml");
        }

        [HttpPost]
        public ActionResult remove(int Id)
        {
            string error = AssoCleActionSetDao.remove(Id);
            return Json(error);
        }

        [HttpPost]
        public string update(AssoCleActionSet assoCleActionSet)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            try
            {
            AssoCleActionSet assoCleActionSet_fromdb = AssoCleActionSetDao.find(assoCleActionSet.Id_AssoCleAction);
            
                assoCleActionSet_fromdb.IsSuppr = assoCleActionSet.IsSuppr;
                assoCleActionSet_fromdb.Id_Action = assoCleActionSet.Id_Action;
                assoCleActionSet_fromdb.Id_Cle = assoCleActionSet.Id_Cle;
                assoCleActionSet_fromdb.NumAxe = assoCleActionSet.NumAxe;
                assoCleActionSet_fromdb.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles = assoCleActionSet.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles;
                assoCleActionSet_fromdb.AssoCleActionArticle_AssoCleAction_Id_Article = assoCleActionSet.AssoCleActionArticle_AssoCleAction_Id_Article;
                assoCleActionSet_fromdb.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource = assoCleActionSet.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource;
                assoCleActionSet_fromdb.AssoCleActionSource_AssoCleAction_Id_Source = assoCleActionSet.AssoCleActionSource_AssoCleAction_Id_Source;

            AssoCleActionSetDao.update(assoCleActionSet_fromdb);
            
            
                dic.Add("assoCleActionSet", assoCleActionSet_fromdb);
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
            AssoCleActionSet assoCleActionSet = AssoCleActionSetDao.find(Id);
            ViewBag.assoCleActionSet = assoCleActionSet;
            return View("~/Views/Ajax/AssoCleActionSet/modify.cshtml");
        }

        [HttpPost]
        public string find(int Id)
        {
            AssoCleActionSet obj = AssoCleActionSetDao.find(Id);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public string findAll()
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findAll();
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult list()
        {
            return View("~/Views/Ajax/AssoCleActionSet/list.cshtml");
        }

        [HttpPost]
        public string findByIdActionSet(int IdActionSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet(IdActionSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet(int IdArticleSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdArticleSet(IdArticleSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdArticleSet(int IdActionSet, int IdArticleSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdArticleSet(IdActionSet, IdArticleSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdAssoArticleSourceSet(int IdAssoArticleSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdAssoArticleSourceSet(IdAssoArticleSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdAssoArticleSourceSet(int IdActionSet, int IdAssoArticleSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdAssoArticleSourceSet(IdActionSet, IdAssoArticleSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdAssoArticleSourceSet(int IdArticleSet, int IdAssoArticleSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdArticleSet_IdAssoArticleSourceSet(IdArticleSet, IdAssoArticleSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet(int IdActionSet, int IdArticleSet, int IdAssoArticleSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet(IdActionSet, IdArticleSet, IdAssoArticleSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdSourceSet(int IdSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdSourceSet(IdSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdSourceSet(int IdActionSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdSourceSet(IdActionSet, IdSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdSourceSet(int IdArticleSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdArticleSet_IdSourceSet(IdArticleSet, IdSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdArticleSet_IdSourceSet(int IdActionSet, int IdArticleSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdArticleSet_IdSourceSet(IdActionSet, IdArticleSet, IdSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdAssoArticleSourceSet_IdSourceSet(int IdAssoArticleSourceSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdAssoArticleSourceSet_IdSourceSet(IdAssoArticleSourceSet, IdSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdAssoArticleSourceSet_IdSourceSet(int IdActionSet, int IdAssoArticleSourceSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdAssoArticleSourceSet_IdSourceSet(IdActionSet, IdAssoArticleSourceSet, IdSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdAssoArticleSourceSet_IdSourceSet(int IdArticleSet, int IdAssoArticleSourceSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdArticleSet_IdAssoArticleSourceSet_IdSourceSet(IdArticleSet, IdAssoArticleSourceSet, IdSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet_IdSourceSet(int IdActionSet, int IdArticleSet, int IdAssoArticleSourceSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet_IdSourceSet(IdActionSet, IdArticleSet, IdAssoArticleSourceSet, IdSourceSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdLienArticlesSet(int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdLienArticlesSet(IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdLienArticlesSet(int IdActionSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdLienArticlesSet(IdActionSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdLienArticlesSet(int IdArticleSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdArticleSet_IdLienArticlesSet(IdArticleSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdArticleSet_IdLienArticlesSet(int IdActionSet, int IdArticleSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdArticleSet_IdLienArticlesSet(IdActionSet, IdArticleSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdAssoArticleSourceSet_IdLienArticlesSet(int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdAssoArticleSourceSet_IdLienArticlesSet(IdAssoArticleSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdAssoArticleSourceSet_IdLienArticlesSet(int IdActionSet, int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdAssoArticleSourceSet_IdLienArticlesSet(IdActionSet, IdAssoArticleSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdAssoArticleSourceSet_IdLienArticlesSet(int IdArticleSet, int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdArticleSet_IdAssoArticleSourceSet_IdLienArticlesSet(IdArticleSet, IdAssoArticleSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet_IdLienArticlesSet(int IdActionSet, int IdArticleSet, int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet_IdLienArticlesSet(IdActionSet, IdArticleSet, IdAssoArticleSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdSourceSet_IdLienArticlesSet(int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdSourceSet_IdLienArticlesSet(IdSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdSourceSet_IdLienArticlesSet(int IdActionSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdSourceSet_IdLienArticlesSet(IdActionSet, IdSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdSourceSet_IdLienArticlesSet(int IdArticleSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdArticleSet_IdSourceSet_IdLienArticlesSet(IdArticleSet, IdSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdArticleSet_IdSourceSet_IdLienArticlesSet(int IdActionSet, int IdArticleSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdArticleSet_IdSourceSet_IdLienArticlesSet(IdActionSet, IdArticleSet, IdSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(int IdAssoArticleSourceSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(IdAssoArticleSourceSet, IdSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(int IdActionSet, int IdAssoArticleSourceSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(IdActionSet, IdAssoArticleSourceSet, IdSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdArticleSet_IdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(int IdArticleSet, int IdAssoArticleSourceSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdArticleSet_IdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(IdArticleSet, IdAssoArticleSourceSet, IdSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public string findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(int IdActionSet, int IdArticleSet, int IdAssoArticleSourceSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = AssoCleActionSetDao.findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(IdActionSet, IdArticleSet, IdAssoArticleSourceSet, IdSourceSet, IdLienArticlesSet);
            return JsonConvert.SerializeObject(list);
        }

        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View("~/Views/Ajax/AssoCleActionSet/Scripts/createScript.cshtml");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View("~/Views/Ajax/AssoCleActionSet/Scripts/modifyScript.cshtml");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View("~/Views/Ajax/AssoCleActionSet/Scripts/listScript.cshtml");
        }
    }
}
