using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUDGenerator.Database;

namespace CRUDGenerator.Templates.Classes.PHP
{
    public class ClassStructure
    {
        public ClassStructure()
        {
        }

        public static string getClassStructure(Table table, string databasename)
        {
            string name = FirstCharToUpper(table.Name);
            return @"<?php
if (!defined('" + name + @"DAO')) {
    define('" + name + @"DAO', '" + name + @"DAO');
    include './AbstractDAO.php';
	include 'Entity/" + name + @".php';
	class " + name + @"DAO extends AbstractDAO{
			/**/
    }
}
?>";
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}