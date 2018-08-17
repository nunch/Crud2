using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CRUDGenerator.Database;
using Newtonsoft.Json;

namespace CRUDGenerator.Controllers
{
    public class TableController : Controller
    {

        [HttpPost]
        public ActionResult changeType(string databaseName, string tableName, string type)
        {
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            udb.getDatabaseByName(databaseName).getTableByName(tableName).Type = type;
            dao.update(udb);
            return Json("");
        }

        [HttpPost]
        public ActionResult removeColumn(string columnName, string tableName, string databaseName)
        {
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            try
            {
                udb.getDatabaseByName(databaseName).getTableByName(tableName).removeColumnByName(columnName);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            dao.update(udb);
            return Json("");
		}

		[HttpPost]
		public ActionResult addColumn(Column column, string IsNull, string columnType, string tableName, string databaseName)
		{
            if (IsNull == "on")
                column.isNull = 1;
            else
                column.isNull = 0;
            if (column.Length == null)
                column.Length = 0;
            column.Type = Database.TypeString.toType(columnType);
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.getDatabaseByName(databaseName).getTableByName(tableName).Columns.Add(column);
			dao.update(udb);
			return Json("");
		}


		[HttpPost]
		public ActionResult addIndex(Index index, string tableName, string databaseName)
		{
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.getDatabaseByName(databaseName).getTableByName(tableName).addIndex(index);
			dao.update(udb);
			return Json("");
		}


		[HttpPost]
		public ActionResult addForeignKey(ForeignKey fk, List<string> ColumnName, List<string> ExternColumnName, string tableName, string databaseName)
		{
            for (int i = 0; i < ColumnName.Count; i++)
            {
                fk.Info.Add(new ForeignKeyInside
                {
                    ColumnName = ColumnName[i],
                    ExternColumnName = ExternColumnName[i]
                });
            }
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.getDatabaseByName(databaseName).getTableByName(tableName).addForeignKey(fk);
			dao.update(udb);
			return Json("");
		}


		[HttpPost]
		public ActionResult addUniqueKey(UniqueKey uk, string tableName, string databaseName)
		{
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.getDatabaseByName(databaseName).getTableByName(tableName).addUniqueKey(uk);
			dao.update(udb);
			return Json("");
		}


		[HttpPost]
		public ActionResult updateIndex(Index index, string tableName, string databaseName)
		{
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.getDatabaseByName(databaseName).getTableByName(tableName).updateIndex(index);
			dao.update(udb);
			return Json("");
		}


		[HttpPost]
		public ActionResult updateForeignKey(ForeignKey fk, List<string> ColumnName, List<string> ExternColumnName, string tableName, string databaseName)
		{
            for (int i = 0; i < ColumnName.Count; i++)
            {
                fk.Info.Add(new ForeignKeyInside
                {
                    ColumnName = ColumnName[i],
                    ExternColumnName = ExternColumnName[i]
                });
            }
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.getDatabaseByName(databaseName).getTableByName(tableName).updateForeignKey(fk);
			dao.update(udb);
			return Json("");
		}


		[HttpPost]
		public ActionResult updateUniqueKey(UniqueKey uk, string tableName, string databaseName)
		{
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.getDatabaseByName(databaseName).getTableByName(tableName).updateUniqueKey(uk);
			dao.update(udb);
			return Json("");
		}


		[HttpPost]
		public ActionResult removeIndex(string indexName, string tableName, string databaseName)
		{
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.getDatabaseByName(databaseName).getTableByName(tableName).removeIndex(indexName);
			dao.update(udb);
			return Json("");
		}


		[HttpPost]
		public ActionResult removeForeignKey(string fkName, string tableName, string databaseName)
		{
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.getDatabaseByName(databaseName).getTableByName(tableName).removeForeignKey(fkName);
			dao.update(udb);
			return Json("");
		}


		[HttpPost]
		public ActionResult removeUniqueKey(string ukName, string tableName, string databaseName)
		{
			UserDatabasesDAO dao = new UserDatabasesDAO();
			string matricule = ((User)Session["user"]).Matricule;
			UserDatabases udb = dao.findByMatricule(matricule);
			udb.getDatabaseByName(databaseName).getTableByName(tableName).removeUniqueKey(ukName);
			dao.update(udb);
			return Json("");
		}

        [HttpPost]
        public ActionResult removePrimaryKey(string tableName, string databaseName)
        {
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            udb.getDatabaseByName(databaseName).getTableByName(tableName).PrimaryKey.Clear();
            dao.update(udb);
            return Json("");
        }

        [HttpPost]
        public ActionResult addPrimaryKey(List<string> columnNames, string tableName, string databaseName)
        {
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            udb.getDatabaseByName(databaseName).getTableByName(tableName).PrimaryKey = columnNames;
            dao.update(udb);
            return Json("");
        }

        public string getTableColumns(string tableName, string databaseName)
        {
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            List<Dictionary<string, dynamic>> list = new List<Dictionary<string, dynamic>>();
            DatabaseXML db = udb.getDatabaseByName(databaseName);
            Table table = db.getTableByName(tableName);
            foreach (Column column in table.Columns)
            {
                Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
                dic.Add("Name", column.Name);
                dic.Add("Type", TypeString.ToString(column.Type));
                dic.Add("Length", column.getLength());
                dic.Add("PK", table.PrimaryKey.IndexOf(column.Name));
                List<ForeignKey> listFK = new List<ForeignKey>();
                foreach (ForeignKey fk in table.ForeignKeys)
                {
                    foreach (ForeignKeyInside fki in fk.Info)
                    {
                        if (fki.ColumnName == column.Name)
                            listFK.Add(fk);
                    }
                }
                dic.Add("FK", listFK);
                list.Add(dic);
            }
            return JsonConvert.SerializeObject(list);
        }

        public string getTableForeignKeys(string tableName, string databaseName)
        {
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            List<Dictionary<string, dynamic>> list = new List<Dictionary<string, dynamic>>();
            DatabaseXML db = udb.getDatabaseByName(databaseName);
            Table table = db.getTableByName(tableName);
            return JsonConvert.SerializeObject(table.ForeignKeys);
        }
    }
}
