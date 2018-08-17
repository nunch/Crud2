using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class SourceSetDao : AbstractDao
    {
        public SourceSetDao() : base()
        {
            
        }

        
        public static SourceSet create(SourceSet sourceSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.SourceSet.Add(sourceSet);
                AbstractDao.db.SaveChanges();
                return sourceSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    SourceSet sourceSet = AbstractDao.db.SourceSet.Find(Id);
                    AbstractDao.db.SourceSet.Remove(sourceSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(SourceSet sourceSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.SourceSet.Attach(sourceSet);
                AbstractDao.db.Entry(sourceSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static SourceSet find(int Id)
        {
            SourceSet sourceSet = null;
            lock(AbstractDao.dbLock)
            {
                sourceSet = (from tmp in AbstractDao.db.SourceSet
                                where tmp.Id_Source == Id
                                select tmp).FirstOrDefault();
            }
            return sourceSet;
        }

        public static List<SourceSet> findAll()
        {
            List<SourceSet> list = new List<SourceSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.SourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<SourceSet> findByIdEcartSet(int IdEcartSet)
        {
            List<SourceSet> list = new List<SourceSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.SourceSet
                    where tmp.EcartSource_Source_Id_Ecart == IdEcartSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}