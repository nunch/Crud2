using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUDGenerator.Database;

namespace CRUDGenerator.Templates.Classes.CS
{
    public class ClassStructure
    {
        public static string getClassStructure(Table table, string databaseName)
        {
            string res = @"using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class " + FirstCharToUpper(table.Name) + @"DataAccess
    {
        /**/
    }
}";

            return res;
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}