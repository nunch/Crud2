using System;
using System.Collections.Generic;

namespace CRUDGenerator.Database
{
	public class ValidationVisitor
	{
        public List<string> errors { get; set; }
        
		public ValidationVisitor ()
		{
            errors = new List<string>();
		}

		public bool visiteUserDatabases(UserDatabases ud){
			foreach (DatabaseXML db in ud.Databases) {
				if (db.accept (this) == false)
					return false;
			}
			return true;
		}

		public bool visiteDatabaseXML(DatabaseXML db){
			foreach (Table table in db.Content) {
				if (table.accept (this, db) == false)
					return false;
			}
			return true;
		}

		public bool visiteTable(Table table, DatabaseXML db){
			foreach (Column column in table.Columns) {
				if (column.accept (this, table, db) == false)
					return false;
			}
			return true;
		}

		public bool visiteColumn(Column column, Table table, DatabaseXML db){
			ForeignKeyHelper fkh = table.getFKHFromColumn (column);
			if (fkh != null) {
                Table externTable;
                try
                {
				    externTable = db.getTableByName (fkh.ExternTableName);
                }
                catch (Exception e)
                {
                    errors.Add(e.Message+ " of the database "+db.Name);
                    return false;
                }
                Column externColumn;
                try 
	            {
		            externColumn = externTable.getColumnByName(fkh.ExternColumnName);
	            }
	            catch (Exception e)
	            {
                    errors.Add(e.Message + " of the database " + db.Name);
                    return false;
	            }
				if (column.Type == externColumn.Type && column.Length == externColumn.Length)
					return true;
				else {
                    string s = "Un problème de type de champ est apparue dans une clé étrangère entre le champ " + column.Name+" de la table "+table.Name+" et le champ "+externColumn.Name+" de la table "+externTable;
                    errors.Add(s);
					return false;
				}
			}
			return true;
		}
	}
}

