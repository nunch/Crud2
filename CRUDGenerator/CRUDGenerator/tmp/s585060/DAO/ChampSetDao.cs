using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class ChampSetDao : AbstractDao
    {
        public ChampSetDao() : base()
        {
            
        }

        
        public static ChampSet create(ChampSet champSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ChampSet.Add(champSet);
                AbstractDao.db.SaveChanges();
                return champSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    ChampSet champSet = AbstractDao.db.ChampSet.Find(Id);
                    AbstractDao.db.ChampSet.Remove(champSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(ChampSet champSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ChampSet.Attach(champSet);
                AbstractDao.db.Entry(champSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static ChampSet find(int Id)
        {
            ChampSet champSet = null;
            lock(AbstractDao.dbLock)
            {
                champSet = (from tmp in AbstractDao.db.ChampSet
                                where tmp.Id_Champ == Id
                                select tmp).FirstOrDefault();
            }
            return champSet;
        }

        public static List<ChampSet> findAll()
        {
            List<ChampSet> list = new List<ChampSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.ChampSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}