using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using CRUDGenerator.Database;

namespace CRUDGenerator.Areas.SQLManagement.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /SQLManagement/Home/

        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["mssqlConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            DataTable dt = connection.GetSchema("Columns");
            
            StringWriter stringWriter = new StringWriter();
            dt.WriteXml(stringWriter);
            string xml = stringWriter.ToString();
            XElement element = XElement.Parse(xml);
            DatabaseXML db = new DatabaseXML();

            foreach (XElement elem1 in element.Elements())
            {
                string isNull = elem1.Element("IS_NULLABLE").Value;
                string tableName = elem1.Element("TABLE_NAME").Value;
                string columnName = elem1.Element("COLUMN_NAME").Value;
                string Type = elem1.Element("DATA_TYPE").Value;
                int Length = 0;
                if (Type == "int")
                    Length = Int32.Parse(elem1.Element("NUMERIC_PRECISION").Value);
                else if (Type == "varchar")
                    Length = Int32.Parse(elem1.Element("CHARACTER_MAXIMUM_LENGTH").Value);

                Table table = db.getTableByName(tableName);
                if (table == null)
                {
                    table = new Table();
                    table.Name = tableName;
                    db.Content.Add(table);
                }
                Column column = new Column{
                    Name = columnName,
                    Length = Length,
                    Type = Database.TypeString.toType(Type)
                };
                if (isNull == "NO")
                    column.isNull = 0;
                else
                    column.isNull = 1;
                db.getTableByName(tableName).Columns.Add(column);
                if (columnName == "col7")
                {
                    Console.WriteLine("youpi");
                }
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
            List<Dictionary<string, string>> primaryKeysList = query(primaryKeys);
            List<Dictionary<string, string>> foreignKeysList = query(foreignKeys);
            List<Dictionary<string, string>> identitiesList = query(identities);
            List<Dictionary<string, string>> indexesList = query(indexes);

            Dictionary<string, List<Dictionary<string, string>>> primaryKeyDic = getDic(primaryKeysList);
            Dictionary<string, List<Dictionary<string, string>>> foreignKeysDic = getDic(foreignKeysList);
            Dictionary<string, List<Dictionary<string, string>>> identitiesDic = getDic(identitiesList);
            Dictionary<string, List<Dictionary<string, string>>> indexesDic = getDic(indexesList);

            foreach (KeyValuePair<string, List<Dictionary<string, string>>> kvp in primaryKeyDic)
            {
                string tableName = kvp.Key;
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


            foreach (KeyValuePair<string, List<Dictionary<string, string>>> kvp in indexesDic)
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
                    db.getTableByName(tableName).getIndexByName(indexName).List[i] = indexName;
                }
            }

            foreach (KeyValuePair<string, List<Dictionary<string, string>>> kvp in indexesDic)
            {
                string tableName = kvp.Key;
                foreach (Dictionary<string, string> dic in kvp.Value)
                {
                    string columnName = dic["ColumnName"];
                    db.getTableByName(tableName).getColumnByName(columnName).Type = Database.Type.Int_AI;
                }
            }


            /*
            TABLE_NAME
            COLUMN_NAME
            IS_NULLABLE
            DATA_TYPE 
                (int) => NUMERIC_PRECISION, 
                (varchar) => CHARACTER_MAXIMUM_LENGTH
                (float) ,
                (datetime) => DATETIME_PRECISION,
                (text),
                (decimal) => NUMERIC_PRECISION = *.NUMERIC_SCALE  (5.05 => {NUMERIC_PRECISION = 3, NUMERIC_SCALE = 2})
                
            */
            return View();
        }

        private Dictionary<string, List<Dictionary<string, string>>> getDic(List<Dictionary<string, string>> list)
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

        protected List<Dictionary<string, string>> query(string query)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssqlConnection"].ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.Prepare();

            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(readerToDic(reader));
            }
            return list;
        }

        private Dictionary<string, string> readerToDic(SqlDataReader reader)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            int count = reader.FieldCount;
            for (int i = 0; i < count; i++)
            {
                string key = reader.GetName(i);
                string value = "";
                try
                {
                    value = reader.GetString(i);
                }
                catch (Exception)
                {
                    try
                    {
                        value = reader.GetInt32(i).ToString();
                    }
                    catch (Exception)
                    {
                        try
                        {
                            value = reader.GetDouble(i).ToString();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                value = reader.GetByte(i).ToString();
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    }
                }
                dic[key] = value;
            }
            return dic;
        }

    }
}
