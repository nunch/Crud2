using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUDGenerator.Database;
using System.Net;

namespace CRUDGenerator.Templates.Classes.CS
{
    public class ControllerGenerator
    {
        public static string getControllerClass(Table table, DatabaseXML db)
        {
            string res = getStructure(db, table);
            string res2 = getBasicFunctions(db, table);
            res = res.Replace("/**/", res2);
            return res;
        }
        public static string getStructure(DatabaseXML db, Table table)
        {
            string modelAttributes = "";
            for (int i = 0; i < db.Content.Count; i++)
            {
                Table tmp = db.Content[i];
                modelAttributes += @"
        readonly "+FirstCharToUpper(tmp.Name)+ "DataAccess " + FirstCharToUpper(tmp.Name)+ "Instance = new " + FirstCharToUpper(tmp.Name) + "DataAccess();";
            }
            string res = @"using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using DataAccess;
using DataAccess.DataAccessObject;

namespace " + db.Name + @".Controllers.Ajax
{
    public class " + FirstCharToUpper(table.Name) + @"Controller : Controller
    {"+modelAttributes+@"
        
        /**/
    }
}
";
            return res;
        }


        public static string getBasicFunctions(DatabaseXML db, Table table)
        {
            var res = "";
            if(table.Type == "Normal")
            {
                res = getCreateFunction(db, table);
                res += getRemoveFunction(db, table);
                res += getUpdateFunction(db, table);
                res += getFindByPrimaryKeyFunction(table);
                res += getFindAllFunction(table);
                res += getFindByForeignKey(table);
                //res += getScriptFunction(db, table);
            }
            else
            {
                res += getFindAllFunction(table);
            }
            return res;
        }

        public static string getScriptFunction(DatabaseXML db, Table table)
        {
            string generateAllTableScript = "";
            for (int i = 0; i < db.Content.Count; i++)
            {
                Table tmp = db.Content[i];
                generateAllTableScript += @"
            " + FirstCharToUpper(tmp.Name) + "Controller " + FirstCharToLower(tmp.Name) + "Controller = new " + FirstCharToUpper(tmp.Name) + @"Controller();
            " + FirstCharToLower(tmp.Name) + "Controller.generateTableScript2(Request.Url.Authority);";
            }
            return @"
        [HttpGet]
        public void generateTableScript()
        {
            WebRequest createRequest = WebRequest.Create(""http://"" + Request.Url.Authority + ""/" + table.Name + @"/getCreateScript""); 
            createRequest.Method = ""Get"";
            createRequest.Credentials = CredentialCache.DefaultCredentials;
            var createResponse = (HttpWebResponse)createRequest.GetResponse();
            string createString = new System.IO.StreamReader(createResponse.GetResponseStream()).ReadToEnd();
            System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory+""/Scripts/Ajax/" + table.Name + @"/create.js"", createString);

            WebRequest modifyRequest = WebRequest.Create(""http://"" + Request.Url.Authority + ""/" + table.Name + @"/getModifyScript""); 
            modifyRequest.Method = ""Get"";
            modifyRequest.Credentials = CredentialCache.DefaultCredentials;
            var modifyResponse = (HttpWebResponse)modifyRequest.GetResponse();
            string modifyString = new System.IO.StreamReader(modifyResponse.GetResponseStream()).ReadToEnd();
            System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory+""/Scripts/Ajax/" + table.Name + @"/modify.js"", modifyString);

            WebRequest listRequest = WebRequest.Create(""http://"" + Request.Url.Authority + ""/" + table.Name + @"/getListScript""); 
            listRequest.Method = ""Get"";
            listRequest.Credentials = CredentialCache.DefaultCredentials;
            var listResponse = (HttpWebResponse)listRequest.GetResponse();
            string listString = new System.IO.StreamReader(listResponse.GetResponseStream()).ReadToEnd();
            System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory+""/Scripts/Ajax/" + table.Name + @"/list.js"", listString);
        }


        public void generateTableScript2(string authority)
        {
            WebRequest createRequest = WebRequest.Create(""http://"" + authority + ""/" + table.Name + @"/getCreateScript""); 
            createRequest.Method = ""Get"";
            createRequest.Credentials = CredentialCache.DefaultCredentials;
            var createResponse = (HttpWebResponse)createRequest.GetResponse();
            string createString = new System.IO.StreamReader(createResponse.GetResponseStream()).ReadToEnd();
            System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory+""/Scripts/Ajax/" + table.Name + @"/create.js"", createString);

            WebRequest modifyRequest = WebRequest.Create(""http://"" + authority + ""/" + table.Name + @"/getModifyScript""); 
            modifyRequest.Method = ""Get"";
            modifyRequest.Credentials = CredentialCache.DefaultCredentials;
            var modifyResponse = (HttpWebResponse)modifyRequest.GetResponse();
            string modifyString = new System.IO.StreamReader(modifyResponse.GetResponseStream()).ReadToEnd();
            System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory+""/Scripts/Ajax/" + table.Name + @"/modify.js"", modifyString);

            WebRequest listRequest = WebRequest.Create(""http://"" + authority + ""/" + table.Name + @"/getListScript""); 
            listRequest.Method = ""Get"";
            listRequest.Credentials = CredentialCache.DefaultCredentials;
            var listResponse = (HttpWebResponse)listRequest.GetResponse();
            string listString = new System.IO.StreamReader(listResponse.GetResponseStream()).ReadToEnd();
            System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory+""/Scripts/Ajax/" + table.Name + @"/list.js"", listString);
        }



        [HttpGet]
        public void generateAllTablesScript()
        {" + generateAllTableScript + @"
        }


        [HttpGet]
        public ActionResult getCreateScript()
        {
            return View(""~/Views/Ajax/" + FirstCharToUpper(table.Name) + @"/Scripts/createScript.cshtml"");
        }

        [HttpGet]
        public ActionResult getModifyScript()
        {
            return View(""~/Views/Ajax/" + FirstCharToUpper(table.Name) + @"/Scripts/modifyScript.cshtml"");
        }

        [HttpGet]
        public ActionResult getListScript()
        {
            return View(""~/Views/Ajax/" + FirstCharToUpper(table.Name) + @"/Scripts/listScript.cshtml"");
        }";
        }

        public static string getCreateFunction(DatabaseXML db, Table table)
        {
            string tableName = table.Name;
            List<Dictionary<string, dynamic>> n_m_relations = getNMRelations(db, table);
            string n_m_parameters = "";
            string n_m_init = "";
            string tableIdName = table.PrimaryKey[0];
            for (int i = 0; i < n_m_relations.Count; i++)
            {
                //@(n_m_Relations[i].Name)_list
                string parameterName = ((Table)n_m_relations[i]["inter_n_m_Relations"]).Name + "_list";
                Table interTable = (Table)n_m_relations[i]["inter_n_m_Relations"];
                string inter_n_m_Relation_columnName = (string)n_m_relations[i]["inter_n_m_Relation_columnName"];
                n_m_parameters += ", List<"+ interTable.Name + "> " + parameterName;
                n_m_init += @"
            if(" + parameterName + @" != null)
            {
                for (int i = 0; i < " + parameterName + @".Count; i++)
                {
                    " + FirstCharToUpper(interTable.Name) + @"  " + FirstCharToLower(interTable.Name) + @" = " + parameterName + @"[i];
                    " + FirstCharToLower(interTable.Name) + @"." + inter_n_m_Relation_columnName + @" = " + FirstCharToLower(tableName) + @"." + tableIdName + @";
                    msg = " + FirstCharToUpper(interTable.Name) + @"Instance.Add" + FirstCharToUpper(interTable.Name) + @"(ref " + FirstCharToLower(interTable.Name) + @");
                    //if(!msg.status)
                        //return JsonConvert.SerializeObject(msg);
                }
            }
";
            }
            string res = "";
            if(n_m_parameters == "") {
                res = @"
        
        [HttpPost]
        public string Add" + FirstCharToUpper(tableName) + "(" + FirstCharToUpper(tableName) + " " + FirstCharToLower(tableName) + @"" + n_m_parameters + @")
        {
            MessageModel msg = null;
            MessageModel msgAdd = " + FirstCharToUpper(table.Name) + @"Instance.Add" + FirstCharToUpper(tableName) + "(ref " + FirstCharToLower(tableName) + @");
            " + n_m_init + @"
            return JsonConvert.SerializeObject(msgAdd);
        }
";
            }
            else {
                res = @"
        
        [HttpPost]
        public string Add" + FirstCharToUpper(tableName) + "(" + FirstCharToUpper(tableName) + " " + FirstCharToLower(tableName) + @"" + n_m_parameters + @")
        {
            MessageModel msg = null;
            MessageModel msgAdd = " + FirstCharToUpper(table.Name) + @"Instance.Add" + FirstCharToUpper(tableName) + "(ref " + FirstCharToLower(tableName) + @");
            " + n_m_init + @"
            return JsonConvert.SerializeObject(msgAdd);
        }

        [HttpPost]
        public string Add" + FirstCharToUpper(tableName) + "(" + FirstCharToUpper(tableName) + " " + FirstCharToLower(tableName) + @")
        {
            MessageModel msgAdd = " + FirstCharToUpper(table.Name) + @"Instance.Add" + FirstCharToUpper(tableName) + "(ref " + FirstCharToLower(tableName) + @");
            return JsonConvert.SerializeObject(msgAdd);
        }
";
            }
            return res;
        }

        public static string getRemoveFunction(DatabaseXML db, Table table)
        {
            string tableName = table.Name;
            string res = @"
        [HttpPost]
        public string Remove" + FirstCharToUpper(tableName) + @"(int Id_" + FirstCharToUpper(tableName) + @")
        {
            MessageModel msg = " + FirstCharToUpper(table.Name) + @"Instance.Remove" + FirstCharToUpper(tableName) + @"(Id_" + FirstCharToUpper(tableName) + @");
            return JsonConvert.SerializeObject(msg);
        }
";
            return res;
        }

        public static string getUpdateFunction(DatabaseXML db, Table table)
        {
            string tableName = table.Name;
            List<Dictionary<string, dynamic>> n_m_relations = getNMRelations(db, table);
            string n_m_add_parameters = "";
            string n_m_add_init = "";
            string n_m_remove_parameters = "";
            string n_m_remove_init = "";
            string tableIdName = table.PrimaryKey[0];
            for (int i = 0; i < n_m_relations.Count; i++)
            {
                //@(n_m_Relations[i].Name)_list
                string addParameterName = ((Table)n_m_relations[i]["inter_n_m_Relations"]).Name + "_list";
                string removeParameterName = ((Table)n_m_relations[i]["inter_n_m_Relations"]).Name + "_remove_list";
                Table interTable = (Table)n_m_relations[i]["inter_n_m_Relations"];
                string inter_n_m_Relation_columnName = (string)n_m_relations[i]["inter_n_m_Relation_columnName"];
                n_m_add_parameters += ", List<" + interTable.Name + "> " + addParameterName;
                n_m_remove_parameters += ", List<int> " + removeParameterName;
                n_m_add_init += @"
                if(" + addParameterName + @" != null)
                {
                    for (int i = 0; i < " + addParameterName + @".Count; i++)
                    {
                        " + FirstCharToUpper(interTable.Name) + @"  " + FirstCharToLower(interTable.Name) + @" = " + addParameterName + @"[i];
                        " + FirstCharToLower(interTable.Name) + @"." + inter_n_m_Relation_columnName + @" = " + FirstCharToLower(tableName) + @"." + tableIdName + @";
                        msg = " + FirstCharToUpper(interTable.Name) + @"Instance.Add" + FirstCharToUpper(interTable.Name) + @"(ref " + FirstCharToLower(interTable.Name) + @");
                    }
                }
";
                n_m_remove_init += @"
                if(" + removeParameterName + @" != null)
                {
                    for (int i = 0; i < " + removeParameterName + @".Count; i++)
                    {
                        msg = " + FirstCharToUpper(interTable.Name) + @"Instance.Remove" + FirstCharToUpper(interTable.Name) + @"(" + removeParameterName + @"[i]);
                    }
                }
";
            }
            string res = "";
            if(n_m_add_parameters == "") {
                res = @"
        [HttpPost]
        public string Update" + FirstCharToUpper(tableName) + @"(" + FirstCharToUpper(tableName) + " " + FirstCharToLower(tableName) + n_m_add_parameters + n_m_remove_parameters + @")
        {
            MessageModel msg = null;
            MessageModel updateMsg = " + FirstCharToUpper(tableName) + @"Instance.Update" + FirstCharToUpper(tableName) + @"(" + FirstCharToLower(tableName) + @");
            " + n_m_add_init + @"
            " + n_m_remove_init + @"
            return JsonConvert.SerializeObject(updateMsg);
        }
";
            }
            else {
                res = @"
        [HttpPost]
        public string Update" + FirstCharToUpper(tableName) + @"(" + FirstCharToUpper(tableName) + " " + FirstCharToLower(tableName) + @")
        {
            MessageModel msg = null;
            MessageModel updateMsg = " + FirstCharToUpper(tableName) + @"Instance.Update" + FirstCharToUpper(tableName) + @"(" + FirstCharToLower(tableName) + @");
            return JsonConvert.SerializeObject(updateMsg);
        }
        [HttpPost]
        public string Update" + FirstCharToUpper(tableName) + @"(" + FirstCharToUpper(tableName) + " " + FirstCharToLower(tableName) + n_m_add_parameters + n_m_remove_parameters + @")
        {
            MessageModel msg = null;
            MessageModel updateMsg = " + FirstCharToUpper(tableName) + @"Instance.Update" + FirstCharToUpper(tableName) + @"(" + FirstCharToLower(tableName) + @");
            " + n_m_add_init + @"
            " + n_m_remove_init + @"
            return JsonConvert.SerializeObject(updateMsg);
        }
";
            }
            
            return res;
        }

        public static string getFindByPrimaryKeyFunction(Table table)
        {
            string tableName = table.Name;

            string res = @"
        [HttpPost]
        public string Get" + FirstCharToUpper(tableName) + @"ById(int Id_" + FirstCharToUpper(tableName) + @")
        {
            MessageModel msg = " + FirstCharToUpper(tableName) + @"Instance.Get" + FirstCharToUpper(tableName) + @"ById(Id_" + FirstCharToUpper(tableName) + @");
            return JsonConvert.SerializeObject(msg);
        }
";
            return res;
        }

        public static string getFindAllFunction(Table table)
        {
            string tableName = table.Name;
            
            string res = @"
        [HttpPost]
        public string GetAll" + FirstCharToUpper(tableName) + @"()
        {
            MessageModel msg = " + FirstCharToUpper(tableName) + @"Instance.GetAll" + FirstCharToUpper(tableName) + @"();
            return JsonConvert.SerializeObject(msg);
        }
";
            return res;
        }
        

        public static string getFindByForeignKey(Table table)
        {
            string tableName = table.Name;
            string res = "";
            int foreignKeyNumber = table.ForeignKeys.Count;
            List<ForeignKey[]> combinations = CreateSubsets(table.ForeignKeys.ToArray());
            foreach (ForeignKey[] tmp in combinations)
            {
                List<ForeignKey> combination = tmp.ToList();
                string functionName = combination[0].ExternTableName+"Id";
                string parameters = "int " + combination[0].ExternTableName + "Id";
                string parameters2 = combination[0].ExternTableName + "Id";
                
                for (int i = 1; i < combination.Count; i++)
                {
                    functionName += "_" + combination[i].ExternTableName + "Id";
                    parameters += ", int " + combination[i].ExternTableName + "Id";
                    parameters2 += ", " + combination[i].ExternTableName + "Id";
                }

                string text = @"
        [HttpPost]
        public string GetBy" + functionName + @"(" + parameters + @")
        {
            MessageModel msg = " + FirstCharToUpper(tableName) + @"Instance.GetBy" + functionName + @"(" + parameters2 + @");
            return JsonConvert.SerializeObject(msg);
        }
";
                res += text;
            }
            return res;
        }

        public static List<T[]> CreateSubsets<T>(T[] originalArray)
        {
            List<T[]> subsets = new List<T[]>();

            for (int i = 0; i < originalArray.Length; i++)
            {
                int subsetCount = subsets.Count;
                subsets.Add(new T[] { originalArray[i] });

                for (int j = 0; j < subsetCount; j++)
                {
                    T[] newSubset = new T[subsets[j].Length + 1];
                    subsets[j].CopyTo(newSubset, 0);
                    newSubset[newSubset.Length - 1] = originalArray[i];
                    subsets.Add(newSubset);
                }
            }

            return subsets;
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        public static string FirstCharToLower(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToLower() + input.Substring(1);
        }

        private static List<Dictionary<string, dynamic>> getNMRelations(DatabaseXML db, Table table)
        {
            List<Dictionary<string, dynamic>> res = new List<Dictionary<string, dynamic>>();
            foreach (Table tmp in db.Content)
            {
                if (tmp.Name != table.Name && tmp.Type == "n-m-relation")
                {
                    if (tmp.ForeignKeys.Count == 2)
                    {
                        Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
                        ForeignKey fk1 = tmp.ForeignKeys[0];
                        ForeignKey fk2 = tmp.ForeignKeys[1];
                        if (fk1.ExternTableName == table.Name)
                        {
                            dic.Add("n_m_Relations", db.getTableByName(fk2.ExternTableName));
                            dic.Add("inter_n_m_Relations", tmp);
                            dic.Add("inter_n_m_Relation_columnName", fk1.Info[0].ColumnName);
                            dic.Add("inter_n_m_Relation_externColumnName", fk2.Info[0].ColumnName);
                            res.Add(dic);
                        }
                        else if (fk2.ExternTableName == table.Name)
                        {
                            dic.Add("n_m_Relations", db.getTableByName(fk1.ExternTableName));
                            dic.Add("inter_n_m_Relations", tmp);
                            dic.Add("inter_n_m_Relation_columnName", fk2.Info[0].ColumnName);
                            dic.Add("inter_n_m_Relation_externColumnName", fk1.Info[0].ColumnName);
                            res.Add(dic);
                        }
                    }
                }
            }
            return res;
        }
    }
}