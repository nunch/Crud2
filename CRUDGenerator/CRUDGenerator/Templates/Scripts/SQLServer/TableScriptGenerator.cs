using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUDGenerator.Database;

namespace CRUDGenerator.Templates.Scripts.SQLServer
{
    public class TableScriptGenerator
    {
        public static string getTableScript(Table table, out string fkString)
        {
            string initColumns = "";
            string primaryKey = "";
            string uniqueKeys = "";
            string indexes = "";
            string foreignKeys = "";
            int i = 0;
            foreach (Column column in table.Columns)
            {
                if (i == 0)
                {
                    initColumns = getColumnScript(column);
                }
                else
                {
                    initColumns += @",
    " + getColumnScript(column);
                }
                i++;
            }
			i = 0;
			string primaryKeyInside = "";
            foreach (Column column in table.getPrimaryKeys())
            {
                if (i == 0)
					primaryKeyInside = "[" + column.Name + "]";
                else
					primaryKeyInside += ", [" + column.Name + "]";
                i++;
			}
            if (primaryKeyInside != "")
			    primaryKey = "ALTER TABLE ["+table.Name+"] ADD CONSTRAINT pk_"+table.Name+" PRIMARY KEY ("+primaryKeyInside+");";
            i = 0;
            int j = 0;
			foreach (UniqueKey uk in table.UniqueKeys)
            {
                j = 0;
                string tmp = "";
				foreach (string columnName in uk.List)
                {
                    if (j == 0)
						tmp = "[" + columnName + "]";
                    else
                        tmp += ", [" + columnName + "]";
                    j++;
                }
                i++;
				uniqueKeys += @"
ALTER TABLE [" + table.Name + @"]
    ADD CONSTRAINT " + uk.Name + " UNIQUE ("+tmp+");";
            }
            i = 0;
			foreach (Index index in table.Indexes)
            {
                j = 0;
                string inside = "";
				foreach (string columnName in index.List)
                {
                    if (j == 0)
						inside += "[" + columnName + "]";
                    else
						inside += ", [" + columnName + "]";
                    j++;
                }
                string tmp = @"
CREATE INDEX " + index.Name + @"
    ON [" + table.Name + "] (" + inside + ");";
                i++;
                indexes += tmp;
            }
            i = 0;
            foreach (ForeignKey fk in table.ForeignKeys)
            {
                j = 0;
                string tmp_fk = "";
                string references = "";

                foreach (ForeignKeyInside fki in fk.Info)
                {
                    if (j == 0)
                    {
                        tmp_fk = "[" + fki.ColumnName + "]";
                        references = "[" + fki.ExternColumnName + "]";
                    }
                    else
                    {
                        tmp_fk += ", [" + fki.ColumnName + "]";
                        references += ", [" + fki.ExternColumnName + "]";
                    }
                    j++;
                }

                string tmp = @"
ALTER TABLE [" + table.Name + @"]
    ADD CONSTRAINT fk_" + table.Name + "_" + i + @"
    FOREIGN KEY (" + tmp_fk + @")
    REFERENCES [" + fk.ExternTableName + "](" + references + ");";
                foreignKeys += tmp;
                i++;
            }

            string res = @"
go
CREATE TABLE [dbo].[" + table.Name + @"](
    " + initColumns + @"
);

" + primaryKey + @"" + indexes + @"" + uniqueKeys + @"" +  @"
go
";
            fkString = foreignKeys;
            return res;
        }

        private static string getColumnScript(Column column)
        {
            string isNull = (column.isNull == 0) ? "NOT NULL" : "";
            string res = "";
            switch (column.Type)
            {
                case CRUDGenerator.Database.Type.Int:
                    res = "[" + column.Name + "] INT " + isNull;
                    break;
                case CRUDGenerator.Database.Type.Int_AI:
                    res = "[" + column.Name + "] INT IDENTITY(1, 1) " + isNull;
                    break;
                case CRUDGenerator.Database.Type.Boolean:
                    res = "[" + column.Name + "] BIT " + isNull;
                    break;
                case CRUDGenerator.Database.Type.String:
                    res = "[" + column.Name + "] VARCHAR(" + ((column.Length > 0) ? column.Length.ToString(): "MAX") + ") " + isNull;
                    break;
                case CRUDGenerator.Database.Type.Text:
                    res = "[" + column.Name + "] VARCHAR(MAX) " + isNull;
                    break;
                case CRUDGenerator.Database.Type.Float:
                    res = "[" + column.Name + "] FLOAT " + isNull;
                    break;
                case CRUDGenerator.Database.Type.Datetime:
                    res = "[" + column.Name + "] DATETIME " + isNull;
                    break;
                case CRUDGenerator.Database.Type.Date:
                    res = "[" + column.Name + "] DATE " + isNull;
                    break;
                case CRUDGenerator.Database.Type.Decimal:
                    res = "[" + column.Name + "] DECIMAL(" + column.Length + ", " + column.DecimalLength + ") " + isNull;
                    break;
                case CRUDGenerator.Database.Type.Tinyint:
                    res = "[" + column.Name + "] TINYINT " + isNull;
                    break;
                case CRUDGenerator.Database.Type.Smallint:
                    res = "[" + column.Name + "] SMALLINT " + isNull;
                    break;
                case CRUDGenerator.Database.Type.Bigint:
                    res = "[" + column.Name + "] BIGINT " + isNull;
                    break;
                default:
                    break;
            }
            return res;
        }
    }
}