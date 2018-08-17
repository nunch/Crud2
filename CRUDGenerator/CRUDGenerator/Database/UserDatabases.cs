using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDGenerator.Database
{
    public class UserDatabases
    {
        public string Matricule { get; set; }
        public List<DatabaseXML> Databases { get; set; }

        public DatabaseXML getDatabaseByName(string name)
        {
            foreach (DatabaseXML db in this.Databases)
            {
                if (db.Name == name)
                    return db;
            }
            return null;
        }

		public bool accept(ValidationVisitor visitor){
			return visitor.visiteUserDatabases (this);
		}

        public void removeDatabaseByName(string name)
        {
            Databases.Remove(getDatabaseByName(name));
        }
    }
}