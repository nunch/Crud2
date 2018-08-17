using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using CRUDGenerator.Database;
using CRUDGenerator.Templates.Classes.CS;
using CRUDGenerator.Templates.Scripts.SQLServer;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using System.DirectoryServices;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace CRUDGenerator.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult ModifyForm(string databaseName, string tableName, string matricule)
        {
            initUser();
            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db = udb.getDatabaseByName(databaseName);
            Table table = db.getTableByName(tableName);
            ViewBag.database = db;
            ViewBag.table = table;
            return View();
        }

        public ActionResult ModifyFormScript(string databaseName, string tableName, string matricule)
        {
            initUser();
            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db = udb.getDatabaseByName(databaseName);
            Table table = db.getTableByName(tableName);
            ViewBag.database = db;
            ViewBag.table = table;
            return View();
        }

        public ActionResult InsertForm(string databaseName, string tableName, string matricule)
        {
            initUser();
            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db = udb.getDatabaseByName(databaseName);
            Table table = db.getTableByName(tableName);
            ViewBag.database = db;
            ViewBag.table = table;
            return View();
        }

        public ActionResult InsertFormScript(string databaseName, string tableName, string matricule)
        {
            initUser();
            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db = udb.getDatabaseByName(databaseName);
            Table table = db.getTableByName(tableName);
            ViewBag.database = db;
            ViewBag.table = table;
            return View();
        }

        public ActionResult ViewDatas(string databaseName, string tableName, string matricule)
        {
            initUser();
            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db = udb.getDatabaseByName(databaseName);
            Table table = db.getTableByName(tableName);
            ViewBag.database = db;
            ViewBag.table = table;
            return View();
        }

        public ActionResult ViewDatasScript(string databaseName, string tableName, string matricule)
        {
            initUser();
            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db = udb.getDatabaseByName(databaseName);
            Table table = db.getTableByName(tableName);
            ViewBag.database = db;
            ViewBag.table = table;
            return View();
        }

        public ActionResult AjaxTemplate(string databaseName, string tableName, string matricule)
        {
            initUser();
            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db = udb.getDatabaseByName(databaseName);
            ViewBag.db = db;
            return View();
        }

        public User searchLDAPUser(string matricule)
        {
            try
            {
                //DirectoryEntry rootDSE = new DirectoryEntry("LDAP://DC=snm,DC=snecma", "p0ldap11", "11padl0p/1");
                DirectoryEntry rootDSE = new DirectoryEntry();
                DirectorySearcher search = new DirectorySearcher(rootDSE);

                search.PageSize = 1001;// To Pull up more than 100 records.

                search.Filter = "(cn=" + matricule + ")";
                SearchResultCollection result = search.FindAll();
                User u = null;
                foreach (SearchResult item in result)
                {
                    u = new User();
                    u.FirstName = item.Properties["givenname"][0].ToString();
                    u.LastName = item.Properties["sn"][0].ToString();
                    u.Matricule = item.Properties["cn"][0].ToString();
                }
                rootDSE.Dispose();
                return u;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        [HttpPost]
        public ActionResult loadDatabase(string projectName, string host, string instance, string databaseName, string username, string password)
        {
            initUser();

            string matricule = ((User)Session["user"]).Matricule;

            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            string connectionString = "Data Source=" + host + "\\" + instance + ";Initial Catalog=" + databaseName + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
            if (instance == null || instance == "")
            {
                connectionString = "Data Source=" + host + ";Initial Catalog=" + databaseName + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
            }

            if (username == null || username == "")
            {
                if (instance == null || instance == "")
                {
                    connectionString = "Data Source=" + host + ";initial catalog=" + databaseName + ";integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                }
                else
                {
                    connectionString = "Data Source=" + host + "\\" + instance + ";initial catalog=" + databaseName + ";integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                }
            }

            DatabaseXML db = loadDBFromConnectionString(projectName, connectionString, databaseName);
            for (int i = 0; i < udb.Databases.Count; i++)
            {
                if (udb.Databases[i].Name == databaseName)
                {
                    udb.Databases.RemoveAt(i);
                    break;
                }
            }
            udb.Databases.Add(db);
            dao.update(udb);
            return RedirectToAction("index");
        }

        public void initUser()
        {
            if (Session["user"] == null)
            {
                User u = new User
                {
                    FirstName = "null",
                    LastName = "null",
                    Matricule = "iborray"
                };
                Session["user"] = u;
                /*string matricule = User.Identity.Name;
                if (matricule == "")
                    matricule = "iborray";
                else
                {
                    matricule = matricule.Substring(matricule.IndexOf('\\') + 1);
                }
                //User u = searchLDAPUser(matricule);
                User u = null;

                if (u == null)
                {
                    u = new User
                    {
                        FirstName = "null",
                        LastName = "null",
                        Matricule = matricule
                    };
                }*/
            }
        }

        public UserDatabases getDatabases()
        {
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            return udb;
        }

        public void update(Column column, string previousName, string tableName, string databaseName)
        {
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db = udb.getDatabaseByName(databaseName);
            db.getTableByName(tableName).updateColumn(previousName, column, db);
            dao.update(udb);
        }

        public ActionResult Index()
        {
            string str = ConfigurationManager.ConnectionStrings["mssqlConnection"].ConnectionString; ;
            initUser();
            UserDatabases udb = getDatabases();
            ViewBag.databases = udb.Databases;
            string s = "s1\ts2\t\ts3\ts4\t\t\ts6";
            List<string> list = s.Split('\t').ToList();
            return View();
        }

        public ActionResult loadDatabase()
        {
            initUser();
            UserDatabases udb = getDatabases();
            ViewBag.databases = udb.Databases;
            return View();
        }

        public ActionResult addDatabase()
        {
            initUser();
            UserDatabases udb = getDatabases();
            ViewBag.databases = udb.Databases;
            return View();
        }


        public ActionResult removeDatabase()
        {
            initUser();
            UserDatabases udb = getDatabases();
            ViewBag.databases = udb.Databases;
            return View();
        }


        public ActionResult extractDatabase()
        {
            initUser();
            UserDatabases udb = getDatabases();
            ViewBag.databases = udb.Databases;
            return View();
        }


        public ActionResult showDatabase(string databaseName)
        {
            initUser();
            UserDatabases udb = getDatabases();
            ViewBag.databases = udb.Databases;
            ViewBag.database = udb.getDatabaseByName(databaseName);
            return View();
        }


        public ActionResult showTable(string databaseName, string tableName)
        {
            initUser();
            UserDatabases udb = getDatabases();
            ViewBag.databases = udb.Databases;
            ViewBag.database = udb.getDatabaseByName(databaseName);
            ViewBag.table = udb.getDatabaseByName(databaseName).getTableByName(tableName);
            ViewBag.setectedDatabase = databaseName;
            ViewBag.setectedTable = tableName;
            return View();
        }

        public static Dictionary<string, List<Dictionary<string, string>>> listToDic(List<Dictionary<string, string>> list)
        {
            Dictionary<string, List<Dictionary<string, string>>> res = new Dictionary<string, List<Dictionary<string, string>>>();
            foreach (Dictionary<string, string> tmp in list)
            {
                string tableName = tmp["TableName"];
                if (!res.ContainsKey(tableName))
                    res[tableName] = new List<Dictionary<string, string>>();
                res[tableName].Add(tmp);
            }
            return res;
        }

        public void resetDatabase(string dbName)
        {
            initUser();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db = udb.getDatabaseByName(dbName);

            db = loadDBFromConnectionString(dbName, db.ConnectionString, "");
            for (int i = 0; i < udb.Databases.Count; i++)
            {
                if (udb.Databases[i].Name == dbName)
                {
                    udb.Databases.RemoveAt(i);
                    break;
                }
            }
            udb.Databases.Add(db);
            dao.update(udb);
        }

        private DatabaseXML loadDBFromConnectionString(string projectName, string connectionString, string databaseName)
        {
            SQL.HelperSQL.ConnectionString = connectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            DataTable dt = connection.GetSchema("Columns");

            StringWriter stringWriter = new StringWriter();
            dt.WriteXml(stringWriter);
            string xml = stringWriter.ToString();
            XElement element = XElement.Parse(xml);
            DatabaseXML db = new DatabaseXML
            {
                Name = projectName,
                ConnectionString = connectionString
            };
            string getColumnScript = @"
            SELECT [type], TABLE_NAME, COLUMN_NAME, IS_NULLABLE, DATA_TYPE, NUMERIC_PRECISION, CHARACTER_MAXIMUM_LENGTH, NUMERIC_SCALE
            FROM sysobjects as so
	        right join INFORMATION_SCHEMA.COLUMNS on so.name = INFORMATION_SCHEMA.COLUMNS.TABLE_NAME
            WHERE ([type] = 'U' OR [type] = 'V') AND category = 0";
            List<Dictionary<string, string>> columnList = SQL.HelperSQL.query(getColumnScript);
            foreach (Dictionary<string, string> columnDic in columnList)
            {
                string isNull = columnDic["IS_NULLABLE"];
                string tableName = columnDic["TABLE_NAME"];
                string columnName = columnDic["COLUMN_NAME"];
                string Type = columnDic["DATA_TYPE"];
                string NUMERIC_PRECISION = columnDic["NUMERIC_PRECISION"];
                string CHARACTER_MAXIMUM_LENGTH = columnDic["CHARACTER_MAXIMUM_LENGTH"];
                string NUMERIC_SCALE = columnDic["NUMERIC_SCALE"];
                string tableType = columnDic["type"];
                if (tableType.Contains("U"))
                {
                    tableType = "Normal";
                }
                if (tableType.Contains("V"))
                {
                    tableType = "Vue";
                }
                int Length = 0;
                int DecimalLength = 0;
                if (Type == "varchar" || Type == "nvarchar")
                {
                    Length = int.Parse(CHARACTER_MAXIMUM_LENGTH);
                }
                if (Type == "decimal")
                {
                    Length = int.Parse(NUMERIC_PRECISION);
                    DecimalLength = int.Parse(NUMERIC_SCALE);
                }
                Table table = db.getTableByName(tableName);
                if (table == null)
                {
                    table = new Table();
                    table.Name = tableName;
                    table.Type = tableType;
                    db.Content.Add(table);
                }
                Column column = new Column
                {
                    Name = columnName,
                    Length = Length,
                    Type = Database.TypeString.toType(Type),
                    DecimalLength = DecimalLength
                };
                if (isNull == "NO")
                    column.isNull = 0;
                else
                    column.isNull = 1;
                db.getTableByName(tableName).Columns.Add(column);
            }
            string primaryKeys = @"SELECT
    TableName = i1.TABLE_NAME,
    ColumnName = i2.COLUMN_NAME,
	Position = i2.ORDINAL_POSITION,
	PrimaryKeyName = i1.CONSTRAINT_NAME
FROM
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
    ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
WHERE
    i1.CONSTRAINT_TYPE = 'PRIMARY KEY'";

            string foreignKeys = @"SELECT
    TableName = FK.TABLE_NAME,
    ColumnName = CU.COLUMN_NAME,
    ExternTableName = PK.TABLE_NAME,
    ExternColumnName = PT.COLUMN_NAME,
	ForeignKeyName = C.CONSTRAINT_NAME
FROM
    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK
    ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK
    ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU
    ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
INNER JOIN (
            SELECT
                i1.TABLE_NAME,
                i2.COLUMN_NAME
            FROM
                INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
                ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
            WHERE
                i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
           ) PT
    ON PT.TABLE_NAME = PK.TABLE_NAME";

            string identities = @"select ColumnName = COLUMN_NAME, TableName = TABLE_NAME
    from INFORMATION_SCHEMA.COLUMNS
    where COLUMNPROPERTY(object_id(TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1
    order by TableName";

            string indexes = @"SELECT 
     TableName = t.name,
     IndexName = ind.name,
     IndexId = ind.index_id,
     ColumnId = ic.index_column_id,
     ColumnName = col.name
FROM 
     sys.indexes ind 
INNER JOIN 
     sys.index_columns ic ON  ind.object_id = ic.object_id and ind.index_id = ic.index_id 
INNER JOIN 
     sys.columns col ON ic.object_id = col.object_id and ic.column_id = col.column_id 
INNER JOIN 
     sys.tables t ON ind.object_id = t.object_id 
ORDER BY 
     t.name, ind.name, ind.index_id, ic.index_column_id ";

            string uniqueKeys = @"SELECT
    TableName = i1.TABLE_NAME,
    ColumnName = i2.COLUMN_NAME,
	Position = i2.ORDINAL_POSITION,
	UniqueKeyName = i1.CONSTRAINT_NAME
FROM
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
    ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
WHERE
    i1.CONSTRAINT_TYPE = 'UNIQUE'";
            List<Dictionary<string, string>> primaryKeysList = SQL.HelperSQL.query(primaryKeys);
            List<Dictionary<string, string>> foreignKeysList = SQL.HelperSQL.query(foreignKeys);
            List<Dictionary<string, string>> identitiesList = SQL.HelperSQL.query(identities);
            //List<Dictionary<string, string>> indexesList = SQL.HelperSQL.query(indexes);
            List<Dictionary<string, string>> uniqueKeysList = SQL.HelperSQL.query(uniqueKeys);

            //
            Dictionary<string, List<Dictionary<string, string>>> primaryKeyDic = listToDic(primaryKeysList);
            Dictionary<string, List<Dictionary<string, string>>> foreignKeysDic = listToDic(foreignKeysList);
            Dictionary<string, List<Dictionary<string, string>>> identitiesDic = listToDic(identitiesList);
            //Dictionary<string, List<Dictionary<string, string>>> indexesDic = listToDic(indexesList);
            Dictionary<string, List<Dictionary<string, string>>> UniqueKeysDic = listToDic(uniqueKeysList);

            foreach (KeyValuePair<string, List<Dictionary<string, string>>> kvp in primaryKeyDic)
            {
                string tableName = kvp.Key;
                // Rempli pour mettre les index dans les bonnes positions
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    db.getTableByName(tableName).PrimaryKey.Add("youpi");
                }
                foreach (Dictionary<string, string> dic in kvp.Value)
                {
                    int index = Int32.Parse(dic["Position"]) - 1;
                    db.getTableByName(tableName).PrimaryKey[index] = dic["ColumnName"];
                }
            }


            foreach (KeyValuePair<string, List<Dictionary<string, string>>> kvp in foreignKeysDic)
            {
                string tableName = kvp.Key;
                foreach (Dictionary<string, string> dic in kvp.Value)
                {
                    string fkName = dic["ForeignKeyName"];
                    ForeignKey fk = db.getTableByName(tableName).getForeignKeyByName(fkName);
                    ForeignKeyInside fki = new ForeignKeyInside
                    {
                        ColumnName = dic["ColumnName"],
                        ExternColumnName = dic["ExternColumnName"]
                    };
                    if (fk == null)
                    {
                        db.getTableByName(tableName).addForeignKey(new ForeignKey
                        {
                            ExternTableName = dic["ExternTableName"],
                            Name = fkName,
                            Info = new List<ForeignKeyInside>
                            {
                                fki
                            }
                        });
                    }
                    else
                    {
                        db.getTableByName(tableName).getForeignKeyByName(fkName).Info.Add(fki);
                    }
                }
            }


            /*foreach (KeyValuePair<string, List<Dictionary<string, string>>> kvp in indexesDic)
            {
                string tableName = kvp.Key;
                foreach (Dictionary<string, string> dic in kvp.Value)
                {
                    string indexName = dic["IndexName"];
                    Index index = db.getTableByName(tableName).getIndexByName(indexName);
                    string ColumnName = dic["ColumnName"];

                    if (index == null)
                    {
                        db.getTableByName(tableName).Indexes.Add(new Index
                        {
                            Name = indexName,
                            List = new List<string>
                            {
                                ColumnName
                            }
                        });
                    }
                    else
                    {
                        db.getTableByName(tableName).getIndexByName(indexName).List.Add(ColumnName);
                    }
                }
                foreach (Dictionary<string, string> dic in kvp.Value)
                {
                    string indexName = dic["IndexName"];
                    string ColumnName = dic["ColumnName"];
                    int i = Int32.Parse(dic["ColumnId"]) - 1;
                    db.getTableByName(tableName).getIndexByName(indexName).List[i] = ColumnName;
                }
            }*/


            foreach (KeyValuePair<string, List<Dictionary<string, string>>> kvp in UniqueKeysDic)
            {
                string tableName = kvp.Key;
                foreach (Dictionary<string, string> dic in kvp.Value)
                {
                    string ukName = dic["UniqueKeyName"];
                    UniqueKey uk = db.getTableByName(tableName).getUniqueKeyByName(ukName);
                    string ColumnName = dic["ColumnName"];

                    if (uk == null)
                    {
                        db.getTableByName(tableName).UniqueKeys.Add(new UniqueKey
                        {
                            Name = ukName,
                            List = new List<string>
                            {
                                ColumnName
                            }
                        });
                    }
                    else
                    {
                        db.getTableByName(tableName).getUniqueKeyByName(ukName).List.Add(ColumnName);
                    }
                }
                foreach (Dictionary<string, string> dic in kvp.Value)
                {
                    string ukName = dic["UniqueKeyName"];
                    string ColumnName = dic["ColumnName"];
                    int i = Int32.Parse(dic["Position"]) - 1;
                    db.getTableByName(tableName).getUniqueKeyByName(ukName).List[i] = ColumnName;
                }
            }

            foreach (KeyValuePair<string, List<Dictionary<string, string>>> kvp in identitiesDic)
            {
                string tableName = kvp.Key;
                foreach (Dictionary<string, string> dic in kvp.Value)
                {
                    string columnName = dic["ColumnName"];
                    db.getTableByName(tableName).getColumnByName(columnName).Type = Database.Type.Int_AI;
                }
            }
            return db;
        }
    }
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Matricule { get; set; }
    }
}
