using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace CRUDGenerator.SQL
{
    public class HelperSQL
    {
        public static string ConnectionString { get; set; }

        public static void initConnectionString()
        {
            if (HelperSQL.ConnectionString == null)
            {
                HelperSQL.ConnectionString = ConfigurationManager.ConnectionStrings["mssqlConnection"].ConnectionString;
            }
        }

        public static string getConnectionString()
        {
            initConnectionString();
            return HelperSQL.ConnectionString;
        }

        public static List<Dictionary<string, string>> query(string query, Dictionary<string, string> dic = null)
        {
            initConnectionString();
            SqlConnection connection = new SqlConnection(HelperSQL.ConnectionString);
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

        public static string exec(string query, Dictionary<string, string> dic = null)
        {
            initConnectionString();
            SqlConnection connection = new SqlConnection(HelperSQL.ConnectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction("SampleTransaction");
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
                    return e1.Message;
                }
                catch (Exception e2)
                {
                    throw;
                }
            }
            return "OK";
        }

        public static Dictionary<string, string> readerToDic(SqlDataReader reader)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            CultureInfo culture = new CultureInfo("fr-FR");
            int count = reader.FieldCount;
            for (int i = 0; i < count; i++)
            {
                string key = reader.GetName(i);
                string value = "";
                bool isNull = reader.IsDBNull(i);
                if (isNull == true)
                {
                    value = null;
                }
                else
                {
                    string type = reader.GetDataTypeName(i);
                    bool taken = true;
                    switch (type)
                    {
                        case "datetime":
                            value = reader.GetDateTime(i).ToString(culture);
                            break;
                        case "varchar":
                        case "nvarchar":
                            value = reader.GetString(i);
                            break;
                        case "int":
                            value = reader.GetInt32(i).ToString();
                            break;
                        case "smallint":
                            value = reader.GetInt16(i).ToString();
                            break;
                        case "bigint":
                            value = reader.GetInt64(i).ToString();
                            break;
                        case "decimal":
                            value = reader.GetDecimal(i).ToString();
                            break;
                        case "text":
                            value = reader.GetString(i).ToString();
                            break;
                        case "float":
                            value = reader.GetDouble(i).ToString();
                            break;
                        case "date":
                            value = reader.GetDateTime(i).ToString(culture);
                            break;
                        case "bit":
                            value = reader.GetBoolean(i).ToString();
                            break;
                        case "tinyint":
                            value = reader.GetByte(i).ToString();
                            break;
                        default:
                            taken = false;
                            break;

                    }
                    if (!taken)
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