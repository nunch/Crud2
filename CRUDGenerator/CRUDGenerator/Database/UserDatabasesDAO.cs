using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;

namespace CRUDGenerator.Database
{
    public class UserDatabasesDAO
    {
        public void create(UserDatabases ud)
        {
            update(ud);
        }

        public void update(UserDatabases ud)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "XMLFiles\\" + ud.Matricule + ".xml";
            XmlSerializer xs = new XmlSerializer(typeof(UserDatabases));
            using (StreamWriter wr = new StreamWriter(filePath))
            {
				xs.Serialize(wr, ud);
            }
        }

        public UserDatabases findByMatricule(string matricule)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "XMLFiles\\" + matricule + ".xml";
            XmlSerializer xs = new XmlSerializer(typeof(UserDatabases));
            if (File.Exists(filePath))
            {
                using (StreamReader wr = new StreamReader(filePath))
                {
                    return xs.Deserialize(wr) as UserDatabases;
                }
            }
            UserDatabases userDatabases = new UserDatabases
            {
                Matricule = matricule,
                Databases = new List<DatabaseXML>()
            };
            using (StreamWriter sr = new StreamWriter(filePath))
            {
                xs.Serialize(sr, userDatabases);
            }
            return userDatabases;
        }
    }
}