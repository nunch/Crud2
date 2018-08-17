using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUDGenerator.Database;

namespace CRUDGenerator.Templates.Classes.PHP
{
    public class BasicFunction
    {
        public BasicFunction()
        {
        }

        public static string getBasicFunctions(Table table)
        {
            var res = "";
            res = getCreateFunction(table);
            res += getRemoveFunction(table);
            res += getUpdateFunction(table);
            res += getFindByPrimaryKeyFunction(table);
            res += getFindLike(table);
            return res;
        }

        public static string getCreateFunction(Table table)
        {
            string tableName = table.Name;
            string tableColumns = "";
            string tableColumnsReplace = "";
            string bindParams = "";
            int i=0;
            foreach (Column column in table.Columns)
            {
                if (i == 0)
                {
                    tableColumns = "`"+column.Name+"`";
                    tableColumnsReplace = ":" + column.Name;
                }
                else
                {
                    tableColumns += ", `" + column.Name + "`";
                    tableColumnsReplace += ", :" + column.Name;
                }
                bindParams += @"
            " + "$stmt->bindParam(\":" + column.Name + "\", $"+tableName+"->" + column.Name + ");";
                i++;
            }
            string res = @"
        public function create($" + tableName + @"){
        	$query = ""
    INSERT INTO `" + tableName + @"`
        (" + tableColumns + @")
    VALUES (
        " + tableColumnsReplace + @"
    );""
            $stmt = $this->db->prepare($querry);
            " + bindParams + @"
	    $stmt->execute();
        }
";
            return res;
        }

        public static string getRemoveFunction(Table table)
        {
            string tableName = table.Name;
            string primaryKeys = "";
            string where = "";
            string bindParams = "";
            int i = 0;
            foreach (Column column in table.getPrimaryKeys())
            {
                if (i == 0)
                {
                    primaryKeys = "$"+column.Name;
                    where = "`" + column.Name + "` = :" + column.Name;
                }
                else
                {
                    primaryKeys += ", $" + column.Name;
                    where += " AND `" + column.Name + "` = :" + column.Name;
                }
                bindParams += @"
                " + "$stmt->bindParam(\":" + column.Name + "\", $" + tableName + "->" + column.Name + ");";
                i++;
            }
            string res = @"
        public function remove("+primaryKeys+@"){
            try{
                $querry = ""DELETE FROM `" + table.Name + @"` WHERE " + where + @";"";
                $stmt = $this->db->prepare($querry);
                " + bindParams + @"
                $stmt->execute();
            }
            catch(\Exception $e) {
                echo $e->getMessage();
                echo $e->getTraceAsString();
            }
        }";
            return res;
        }

        public static string getUpdateFunction(Table table)
        {
            string tableName = table.Name;
            string set = "";
            string where = "";
            string bindParams = "";
            int i = 0;
            foreach (Column column in table.getNormalColumns())
            {
                if (i == 0)
                    set = "`" + column.Name + "` = :" + column.Name;
                else
                    set += ", `" + column.Name + "` = :" + column.Name;
                bindParams += @"
                " + "$stmt->bindParam(\":" + column.Name + "\", $" + tableName + "->" + column.Name + ");";
                i++;
            }
            i = 0;
            foreach (Column column in table.getPrimaryKeys())
            {
                if (i == 0)
                    where =  "`" + column.Name + "` = :" + column.Name;
                else
                    where += " AND `" + column.Name + "` = :" + column.Name;
                bindParams += @"
                " + "$stmt->bindParam(\":" + column.Name + "\", $" + tableName + "->" + column.Name + ");";
            }
            string res = @"
        public function update($etape){
            $querry = ""update `" + table.Name + @"`
            set " +set+@"
            where "+where+ @";"";
            try{
                $stmt = $this->db->prepare($querry);
				" + bindParams+@"
                $stmt->execute();
            } catch(\Exception $e) {
                echo $e->getMessage();
                echo '<br/>';
                echo $e->getTraceAsString();
            }
        }";
            return res;
        }

        public static string getFindByPrimaryKeyFunction(Table table)
        {

            string tableName = table.Name;
            string parameters = "";
            string where = "";
            string bindParams = "";
            string initObject = "$" + tableName + " = new " + FirstCharToUpper(tableName) + "(";
            int i=0;
            foreach (Column column in table.getPrimaryKeys())
            {
                if(i==0){
                    parameters = "$" + column.Name;
                    where = "`" + column.Name + "` = :" + column.Name;
                }else{
                    parameters += ", $" + column.Name;
                    where += " AND `" + column.Name + "` = :" + column.Name;
                }
                bindParams += @"
                " + "$stmt->bindParam(\":" + column.Name + "\", $" + column.Name + ");";
                i++;
            }
            i = 0;
            foreach (Column column in table.Columns)
            {
                if (i == 0)
                    initObject += "$data[\"" + column.Name + "\"]";
                else
                    initObject += ", $data[\"" + column.Name + "\"]";
                i++;
            }
            initObject += ");";
            string res = @"
        public function find("+parameters+@"){
            try{
                $querry = ""select * from "+tableName+" where "+where+@";"";
                $stmt = $this->db->prepare($querry);
				" + bindParams + @"
                $stmt->execute();
                if($stmt->rowCount() == 0){
                    return NULL;   
                }
                $data = $stmt->fetch(PDO::FETCH_ASSOC);
				" +initObject+@" 
                return $"+tableName+@";
            }
            catch(\Exception $e) {
                echo $e->getMessage();
                echo '<br/>';
                echo $e->getTraceAsString();
            }
        }";
            return res;
        }

        public static string getFindAllFunction(Table table)
        {
            string tableName = table.Name;
            string initObject = "$list[] = new " + FirstCharToUpper(tableName) + "(";
            int i = 0;
            foreach (Column column in table.Columns)
            {
                if (i == 0)
                    initObject += "$data[\"" + column.Name + "\"]";
                else
                    initObject += ", $data[\"" + column.Name + "\"]";
                i++;
            }
            string res = @"
        public function findAll(){
            try{
                $querry = ""select * from "+tableName+@";"";
                $stmt = $this->db->prepare($querry);
                $stmt->execute();
                $results = $stmt->fetchAll(PDO::FETCH_ASSOC);
                foreach($results as $data){
                    "+initObject+@"
                }
                return $list;
            }
            catch(\Exception $e) {
                echo $e->getMessage();
                echo '<br/>';
                echo $e->getTraceAsString();
            }
        }";
            return res;
        }

        public static string getFindLike(Table table)
        {
            string res = "";
            string tableName = table.Name;

            List<List<Column>> doubleList = table.getIndexes();
            foreach (List<Column> list in doubleList)
            {
                string functionName = "findLike";
                string where = "`nom` COLLATE utf8_general_ci like :name";
                string bindParams = "";
                string parameters = "";
                string redefineParameters = "";
                string initObject = "$list[] = new " + tableName + "(";
                int i = 0;
                foreach (Column column in list)
                {
                    if (i == 0)
                    {
                        initObject += "$data[\"" + column.Name + "\"]";
                        parameters = "$" + column.Name;
                        where = "`" + column.Name + "` COLLATE utf8_general_ci like :" + column.Name;
                    }
                    else
                    {
                        initObject += ", $data[\"" + column.Name + "\"]";
                        parameters += ", $" + column.Name;
                        where += " AND `" + column.Name + "` COLLATE utf8_general_ci like :" + column.Name;
                    }
                    redefineParameters += @"
                $" + column.Name + " = $" + column.Name + ".\"%\";";
                    functionName += FirstCharToUpper(column.Name);
                    bindParams += @"
                " + "$stmt->bindParam(\":" + column.Name + "\", $" + column.Name + ");";
                    i++;
                }

                var tmp = @"

        public function "+functionName+"("+parameters+@"){
            try{
				" + redefineParameters + @"
                $querry = ""select * from `"+tableName+@"` where "+where+@" ;"";
                $stmt = $this->db->prepare($querry);
				" + bindParams + @"
                $stmt->execute();
                $results = $stmt->fetchAll(PDO::FETCH_ASSOC);
				$list = array();
                foreach($results as $data){
					" + initObject + @"
                }
                return $list;
            }
            catch(\Exception $e) {
                echo $e->getMessage();
                echo '<br/>';
                echo $e->getTraceAsString();
            }
        }";
                res += tmp;
            }

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