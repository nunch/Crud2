using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class RapportIntegrationSetDao : AbstractDao
    {
        public RapportIntegrationSetDao() : base()
        {
            
        }

        
        public static RapportIntegrationSet create(RapportIntegrationSet rapportIntegrationSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.RapportIntegrationSet.Add(rapportIntegrationSet);
                AbstractDao.db.SaveChanges();
                return rapportIntegrationSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    RapportIntegrationSet rapportIntegrationSet = AbstractDao.db.RapportIntegrationSet.Find(Id);
                    AbstractDao.db.RapportIntegrationSet.Remove(rapportIntegrationSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(RapportIntegrationSet rapportIntegrationSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.RapportIntegrationSet.Attach(rapportIntegrationSet);
                AbstractDao.db.Entry(rapportIntegrationSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static RapportIntegrationSet find(int Id)
        {
            RapportIntegrationSet rapportIntegrationSet = null;
            lock(AbstractDao.dbLock)
            {
                rapportIntegrationSet = (from tmp in AbstractDao.db.RapportIntegrationSet
                                where tmp.Id_RapportIntegration == Id
                                select tmp).FirstOrDefault();
            }
            return rapportIntegrationSet;
        }

        public static List<RapportIntegrationSet> findAll()
        {
            List<RapportIntegrationSet> list = new List<RapportIntegrationSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.RapportIntegrationSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}