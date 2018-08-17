using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUDGenerator.Database;

namespace CRUDGenerator.Controllers
{
    public class UserDatabasesController : Controller
	{
		[HttpPost]
		public ActionResult addDatabase(DatabaseXML database)
		{
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.Databases.Add(database);
			dao.update(udb);
			return Json("");
		}

		[HttpPost]
		public ActionResult removeDatabase(string databaseName)
		{
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.removeDatabaseByName(databaseName);
			dao.update(udb);
			return Json("");
		}
    }
}
