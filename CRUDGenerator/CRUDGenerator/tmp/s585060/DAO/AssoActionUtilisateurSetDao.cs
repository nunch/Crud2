using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class AssoActionUtilisateurSetDao : AbstractDao
    {
        public AssoActionUtilisateurSetDao() : base()
        {
            
        }

        
        public static AssoActionUtilisateurSet create(AssoActionUtilisateurSet assoActionUtilisateurSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.AssoActionUtilisateurSet.Add(assoActionUtilisateurSet);
                AbstractDao.db.SaveChanges();
                return assoActionUtilisateurSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    AssoActionUtilisateurSet assoActionUtilisateurSet = AbstractDao.db.AssoActionUtilisateurSet.Find(Id);
                    AbstractDao.db.AssoActionUtilisateurSet.Remove(assoActionUtilisateurSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(AssoActionUtilisateurSet assoActionUtilisateurSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.AssoActionUtilisateurSet.Attach(assoActionUtilisateurSet);
                AbstractDao.db.Entry(assoActionUtilisateurSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static AssoActionUtilisateurSet find(int Id)
        {
            AssoActionUtilisateurSet assoActionUtilisateurSet = null;
            lock(AbstractDao.dbLock)
            {
                assoActionUtilisateurSet = (from tmp in AbstractDao.db.AssoActionUtilisateurSet
                                where tmp.Id_AssoActionUtilisateur == Id
                                select tmp).FirstOrDefault();
            }
            return assoActionUtilisateurSet;
        }

        public static List<AssoActionUtilisateurSet> findAll()
        {
            List<AssoActionUtilisateurSet> list = new List<AssoActionUtilisateurSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoActionUtilisateurSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoActionUtilisateurSet> findByIdActionSet(int IdActionSet)
        {
            List<AssoActionUtilisateurSet> list = new List<AssoActionUtilisateurSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoActionUtilisateurSet
                    where tmp.Id_Action == IdActionSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoActionUtilisateurSet> findByIdUtilisateurSet(int IdUtilisateurSet)
        {
            List<AssoActionUtilisateurSet> list = new List<AssoActionUtilisateurSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoActionUtilisateurSet
                    where tmp.Id_Utilisateur == IdUtilisateurSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoActionUtilisateurSet> findByIdActionSet_IdUtilisateurSet(int IdActionSet, int IdUtilisateurSet)
        {
            List<AssoActionUtilisateurSet> list = new List<AssoActionUtilisateurSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoActionUtilisateurSet
                    where tmp.Id_Action == IdActionSet && tmp.Id_Utilisateur == IdUtilisateurSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}