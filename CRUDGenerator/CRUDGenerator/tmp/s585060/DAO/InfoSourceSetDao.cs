using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class InfoSourceSetDao : AbstractDao
    {
        public InfoSourceSetDao() : base()
        {
            
        }

        
        public static InfoSourceSet create(InfoSourceSet infoSourceSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.InfoSourceSet.Add(infoSourceSet);
                AbstractDao.db.SaveChanges();
                return infoSourceSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    InfoSourceSet infoSourceSet = AbstractDao.db.InfoSourceSet.Find(Id);
                    AbstractDao.db.InfoSourceSet.Remove(infoSourceSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(InfoSourceSet infoSourceSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.InfoSourceSet.Attach(infoSourceSet);
                AbstractDao.db.Entry(infoSourceSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static InfoSourceSet find(int Id)
        {
            InfoSourceSet infoSourceSet = null;
            lock(AbstractDao.dbLock)
            {
                infoSourceSet = (from tmp in AbstractDao.db.InfoSourceSet
                                where tmp.Id_InfoSource == Id
                                select tmp).FirstOrDefault();
            }
            return infoSourceSet;
        }

        public static List<InfoSourceSet> findAll()
        {
            List<InfoSourceSet> list = new List<InfoSourceSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.InfoSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<InfoSourceSet> findByIdSourceSet(int IdSourceSet)
        {
            List<InfoSourceSet> list = new List<InfoSourceSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.InfoSourceSet
                    where tmp.Id_Source == IdSourceSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}