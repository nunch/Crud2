using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class ActionSetDao : AbstractDao
    {
        public ActionSetDao() : base()
        {
            
        }

        
        public static ActionSet create(ActionSet actionSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ActionSet.Add(actionSet);
                AbstractDao.db.SaveChanges();
                return actionSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    ActionSet actionSet = AbstractDao.db.ActionSet.Find(Id);
                    AbstractDao.db.ActionSet.Remove(actionSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(ActionSet actionSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ActionSet.Attach(actionSet);
                AbstractDao.db.Entry(actionSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static ActionSet find(int Id)
        {
            ActionSet actionSet = null;
            lock(AbstractDao.dbLock)
            {
                actionSet = (from tmp in AbstractDao.db.ActionSet
                                where tmp.Id_Action == Id
                                select tmp).FirstOrDefault();
            }
            return actionSet;
        }

        public static List<ActionSet> findAll()
        {
            List<ActionSet> list = new List<ActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.ActionSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<ActionSet> findByIdEntiteSet(int IdEntiteSet)
        {
            List<ActionSet> list = new List<ActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.ActionSet
                    where tmp.Id_Entite == IdEntiteSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}