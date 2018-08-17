using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUDGenerator.Database;

namespace CRUDGenerator.Templates.Classes.PHP
{
    public class DAOGenerator
    {
        public static string getDAOClass(Table table, string databasename)
        {
            string res = ClassStructure.getClassStructure(table, databasename);
            string res2 = BasicFunction.getBasicFunctions(table);
            res = res.Replace("/**/", res2);
            return res;
        }
    }
}