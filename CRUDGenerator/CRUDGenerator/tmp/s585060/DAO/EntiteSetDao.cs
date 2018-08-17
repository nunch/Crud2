using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class EntiteSetDao : AbstractDao
    {
        public EntiteSetDao() : base()
        {
            
        }

        
        public static EntiteSet create(EntiteSet entiteSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.EntiteSet.Add(entiteSet);
                AbstractDao.db.SaveChanges();
                return entiteSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    EntiteSet entiteSet = AbstractDao.db.EntiteSet.Find(Id);
                    AbstractDao.db.EntiteSet.Remove(entiteSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(EntiteSet entiteSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.EntiteSet.Attach(entiteSet);
                AbstractDao.db.Entry(entiteSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static EntiteSet find(int Id)
        {
            EntiteSet entiteSet = null;
            lock(AbstractDao.dbLock)
            {
                entiteSet = (from tmp in AbstractDao.db.EntiteSet
                                where tmp.Id_Entite == Id
                                select tmp).FirstOrDefault();
            }
            return entiteSet;
        }

        public static List<EntiteSet> findAll()
        {
            List<EntiteSet> list = new List<EntiteSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.EntiteSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}