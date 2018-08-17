using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUDGenerator.Database;

namespace CRUDGenerator.Templates.Classes.CS
{
    public class BasicFunction
    {
        public static string getBasicFunctions(Table table)
        {
            var res = "";
            if(table.Type != "Vue")
            {
                res = getAddFunction(table);
                res += getUpdateFunction(table);
                res += getRemoveFunction(table);
                res += getGetById(table);
                res += getRemoveAllFunction(table);
                res += getGetAll(table);
                res += getGetByForeignKey(table);
            }
            else
            {
                res += getGetAll(table);
            }
            return res;
        }
        public static string getAddFunction(Table table)
        {
            string tableName = table.Name;
            string res = @"
        public MessageModel Add" + FirstCharToUpper(tableName) + @"(ref " + FirstCharToUpper(tableName) + " " + FirstCharToLower(tableName) + @")
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db." + FirstCharToUpper(tableName) + @".Add(" + FirstCharToLower(tableName) + @");
                    db.SaveChanges();
                    return new MessageModel(true, """ + FirstCharToUpper(tableName) + @" ajouté avec succès."", " + FirstCharToLower(tableName) + @");
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(""Error Property Name "" + error.PropertyName +"" : Error Message: "" + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
";
            return res;
        }
        //dic["@numberOfFloor"] = building.numberOfFloor.ToString();

        public static string getRemoveFunction(Table table)
        {
            string tableName = table.Name;
            bool isSupprPresent = false;
            string isSupprName = "";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].Name == "IsSuppr")
                {
                    isSupprPresent = true;
                    isSupprName = "IsSuppr";
                }
                if (table.Columns[i].Name == "isSuppr")
                {
                    isSupprPresent = true;
                    isSupprName = "isSuppr";
                }
                if (table.Columns[i].Name == "Is_Suppr")
                {
                    isSupprPresent = true;
                    isSupprName = "Is_Suppr";
                }
                if (table.Columns[i].Name == "is_Suppr")
                {
                    isSupprPresent = true;
                    isSupprName = "is_Suppr";
                }
            }
            string res = "";
            if (isSupprPresent)
            {
                res = @"

        public MessageModel Remove" + FirstCharToUpper(tableName) + @"(int " + table.PrimaryKey[0] + @")
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    " + FirstCharToUpper(tableName) + @" " + FirstCharToLower(tableName) + @"_fromDB = (from tmp in db." + FirstCharToUpper(tableName) + @" where tmp." + table.PrimaryKey[0] + @" == " + table.PrimaryKey[0] + @" select tmp).FirstOrDefault();
                    " + FirstCharToLower(tableName) + @"_fromDB." + isSupprName + @" = true;
                    db.Entry(" + FirstCharToLower(tableName) + @"_fromDB).CurrentValues.SetValues(" + FirstCharToLower(tableName) + @"_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, """ + FirstCharToUpper(tableName) + @" effacé"", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(""Error Property Name "" + error.PropertyName +"" : Error Message: "" + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
    
        public MessageModel Remove" + FirstCharToUpper(tableName) + @"(int " + table.PrimaryKey[0] + @", bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return Remove" + FirstCharToUpper(tableName) + @"(" + table.PrimaryKey[0] + @");
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    " + FirstCharToUpper(tableName) + @" " + FirstCharToLower(tableName) + @"_fromDB = (from tmp in db." + FirstCharToUpper(tableName) + @" where tmp." + table.PrimaryKey[0] + @" == " + table.PrimaryKey[0] + @" select tmp).FirstOrDefault();
                    db." + FirstCharToUpper(tableName) + @".Remove(" + FirstCharToLower(tableName) + @"_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, """ + FirstCharToUpper(tableName) + @" effacé"", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(""Error Property Name "" + error.PropertyName +"" : Error Message: "" + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
";

            }
            else
            {
                res = @"
        public MessageModel Remove" + FirstCharToUpper(tableName) + @"(int " + table.PrimaryKey[0] + @")
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    " + FirstCharToUpper(tableName) + @" " + FirstCharToLower(tableName) + @"_fromDB = (from tmp in db." + FirstCharToUpper(tableName) + @" where tmp." + table.PrimaryKey[0] + @" == " + table.PrimaryKey[0] + @" select tmp).FirstOrDefault();
                    db." + FirstCharToUpper(tableName) + @".Remove(" + FirstCharToLower(tableName) + @"_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, """ + FirstCharToUpper(tableName) + @" effacé"", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(""Error Property Name "" + error.PropertyName +"" : Error Message: "" + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
";
            }
            return res;
        }
        public static string getRemoveAllFunction(Table table)
        {
            string tableName = table.Name;
            string res = @"
        public MessageModel RemoveAll" + FirstCharToUpper(tableName) + @"()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand(""DELETE FROM " + tableName + @""");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand(""DELETE FROM " + tableName + @" where " + table.PrimaryKey[0] + @" = {0}"", " + table.PrimaryKey[0] + @");
                    return new MessageModel(true, """ + FirstCharToUpper(tableName) + @"s effacé(e)s"", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(""Error Property Name "" + error.PropertyName +"" : Error Message: "" + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
";
            return res;
        }

        public static string getUpdateFunction(Table table)
        {
            string tableName = table.Name;
            string res = @"
        public MessageModel Update" + FirstCharToUpper(tableName) + "(" + FirstCharToUpper(tableName) + " " + FirstCharToLower(tableName) + @")
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    " + FirstCharToUpper(tableName) + @" " + FirstCharToLower(tableName) + @"_fromDB = (from tmp in db." + tableName + @" where tmp." + table.PrimaryKey[0] + @" == " + FirstCharToLower(tableName) + @"." + table.PrimaryKey[0] + @" select tmp).FirstOrDefault();
                    if (" + FirstCharToLower(tableName) + @"_fromDB != null)
                    {
                        db.Entry(" + FirstCharToLower(tableName) + @"_fromDB).CurrentValues.SetValues(" + FirstCharToLower(tableName) + @");
                        db.SaveChanges();
                        return new MessageModel(true, ""La modification a eu lieu avec succès."", " + FirstCharToLower(tableName) + @"_fromDB);
                    }
                    else return new MessageModel(false, """ + FirstCharToUpper(tableName) + @" non éxistant."", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(""Error Property Name "" + error.PropertyName +"" : Error Message: "" + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
";
            return res;
        }

        public static string getGetById(Table table)
        {
            string tableName = table.Name;


            string res = @"
        public MessageModel Get" + FirstCharToUpper(tableName) + @"ById(int " + table.PrimaryKey[0] + @")
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    " + FirstCharToUpper(tableName) + @" " + FirstCharToLower(tableName) + @" = (from tmp in db." + tableName + @" where tmp." + table.PrimaryKey[0] + @" == " + table.PrimaryKey[0] + @" select tmp).FirstOrDefault();
                    return new MessageModel(true, """", " + FirstCharToLower(tableName) + @");
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(""Error Property Name "" + error.PropertyName +"" : Error Message: "" + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
";
            return res;
        }

        public static string getGetAll(Table table)
        {
            string tableName = table.Name;

            bool isSupprPresent = false;
            string isSupprName = "";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].Name == "IsSuppr")
                {
                    isSupprPresent = true;
                    isSupprName = "IsSuppr";
                }
                if (table.Columns[i].Name == "isSuppr")
                {
                    isSupprPresent = true;
                    isSupprName = "isSuppr";
                }
                if (table.Columns[i].Name == "Is_Suppr")
                {
                    isSupprPresent = true;
                    isSupprName = "Is_Suppr";
                }
                if (table.Columns[i].Name == "is_Suppr")
                {
                    isSupprPresent = true;
                    isSupprName = "is_Suppr";
                }
            }
            string res = "";
            if (isSupprPresent)
            {
                res = @"
        public MessageModel GetAll" + FirstCharToUpper(tableName) + @"()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<" + FirstCharToUpper(tableName) + @"> list = (from tmp in db." + tableName + @" where tmp."+ isSupprName + @" == false select tmp).ToList();
                    return new MessageModel(true, """", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(""Error Property Name "" + error.PropertyName +"" : Error Message: "" + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
";
            }
            else
            {
                res = @"
        public MessageModel GetAll" + FirstCharToUpper(tableName) + @"()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<" + FirstCharToUpper(tableName) + @"> list = (from tmp in db." + tableName + @" select tmp).ToList();
                    return new MessageModel(true, """", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(""Error Property Name "" + error.PropertyName +"" : Error Message: "" + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
";
            }
            return res;
        }

        public static string getGetByForeignKey(Table table)
        {
            string tableName = table.Name;
            string res = "";
            int foreignKeyNumber = table.ForeignKeys.Count;
            List<ForeignKey[]> combinations = CreateSubsets(table.ForeignKeys.ToArray());
            foreach (ForeignKey[] tmp in combinations)
            {
                List<ForeignKey> combination = tmp.ToList();
                string functionName = FirstCharToUpper(combination[0].ExternTableName) + "Id";
                string parameters = "int " + FirstCharToLower(combination[0].ExternTableName) + "Id";
                string where = "tmp." + combination[0].Info[0].ColumnName + " == " + FirstCharToLower(combination[0].ExternTableName) + "Id";

                for (int i = 1; i < combination.Count; i++)
                {
                    functionName += "_" + FirstCharToUpper(combination[i].ExternTableName) + "Id";
                    parameters += ", int " + FirstCharToLower(combination[i].ExternTableName) + "Id";
                    where += " && tmp." + combination[i].Info[0].ColumnName + " == " + FirstCharToLower(combination[i].ExternTableName) + "Id";
                }
                bool isSupprPresent = false;
                string isSupprName = "";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (table.Columns[i].Name == "IsSuppr")
                    {
                        isSupprPresent = true;
                        isSupprName = "IsSuppr";
                    }
                    if (table.Columns[i].Name == "isSuppr")
                    {
                        isSupprPresent = true;
                        isSupprName = "isSuppr";
                    }
                    if (table.Columns[i].Name == "Is_Suppr")
                    {
                        isSupprPresent = true;
                        isSupprName = "Is_Suppr";
                    }
                    if (table.Columns[i].Name == "is_Suppr")
                    {
                        isSupprPresent = true;
                        isSupprName = "is_Suppr";
                    }
                }
                string text = "";
                if (isSupprPresent)
                {
                    text = @"
        public MessageModel GetBy" + functionName + @"(" + parameters + @")
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<" + FirstCharToUpper(tableName) + @"> list = (from tmp in db." + tableName + @"
                                where tmp." + isSupprName + @" == false && " + where + @"
                                select tmp).ToList();
                    return new MessageModel(true, ""Liste des " + FirstCharToLower(tableName) + @""", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(""Error Property Name "" + error.PropertyName +"" : Error Message: "" + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
";
                }
                else
                {
                    text = @"
        public MessageModel GetBy" + functionName + @"(" + parameters + @")
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<" + FirstCharToUpper(tableName) + @"> list = (from tmp in db." + tableName + @"
                                where " + where + @"
                                select tmp).ToList();
                    return new MessageModel(true, ""Liste des " + FirstCharToLower(tableName) + @""", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(""Error Property Name "" + error.PropertyName +"" : Error Message: "" + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
";

                }

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

        public static string getDicConstructor(Table table)
        {
            string tableName = table.Name;
            string dicInitializers = "";

            foreach (Column column in table.Columns)
            {
                dicInitializers += @"
            " + getDicInitializerLine(column, FirstCharToLower(tableName));
            }
            return @"
        private static " + FirstCharToUpper(tableName) + @" createFromDic(Dictionary<string, string> dic)
        {
            CultureInfo culture = new CultureInfo(""en-US"");
            " + FirstCharToUpper(tableName) + @" " + FirstCharToLower(tableName) + @" = new " + FirstCharToUpper(tableName) + @"();
            " + dicInitializers + @"
            return " + FirstCharToLower(tableName) + @";
        }


";
        }
        private static string getDicInitializerLine(Column column, string tableName)
        {
            string res = "";
            switch (column.Type)
            {
                case CRUDGenerator.Database.Type.Int:
                    res = tableName + "." + column.Name + " = Int32.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Int_AI:
                    res = tableName + "." + column.Name + " = Int32.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Boolean:
                    res = tableName + "." + column.Name + " = bool.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.String:
                    res = tableName + "." + column.Name + " = dic[\"" + column.Name + "\"];";
                    break;
                case CRUDGenerator.Database.Type.Text:
                    res = tableName + "." + column.Name + " = dic[\"" + column.Name + "\"];";
                    break;
                case CRUDGenerator.Database.Type.Float:
                    res = tableName + "." + column.Name + " = double.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Datetime:
                    res = tableName + "." + column.Name + " = DateTime.Parse(dic[\"" + column.Name + "\"], culture);";
                    break;
                case CRUDGenerator.Database.Type.Date:
                    res = tableName + "." + column.Name + " = DateTime.Parse(dic[\"" + column.Name + "\"], culture);";
                    break;
                case CRUDGenerator.Database.Type.Decimal:
                    res = tableName + "." + column.Name + " = double.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Tinyint:
                    res = tableName + "." + column.Name + " = Int16.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Smallint:
                    res = tableName + "." + column.Name + " = Int16.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Bigint:
                    res = tableName + "." + column.Name + " = Int64.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                default:
                    break;
            }
            return res;
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

        private static string getParameter(Column column)
        {
            string res = "";
            switch (column.Type)
            {
                case CRUDGenerator.Database.Type.Int:
                    res = "int " + column.Name;
                    break;
                case CRUDGenerator.Database.Type.Int_AI:
                    res = "int " + column.Name;
                    break;
                case CRUDGenerator.Database.Type.Boolean:
                    res = "byte " + column.Name;
                    break;
                case CRUDGenerator.Database.Type.String:
                    res = "string " + column.Name;
                    break;
                case CRUDGenerator.Database.Type.Text:
                    res = "string " + column.Name;
                    break;
                case CRUDGenerator.Database.Type.Float:
                    res = "double " + column.Name;
                    break;
                case CRUDGenerator.Database.Type.Datetime:
                    res = "DateTime " + column.Name;
                    break;
                case CRUDGenerator.Database.Type.Date:
                    res = "DateTime " + column.Name;
                    break;
                case CRUDGenerator.Database.Type.Decimal:
                    res = "double " + column.Name;
                    break;
                case CRUDGenerator.Database.Type.Tinyint:
                    res = "short " + column.Name;
                    break;
                case CRUDGenerator.Database.Type.Smallint:
                    res = "short " + column.Name;
                    break;
                case CRUDGenerator.Database.Type.Bigint:
                    res = "long " + column.Name;
                    break;
                default:
                    break;
            }
            return res;
        }
    }
}