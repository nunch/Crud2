using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDGenerator.Database
{
    public class DatabaseXML
    {
        public string Name { get; set; }
        public List<Table> Content { get; set; }
        public string ConnectionString { get; set; }
		public DatabaseXML ()
		{
			Content = new List<Table> ();
		}

		public Table getTableByName(string tableName){
			foreach (Table table in Content) {
				if (table.Name == tableName)
					return table;
			}
			return null;
		}

        public void removeTableByName(string tableName)
        {
            List<Table> tmp = new List<Table>();
            foreach (Table table in this.Content)
            {
                if (table.Name != tableName)
                {
                    tmp.Add(table);
                    foreach (ForeignKey fk in table.ForeignKeys)
                    {
                        if (fk.ExternTableName == tableName)
                        {
                            throw new Exception("La table " + table.Name + " possède une clé étrangère sur la table à supprimer (" + tableName + ")");
                        }
                    }
                }
            }
            this.Content = tmp;
        }

		public bool accept(ValidationVisitor visitor){
			return visitor.visiteDatabaseXML (this);
		}

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            DatabaseXML db = (DatabaseXML)obj;

            if (this.Name != db.Name || this.Content != db.Content)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = 23 * hash + this.Content.GetHashCode();
            hash = 23 * hash + this.Name.GetHashCode();
            return hash;
        }
    }
}