using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class UtilisateurSetDao : AbstractDao
    {
        public UtilisateurSetDao() : base()
        {
            
        }

        
        public static UtilisateurSet create(UtilisateurSet utilisateurSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.UtilisateurSet.Add(utilisateurSet);
                AbstractDao.db.SaveChanges();
                return utilisateurSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    UtilisateurSet utilisateurSet = AbstractDao.db.UtilisateurSet.Find(Id);
                    AbstractDao.db.UtilisateurSet.Remove(utilisateurSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(UtilisateurSet utilisateurSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.UtilisateurSet.Attach(utilisateurSet);
                AbstractDao.db.Entry(utilisateurSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static UtilisateurSet find(int Id)
        {
            UtilisateurSet utilisateurSet = null;
            lock(AbstractDao.dbLock)
            {
                utilisateurSet = (from tmp in AbstractDao.db.UtilisateurSet
                                where tmp.Id_Utilisateur == Id
                                select tmp).FirstOrDefault();
            }
            return utilisateurSet;
        }

        public static List<UtilisateurSet> findAll()
        {
            List<UtilisateurSet> list = new List<UtilisateurSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.UtilisateurSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<UtilisateurSet> findByIdEntiteSet(int IdEntiteSet)
        {
            List<UtilisateurSet> list = new List<UtilisateurSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.UtilisateurSet
                    where tmp.Id_Entite == IdEntiteSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}