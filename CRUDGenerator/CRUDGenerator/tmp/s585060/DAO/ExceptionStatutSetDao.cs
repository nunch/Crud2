using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class ExceptionStatutSetDao : AbstractDao
    {
        public ExceptionStatutSetDao() : base()
        {
            
        }

        
        public static ExceptionStatutSet create(ExceptionStatutSet exceptionStatutSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ExceptionStatutSet.Add(exceptionStatutSet);
                AbstractDao.db.SaveChanges();
                return exceptionStatutSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    ExceptionStatutSet exceptionStatutSet = AbstractDao.db.ExceptionStatutSet.Find(Id);
                    AbstractDao.db.ExceptionStatutSet.Remove(exceptionStatutSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(ExceptionStatutSet exceptionStatutSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ExceptionStatutSet.Attach(exceptionStatutSet);
                AbstractDao.db.Entry(exceptionStatutSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static ExceptionStatutSet find(int Id)
        {
            ExceptionStatutSet exceptionStatutSet = null;
            lock(AbstractDao.dbLock)
            {
                exceptionStatutSet = (from tmp in AbstractDao.db.ExceptionStatutSet
                                where tmp.Id_ExceptionStatut == Id
                                select tmp).FirstOrDefault();
            }
            return exceptionStatutSet;
        }

        public static List<ExceptionStatutSet> findAll()
        {
            List<ExceptionStatutSet> list = new List<ExceptionStatutSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.ExceptionStatutSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}