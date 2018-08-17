using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class ConditionSetDao : AbstractDao
    {
        public ConditionSetDao() : base()
        {
            
        }

        
        public static ConditionSet create(ConditionSet conditionSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ConditionSet.Add(conditionSet);
                AbstractDao.db.SaveChanges();
                return conditionSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    ConditionSet conditionSet = AbstractDao.db.ConditionSet.Find(Id);
                    AbstractDao.db.ConditionSet.Remove(conditionSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(ConditionSet conditionSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ConditionSet.Attach(conditionSet);
                AbstractDao.db.Entry(conditionSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static ConditionSet find(int Id)
        {
            ConditionSet conditionSet = null;
            lock(AbstractDao.dbLock)
            {
                conditionSet = (from tmp in AbstractDao.db.ConditionSet
                                where tmp.Id_Condition == Id
                                select tmp).FirstOrDefault();
            }
            return conditionSet;
        }

        public static List<ConditionSet> findAll()
        {
            List<ConditionSet> list = new List<ConditionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.ConditionSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<ConditionSet> findByIdExceptionStatutSet(int IdExceptionStatutSet)
        {
            List<ConditionSet> list = new List<ConditionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.ConditionSet
                    where tmp.Id_ExceptionStatut == IdExceptionStatutSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}