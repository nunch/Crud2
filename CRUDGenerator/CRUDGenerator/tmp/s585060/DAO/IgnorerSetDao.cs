using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class IgnorerSetDao : AbstractDao
    {
        public IgnorerSetDao() : base()
        {
            
        }

        
        public static IgnorerSet create(IgnorerSet ignorerSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.IgnorerSet.Add(ignorerSet);
                AbstractDao.db.SaveChanges();
                return ignorerSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    IgnorerSet ignorerSet = AbstractDao.db.IgnorerSet.Find(Id);
                    AbstractDao.db.IgnorerSet.Remove(ignorerSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(IgnorerSet ignorerSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.IgnorerSet.Attach(ignorerSet);
                AbstractDao.db.Entry(ignorerSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static IgnorerSet find(int Id)
        {
            IgnorerSet ignorerSet = null;
            lock(AbstractDao.dbLock)
            {
                ignorerSet = (from tmp in AbstractDao.db.IgnorerSet
                                where tmp.Id_Ignorer == Id
                                select tmp).FirstOrDefault();
            }
            return ignorerSet;
        }

        public static List<IgnorerSet> findAll()
        {
            List<IgnorerSet> list = new List<IgnorerSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.IgnorerSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<IgnorerSet> findByIdEcartSet(int IdEcartSet)
        {
            List<IgnorerSet> list = new List<IgnorerSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.IgnorerSet
                    where tmp.Id_Ecart == IdEcartSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}