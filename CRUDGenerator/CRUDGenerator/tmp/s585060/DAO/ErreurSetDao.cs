using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class ErreurSetDao : AbstractDao
    {
        public ErreurSetDao() : base()
        {
            
        }

        
        public static ErreurSet create(ErreurSet erreurSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ErreurSet.Add(erreurSet);
                AbstractDao.db.SaveChanges();
                return erreurSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    ErreurSet erreurSet = AbstractDao.db.ErreurSet.Find(Id);
                    AbstractDao.db.ErreurSet.Remove(erreurSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(ErreurSet erreurSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ErreurSet.Attach(erreurSet);
                AbstractDao.db.Entry(erreurSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static ErreurSet find(int Id)
        {
            ErreurSet erreurSet = null;
            lock(AbstractDao.dbLock)
            {
                erreurSet = (from tmp in AbstractDao.db.ErreurSet
                                where tmp.Id_Erreur == Id
                                select tmp).FirstOrDefault();
            }
            return erreurSet;
        }

        public static List<ErreurSet> findAll()
        {
            List<ErreurSet> list = new List<ErreurSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.ErreurSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<ErreurSet> findByIdFichierSet(int IdFichierSet)
        {
            List<ErreurSet> list = new List<ErreurSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.ErreurSet
                    where tmp.Id_Fichier == IdFichierSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}