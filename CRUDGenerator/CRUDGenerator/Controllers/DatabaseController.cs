using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using System.IO.Compression;
using System.DirectoryServices;
using CRUDGenerator.Database;
using System.Net;
using System.Text;

namespace CRUDGenerator.Controllers
{
    public class DatabaseController : Controller
    {

        public User searchLDAPUser(string matricule)
        {
            try
            {
                //DirectoryEntry rootDSE = new DirectoryEntry("LDAP://DC=snm,DC=snecma", "p0ldap11", "11padl0p/1");
                DirectoryEntry rootDSE = new DirectoryEntry();
                DirectorySearcher search = new DirectorySearcher(rootDSE);

                search.PageSize = 1001;// To Pull up more than 100 records.

                search.Filter = "(cn=" + matricule + ")"; SearchResultCollection result = search.FindAll();
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
                Session["user"] = u;/*
                string matricule = User.Identity.Name;
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
                }
                Session["user"] = u;*/
            }
        }
        [HttpPost]
        public ActionResult getTableByName(string tableName, string databaseName)
        {
            initUser();
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            Table table = udb.getDatabaseByName(databaseName).getTableByName(tableName);
            string json = new JavaScriptSerializer().Serialize(table);
            return Json(json);
        }

        [HttpPost]
        public ActionResult removeTable(string tableName, string databaseName)
        {
            initUser();
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            try
            {
                udb.getDatabaseByName(databaseName).removeTableByName(tableName);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            dao.update(udb);
            return Json("");
        }
        
        [HttpPost]
        public ActionResult addTable(string tableName, string databaseName, string type)
        {
            initUser();
            Table table = new Table();
            if(type != null)
            {
                table.Type = type;
            }
            table.Name = tableName;
            Column pk = new Column
            {
                DecimalLength = 0,
                isNull = 0,
                Length = 0,
                Name = "Id_" + tableName,
                Type = Database.Type.Int_AI
            };
            Column isSuppr = new Column
            {
                DecimalLength = 0,
                isNull = 0,
                Length = 0,
                Name = "IsSuppr",
                Type = Database.Type.Boolean
            };
            table.Columns.Add(pk);
            table.Columns.Add(isSuppr);
            table.PrimaryKey.Add("Id_" + tableName);
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            udb.getDatabaseByName(databaseName).Content.Add(table);
            dao.update(udb);
            /*table.ForeignKeys;
            table.Indexes;
            table.PrimaryKey;
            table.UniqueKeys;*/
            return Json("");
        }

        public ActionResult addForeignKey(ForeignKey fk, string tableName, string databaseName)
        {

            initUser();
            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            udb.getDatabaseByName(databaseName).getTableByName(tableName).ForeignKeys.Add(fk);
            dao.update(udb);
            return Json("");
        }



        public ActionResult removeForeignKey(ForeignKey fk, string tableName, string databaseName)
        {
            initUser();

            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            udb.getDatabaseByName(databaseName).getTableByName(tableName).ForeignKeys.Remove(fk);
            dao.update(udb);
            return Json("");
        }

        public ActionResult addIndex(Index index, string tableName, string databaseName)
        {
            initUser();

            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            udb.getDatabaseByName(databaseName).getTableByName(tableName).Indexes.Add(index);
            dao.update(udb);
            return Json("");
        }

		public ActionResult removeIndex(Index index, string tableName, string databaseName)
        {
            initUser();

            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            udb.getDatabaseByName(databaseName).getTableByName(tableName).Indexes.Remove(index);
            dao.update(udb);
            return Json("");
        }

		public ActionResult addUniqueKey(UniqueKey uniqueKey, string tableName, string databaseName)
        {
            initUser();

            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            udb.getDatabaseByName(databaseName).getTableByName(tableName).UniqueKeys.Add(uniqueKey);
            dao.update(udb);
            return Json("");
        }

		public ActionResult removeUniqueKey(UniqueKey uniqueKey, string tableName, string databaseName)
        {
            initUser();

            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            udb.getDatabaseByName(databaseName).getTableByName(tableName).UniqueKeys.Remove(uniqueKey);
            dao.update(udb);
            return Json("");
        }

        public ActionResult setPrimaryKey(List<string> primaryKey, string tableName, string databaseName)
        {
            initUser();

            UserDatabasesDAO dao = new UserDatabasesDAO();
            string matricule = ((User)Session["user"]).Matricule;
            UserDatabases udb = dao.findByMatricule(matricule);
            udb.getDatabaseByName(databaseName).getTableByName(tableName).PrimaryKey = primaryKey;
            dao.update(udb);
            return Json("");
        }

        [HttpPost]
        public ActionResult extractCode(string databaseName, string codeType)
        {
            initUser();

            string matricule = ((User)Session["user"]).Matricule;
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "tmp"+Path.DirectorySeparatorChar + matricule + ""+Path.DirectorySeparatorChar;
            string zipPath = System.AppDomain.CurrentDomain.BaseDirectory + "tmp"+Path.DirectorySeparatorChar + matricule + ".zip";
            generateCode(databaseName, codeType, path, zipPath, matricule);
            ZipFile.CreateFromDirectory(path, zipPath);
			Response.Redirect ("/tmp/" + matricule + ".zip");
            return Json("tmp/" + matricule + ".zip");
        }

        [HttpPost]
        public ActionResult extractScript(string databaseName, string scriptType)
        {
            initUser();

            string matricule = ((User)Session["user"]).Matricule;
            string scriptPath = System.AppDomain.CurrentDomain.BaseDirectory + "tmp"+Path.DirectorySeparatorChar + matricule + ".sql";
			generateScript(databaseName, scriptType, scriptPath, matricule);
			Response.Redirect ("/tmp/" + matricule + ".sql");
            return Json("tmp/" + matricule + ".sql");
        }

        [HttpPost]
        public ActionResult extractAll(string databaseName, string codeType, string scriptType)
        {
            initUser();

            string matricule = ((User)Session["user"]).Matricule;
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "tmp"+Path.DirectorySeparatorChar + matricule + ""+Path.DirectorySeparatorChar;
            string zipPath = System.AppDomain.CurrentDomain.BaseDirectory + "tmp"+Path.DirectorySeparatorChar + matricule + ".zip";
            string scriptPath = System.AppDomain.CurrentDomain.BaseDirectory + "tmp"+Path.DirectorySeparatorChar + matricule + ""+Path.DirectorySeparatorChar+"script.sql";
            generateCode(databaseName, codeType, path, zipPath, matricule);
            generateScript(databaseName, scriptType, scriptPath, matricule);
			ZipFile.CreateFromDirectory(path, zipPath);
			Response.Redirect ("/tmp/" + matricule + ".zip");
            return Json("tmp/" + matricule + ".zip");
        }



        private void generateScript(string databaseName, string scriptType, string scriptPath, string matricule)
        {
            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db = udb.getDatabaseByName(databaseName);


            List<int> list = new List<int>();
            List<Table> tables = db.Content;

            /*for (int i = 0; i < tables.Count; i++)
            {
                for (int j = 0; j < tables.Count; j++)
                {
                    if(j == 2 || j == 8 || j == 9 || j == 10 || j == 16)
                    {

                    }
                    if (!list.Contains(j))
                    {
                        bool isOk = true;
                        if (tables[j].ForeignKeys.Count == 0)
                            isOk = true;
                        foreach (ForeignKey fk in tables[j].ForeignKeys)
                        {
                            if(fk.ExternTableName != tables[j].Name)
                            {
                                bool tmp = false;
                                for (int k = 0; k < list.Count && tmp == false; k++)
                                {
                                    if (tables[list[k]].Name == fk.ExternTableName)
                                    {
                                        tmp = true;
                                    }
                                }
                                if (tmp == false)
                                    isOk = false;
                            }
                        }
                        if (isOk == true)
                        {
                            list.Add(j);
                        }
                    }
                }
            }*/
            string script = "";
            string fkString = "";
            for (int i = 0; i < tables.Count; i++)
            {
                string tableFK = "";
                if (i == 0)
                {
                    if (scriptType == "SQLServer")
                    {
                        script = CRUDGenerator.Templates.Scripts.SQLServer.TableScriptGenerator.getTableScript(tables[i], out tableFK);
                    }
                    else if (scriptType == "MySQL")
                    {
                        script = CRUDGenerator.Templates.Scripts.MySQL.TableScriptGenerator.getTableScript(tables[i]);
                    }
                }
                else
                {
                    if (scriptType == "SQLServer")
                    {
                        script += "\n\n\n" + CRUDGenerator.Templates.Scripts.SQLServer.TableScriptGenerator.getTableScript(tables[i], out tableFK);
                    }
                    else if (scriptType == "MySQL")
                    {
                        script += "\n\n\n" + CRUDGenerator.Templates.Scripts.MySQL.TableScriptGenerator.getTableScript(tables[i]);
                    }
                }
                fkString += "\n\n\n" + tableFK;
            }
            System.IO.File.WriteAllText(scriptPath, script + fkString);
        }

        private void generateCode(string databaseName, string codeType, string path, string zipPath, string matricule)
        {
            UserDatabasesDAO dao = new UserDatabasesDAO();
            UserDatabases udb = dao.findByMatricule(matricule);
            DatabaseXML db = udb.getDatabaseByName(databaseName);

            emptyUserTmp(path, zipPath);
            string abstractDao = "";
            if (codeType == "CS")
            {
                //abstractDao = CRUDGenerator.Templates.Classes.CS.AbstractDAOGenerator.getAbstractDAOClass(db);
                //System.IO.File.WriteAllText(path + "DAO"+Path.DirectorySeparatorChar+"AbstractDao.cs", abstractDao);

            }
            else if (codeType == "PHP")
            {
                abstractDao = CRUDGenerator.Templates.Classes.PHP.AbstractDAOGenerator.getAbstractDAOClass(db);
                System.IO.File.WriteAllText(path + "DAO"+Path.DirectorySeparatorChar+"AbstractDao.php", abstractDao);
            }
            foreach (Table table in db.Content)
            {
                //string entity = "";
                string daoString = "";
                if (codeType == "CS")
                {
                    if (table.Type == "Normal")
                    {
                        Directory.CreateDirectory(path + "Views" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar + table.Name + "" + Path.DirectorySeparatorChar);
                        Directory.CreateDirectory(path + "Views" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar + table.Name + "" + Path.DirectorySeparatorChar + "Scripts");
                        //Directory.CreateDirectory(path + "Scripts" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar + table.Name + "" + Path.DirectorySeparatorChar);
                        //entity = CRUDGenerator.Templates.Classes.CS.EntityGenerator.getEntityClassString(table, db.Name);
                        daoString = CRUDGenerator.Templates.Classes.CS.DAOGenerator.getDAOClass(table, db.Name).Replace("&lt;", "<").Replace("&gt;", ">"); ;
                        //System.IO.File.WriteAllText(path + "DAO"+Path.DirectorySeparatorChar+"Entity"+Path.DirectorySeparatorChar + FirstCharToUpper(table.Name) + ".cs", entity);
                        string controller = CRUDGenerator.Templates.Classes.CS.ControllerGenerator.getControllerClass(table, db).Replace("&lt;", "<").Replace("&gt;", ">").Replace("Ã©", "é");
                        System.IO.File.WriteAllText(path + "DataAccessObject" + Path.DirectorySeparatorChar + FirstCharToUpper(table.Name) + "DataAccess.cs", daoString);
                        System.IO.File.WriteAllText(path + "Controllers" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar + FirstCharToUpper(table.Name) + "Controller.cs", controller);
                        
                        /*string InsertForm = getHtml(db.Name, table.Name, "InsertForm", matricule).Replace("&lt;", "<").Replace("&gt;", ">").Replace("Ã©", "é");
                        string ModifyForm = getHtml(db.Name, table.Name, "ModifyForm", matricule).Replace("&lt;", "<").Replace("&gt;", ">").Replace("Ã©", "é");
                        string ViewDatas = getHtml(db.Name, table.Name, "ViewDatas", matricule).Replace("&lt;", "<").Replace("&gt;", ">").Replace("Ã©", "é");
                        string InsertFormScript = getHtml(db.Name, table.Name, "InsertFormScript", matricule).Replace("&lt;", "<").Replace("&gt;", ">").Replace("Ã©", "é");
                        string ModifyFormScript = getHtml(db.Name, table.Name, "ModifyFormScript", matricule).Replace("&lt;", "<").Replace("&gt;", ">").Replace("Ã©", "é");
                        string ViewDatasScript = getHtml(db.Name, table.Name, "ViewDatasScript", matricule).Replace("&lt;", "<").Replace("&gt;", ">").Replace("Ã©", "é");
                        System.IO.File.WriteAllText(path + "Views" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar + table.Name + "" + Path.DirectorySeparatorChar + "create.cshtml", InsertForm);
                        System.IO.File.WriteAllText(path + "Views" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar + table.Name + "" + Path.DirectorySeparatorChar + "modify.cshtml", ModifyForm);
                        System.IO.File.WriteAllText(path + "Views" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar + table.Name + "" + Path.DirectorySeparatorChar + "list.cshtml", ViewDatas);
                        System.IO.File.WriteAllText(path + "Views" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar + table.Name + "" + Path.DirectorySeparatorChar + "Scripts" + Path.DirectorySeparatorChar + "createScript.cshtml", InsertFormScript);
                        System.IO.File.WriteAllText(path + "Views" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar + table.Name + "" + Path.DirectorySeparatorChar + "Scripts" + Path.DirectorySeparatorChar + "modifyScript.cshtml", ModifyFormScript);
                        System.IO.File.WriteAllText(path + "Views" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar + table.Name + "" + Path.DirectorySeparatorChar + "Scripts" + Path.DirectorySeparatorChar + "listScript.cshtml", ViewDatasScript);
                        */
                    }
                    else
                    {
                        daoString = CRUDGenerator.Templates.Classes.CS.DAOGenerator.getDAOClass(table, db.Name).Replace("&lt;", "<").Replace("&gt;", ">"); ;
                        System.IO.File.WriteAllText(path + "DataAccessObject" + Path.DirectorySeparatorChar + FirstCharToUpper(table.Name) + "DataAccess.cs", daoString);
                        string controller = CRUDGenerator.Templates.Classes.CS.ControllerGenerator.getControllerClass(table, db).Replace("&lt;", "<").Replace("&gt;", ">").Replace("Ã©", "é");
                        System.IO.File.WriteAllText(path + "Controllers" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar + FirstCharToUpper(table.Name) + "Controller.cs", controller);
                    }
                }
                else if (codeType == "PHP")
                {
                    //entity = CRUDGenerator.Templates.Classes.PHP.EntityGenerator.getEntityClassString(table, db.Name);
                    daoString = CRUDGenerator.Templates.Classes.PHP.DAOGenerator.getDAOClass(table, db.Name);
                    //System.IO.File.WriteAllText(path + "DAO"+Path.DirectorySeparatorChar+"Entity"+Path.DirectorySeparatorChar + FirstCharToUpper(table.Name) + ".php", entity);
                    System.IO.File.WriteAllText(path + "DAO"+Path.DirectorySeparatorChar + FirstCharToUpper(table.Name) + "Dao.php", daoString);
                }
            }

            if(codeType == "CS")
            {

                System.IO.File.WriteAllText(path + "addToBundleConfig.txt", @"
            var ajaxCssBundle = new Bundle(""~/bundles/AjaxCss"", new CssMinify());
            ajaxCssBundle.Include(
                ""~/Content/lib/bootstrap.css"",
                ""~/Content/lib/font-awesome.css"",
                ""~/Content/lib/jquery.dataTables.css"",
                ""~/Content/lib/fixedHeader.dataTables.css"",
                ""~/Content/lib/jquery-ui-smoothness.css"",
                ""~/Content/lib/jquery-ui-smoothness.theme.css"",
                ""~/Content/lib/jquery-ui-timepicker-addon.css"",
                ""~/Content/lib/chosen.css"",
                ""~/Content/lib/sweetalert2.css"",
                ""~/Content/app/Site.css""
                );
            var ajaxJsBundle = new Bundle(""~/bundles/AjaxJs"", new JsMinify());
            ajaxJsBundle.Include(
                ""~/Scripts/lib/jquery-2.2.4.js"",
                ""~/Scripts/lib/bootstrap.js"",
                ""~/Scripts/lib/jquery.dataTables.js"",
                ""~/Scripts/lib/jquery-ui.js"",
                ""~/Scripts/lib/datepicker-fr.js"",
                ""~/Scripts/lib/jquery-ui-timepicker-addon.js"",
                ""~/Scripts/lib/chosen.jquery.js"",
                ""~/Scripts/lib/chosen.proto.js"",
                ""~/Scripts/lib/sweetalert2.js"",
                ""~/Scripts/lib/date.format.js"",
                ""~/Scripts/app/globalUtils.js""
                ); 
            bundles.Add(ajaxCssBundle);
            bundles.Add(ajaxJsBundle);");
                string AjaxTemplate = getHtml(db.Name, "truc", "AjaxTemplate", matricule);
                System.IO.File.WriteAllText(path + "Views"+Path.DirectorySeparatorChar+"Shared"+Path.DirectorySeparatorChar+"AjaxTemplate.cshtml", AjaxTemplate);


            }

            string SourcePath = System.AppDomain.CurrentDomain.BaseDirectory + "tmp"+Path.DirectorySeparatorChar+ "addToArchive";
            string DestinationPath = path + "";

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                SearchOption.AllDirectories))
                System.IO.File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);

            /*SourcePath = System.AppDomain.CurrentDomain.BaseDirectory + "tmp"+Path.DirectorySeparatorChar+"Fonts";
            DestinationPath = path + "Fonts";

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                SearchOption.AllDirectories))
                System.IO.File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);

            SourcePath = System.AppDomain.CurrentDomain.BaseDirectory + "tmp"+Path.DirectorySeparatorChar+"Images";
            DestinationPath = path + "Images";

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                SearchOption.AllDirectories))
                System.IO.File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
            */
        }

        private string getHtml(string databaseName, string tableName, string method, string matricule)
		{

			WebRequest request = WebRequest.Create("http://"+Request.Url.Authority+"/"+method+ "?databaseName=" + databaseName + "&tableName=" + tableName + "&matricule=" + matricule);
            request.Method = "Get";
            request.Credentials = CredentialCache.DefaultCredentials;
			var postData = "databaseName=" + databaseName + "&tableName=" + tableName;
            /*using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }*/
            var response = (HttpWebResponse)request.GetResponse();

            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
        

        private void emptyUserTmp(string path, string zipPath)
        {
            if (System.IO.File.Exists(zipPath))
            {
                System.IO.File.Delete(zipPath);
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                var tmp = di.GetDirectories();
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
			Directory.CreateDirectory(path + "DataAccessObject"+Path.DirectorySeparatorChar);
            Directory.CreateDirectory(path + "lib"+Path.DirectorySeparatorChar);
            Directory.CreateDirectory(path + "Images"+Path.DirectorySeparatorChar);
            Directory.CreateDirectory(path + "Fonts"+Path.DirectorySeparatorChar);
            Directory.CreateDirectory(path + "lib"+Path.DirectorySeparatorChar+"Scripts"+Path.DirectorySeparatorChar);
            Directory.CreateDirectory(path + "Controllers"+Path.DirectorySeparatorChar);
            Directory.CreateDirectory(path + "Controllers"+Path.DirectorySeparatorChar+"Ajax"+Path.DirectorySeparatorChar);
            Directory.CreateDirectory(path + "Views"+Path.DirectorySeparatorChar);
            Directory.CreateDirectory(path + "Views" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar);
            Directory.CreateDirectory(path + "Scripts" + Path.DirectorySeparatorChar + "Ajax" + Path.DirectorySeparatorChar);
            Directory.CreateDirectory(path + "Views"+Path.DirectorySeparatorChar+"Shared"+Path.DirectorySeparatorChar);
            //Directory.CreateDirectory(path + "DAO"+Path.DirectorySeparatorChar+"Entity"+Path.DirectorySeparatorChar);
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

    }
}
