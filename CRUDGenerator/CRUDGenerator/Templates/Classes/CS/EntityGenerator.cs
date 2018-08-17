using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUDGenerator.Database;

namespace CRUDGenerator.Templates.Classes.CS
{
    public class EntityGenerator
    {
        public static string getEntityClassString(Table table, string databaseName)
        {
            string tableName = FirstCharToUpper(table.Name);
            string attributes = "";
            string parameters = "";
            string initializers = "";
            string dicInitializers = "";
            int i = 0;
            foreach (Column column in table.Columns)
            {
                attributes += @"
        "+getAttributeLine(column);
                initializers += @"
            this." + column.Name + " = " + column.Name+";";
                dicInitializers += @"
            " + getDicInitializerLine(column);
                if (i == 0)
                    parameters = getParameter(column);
                else
                    parameters += ", " + getParameter(column);
                i++;
            }
            string res = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace " + FirstCharToUpper(databaseName) + @".Dao.Entity
{
    public class " + FirstCharToUpper(table.Name) + @"
    {
        " + attributes + @"

        public " + FirstCharToUpper(table.Name) + @"(" + parameters + @")
        {
            " + initializers + @"
        }
        public " + FirstCharToUpper(table.Name) + @"()
        {
        }

        public " + FirstCharToUpper(table.Name) + @"(Dictionary<string, string> dic)
        {
            CultureInfo culture = new CultureInfo(""en-US"");
            " + dicInitializers + @"
        }
    }
}";
            return res;
        }

        private static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        private static string getAttributeLine(Column column)
        {
            string res = "";
            switch (column.Type)
            {
                case CRUDGenerator.Database.Type.Int:
                    res = "public int " + column.Name + " { get; set; }";
                    break;
                case CRUDGenerator.Database.Type.Int_AI:
                    res = "public int " + column.Name + " { get; set; }";
                    break;
                case CRUDGenerator.Database.Type.Boolean:
                    res = "public byte " + column.Name + " { get; set; }";
                    break;
                case CRUDGenerator.Database.Type.String:
                    res = "public string " + column.Name + " { get; set; }";
                    break;
                case CRUDGenerator.Database.Type.Text:
                    res = "public string " + column.Name + " { get; set; }";
                    break;
                case CRUDGenerator.Database.Type.Float:
                    res = "public double " + column.Name + " { get; set; }";
                    break;
                case CRUDGenerator.Database.Type.Datetime:
                    res = "public DateTime " + column.Name + " { get; set; }";
                    break;
                case CRUDGenerator.Database.Type.Date:
                    res = "public DateTime " + column.Name + " { get; set; }";
                    break;
                case CRUDGenerator.Database.Type.Decimal:
                    res = "public double " + column.Name + " { get; set; }";
                    break;
                case CRUDGenerator.Database.Type.Tinyint:
                    res = "public short " + column.Name + " { get; set; }";
                    break;
                case CRUDGenerator.Database.Type.Smallint:
                    res = "public short " + column.Name + " { get; set; }";
                    break;
                case CRUDGenerator.Database.Type.Bigint:
                    res = "public long " + column.Name + " { get; set; }";
                    break;
                default:
                    break;
            }

            return res;
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

        private static string getDicInitializerLine(Column column)
        {
            string res = "";
            switch (column.Type)
            {
                case CRUDGenerator.Database.Type.Int:
                    res = "this." + column.Name + " = Int32.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Int_AI:
                    res = "this." + column.Name + " = Int32.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Boolean:
                    res = "this." + column.Name + " = byte.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.String:
                    res = "this." + column.Name + " = dic[\"" + column.Name + "\"];";
                    break;
                case CRUDGenerator.Database.Type.Text:
                    res = "this." + column.Name + " = dic[\"" + column.Name + "\"];";
                    break;
                case CRUDGenerator.Database.Type.Float:
                    res = "this." + column.Name + " = double.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Datetime:
                    res = "this." + column.Name + " = DateTime.Parse(dic[\"" + column.Name + "\"], culture);";
                    break;
                case CRUDGenerator.Database.Type.Date:
                    res = "this." + column.Name + " = DateTime.Parse(dic[\"" + column.Name + "\"], culture);";
                    break;
                case CRUDGenerator.Database.Type.Decimal:
                    res = "this." + column.Name + " = double.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Tinyint:
                    res = "this." + column.Name + " = Int16.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Smallint:
                    res = "this." + column.Name + " = Int16.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                case CRUDGenerator.Database.Type.Bigint:
                    res = "this." + column.Name + " = Int64.Parse(dic[\"" + column.Name + "\"]);";
                    break;
                default:
                    break;
            }
            return res;
        }
    }
}