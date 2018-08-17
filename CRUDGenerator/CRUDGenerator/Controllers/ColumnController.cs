using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml.Serialization;
using CRUDGenerator.Database;

namespace CRUDGenerator.Controllers
{
    public class ColumnController : Controller
    {

        [HttpPost]
        public ActionResult update(Column column, string previousName, string tableName, string databaseName)
        {
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db =  udb.getDatabaseByName(databaseName);
            db.getTableByName(tableName).updateColumn(previousName, column, db);
            dao.update(udb);
            return Json("");
        }

    }
}
