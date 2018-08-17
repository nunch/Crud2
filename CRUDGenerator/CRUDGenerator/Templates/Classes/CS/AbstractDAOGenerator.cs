using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUDGenerator.Database;

namespace CRUDGenerator.Templates.Classes.CS
{
    public class AbstractDAOGenerator
    {
        public static string getAbstractDAOClass(DatabaseXML database)
        {
            string res = @"using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using " + FirstCharToUpper(database.Name) + @".Models;

namespace " + FirstCharToUpper(database.Name) + @".Dao
{
    public class AbstractDao
    {
        public static " + FirstCharToUpper(database.Name) + @"Entities db = new " + FirstCharToUpper(database.Name) + @"Entities();
        public static object dbLock = new object();
        public static string ConnectionString;
        public AbstractDao()
        {
            //this.ConnectionString = ConfigurationManager.ConnectionStrings[""" + database.Name + @"""].ConnectionString;
        }

        public static void init()
        {
            if(AbstractDao.ConnectionString == null || AbstractDao.ConnectionString == """")
            {
                var tmp = ConfigurationManager.ConnectionStrings;
                foreach (ConnectionStringSettings connectionString in tmp)
                {
                    if (connectionString.ConnectionString.Contains(""connection string"")) 
                    {
                        AbstractDao.ConnectionString = connectionString.ConnectionString;
                        AbstractDao.ConnectionString = AbstractDao.ConnectionString.Substring(AbstractDao.ConnectionString.IndexOf('""') + 1, AbstractDao.ConnectionString.LastIndexOf('""') - AbstractDao.ConnectionString.IndexOf('""') - 1);
                    }
                }
            }
        }

        public static List<Dictionary<string, string>> query(string query, Dictionary<string, string> dic = null)
        {
            init();
            SqlConnection connection = new SqlConnection(AbstractDao.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.Prepare();
            if (dic != null)
            {
                foreach (KeyValuePair<string, string> entry in dic)
                {
                    double d;
                    if (Double.TryParse(entry.Value, out d))
                        cmd.Parameters.AddWithValue(entry.Key, d);
                    else
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }
            }
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(readerToDic(reader));
            }
            return list;
        }

        public static void exec(string query, Dictionary<string, string> dic = null)
        {
            init();
            SqlConnection connection = new SqlConnection(AbstractDao.ConnectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction(""SampleTransaction"");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandText = query;
                if (dic != null)
                {
                    cmd.Prepare();
                    foreach (KeyValuePair<string, string> entry in dic)
                    {
                        double d;
                        if (Double.TryParse(entry.Value, out d))
                            cmd.Parameters.AddWithValue(entry.Key, d);
                        else
                            cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }
                }
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception e1)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception e2)
                {
                    throw;
                }
            }
        }

        
        public static Dictionary<string, string> readerToDic(SqlDataReader reader)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            CultureInfo culture = new CultureInfo(""fr-FR"");
            int count = reader.FieldCount;
            for (int i = 0; i < count; i++)
            {
                string key = reader.GetName(i);
                string value = """";
                bool isNull = reader.IsDBNull(i);
                if (isNull == true)
                {
                    value = null;
                }
                else
                {
                    string type = reader.GetDataTypeName(i);
                    switch (type)
                    {
                        case ""datetime"":
                            value = reader.GetDateTime(i).ToString(culture);
                            break;
                        case ""varchar"":
                            value = reader.GetString(i);
                            break;
                        case ""int"":
                            value = reader.GetInt32(i).ToString();
                            break;
                        case ""smallint"":
                            value = reader.GetInt16(i).ToString();
                            break;
                        case ""bigint"":
                            value = reader.GetInt64(i).ToString();
                            break;
                        case ""decimal"":
                            value = reader.GetDecimal(i).ToString();
                            break;
                        case ""text"":
                            value = reader.GetString(i).ToString();
                            break;
                        case ""float"":
                            value = reader.GetDouble(i).ToString();
                            break;
                        case ""date"":
                            value = reader.GetDateTime(i).ToString(culture);
                            break;
                        case ""bit"":
                            value = reader.GetBoolean(i).ToString();
                            break;
                        default:
                            break;
                    }
                    if (value == """")
                    {
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
                                        try
                                        {
                                            value = reader.GetDateTime(i).ToString(culture);
                                        }
                                        catch (Exception)
                                        {
                                            try
                                            {
                                                value = reader.GetDecimal(i).ToString();
                                            }
                                            catch (Exception)
                                            {

                                                throw;
                                            }
                                        }
                                    }
                                }
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

";
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