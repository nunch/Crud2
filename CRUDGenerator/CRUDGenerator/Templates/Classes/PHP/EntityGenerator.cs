using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUDGenerator.Database;

namespace CRUDGenerator.Templates.Classes.PHP
{
    public class EntityGenerator
    {
        public EntityGenerator()
        {

        }

        public static string getEntityClassString(Table table, string databaseName)
        {
            string tableName = FirstCharToUpper(table.Name);
            string attributes = "";
            string parameters = "";
            string initializers = "";
            int i = 0;
            foreach (Column column in table.Columns)
            {
                attributes += @"
        var $"+ column.Name+";";
                initializers += @"
            $this->"+column.Name+" = $"+column.Name+";";
                if (i == 0)
                    parameters = column.Name;
                else
                    parameters += ", " + column.Name;
                i++;
            }
            string res = @"<?php
if (!defined('"+tableName+@"')) {
    define('" + tableName + @"', '" + tableName + @"');

    class " + tableName + @"{
        " + attributes + @"

        public function __construct(" + parameters + @"){
            " + initializers + @"
        }

        public function __construct(){
            
        }
    }
}
?>";
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