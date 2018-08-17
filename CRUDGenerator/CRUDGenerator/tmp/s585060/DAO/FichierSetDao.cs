using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class FichierSetDao : AbstractDao
    {
        public FichierSetDao() : base()
        {
            
        }

        
        public static FichierSet create(FichierSet fichierSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.FichierSet.Add(fichierSet);
                AbstractDao.db.SaveChanges();
                return fichierSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    FichierSet fichierSet = AbstractDao.db.FichierSet.Find(Id);
                    AbstractDao.db.FichierSet.Remove(fichierSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(FichierSet fichierSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.FichierSet.Attach(fichierSet);
                AbstractDao.db.Entry(fichierSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static FichierSet find(int Id)
        {
            FichierSet fichierSet = null;
            lock(AbstractDao.dbLock)
            {
                fichierSet = (from tmp in AbstractDao.db.FichierSet
                                where tmp.Id_Fichier == Id
                                select tmp).FirstOrDefault();
            }
            return fichierSet;
        }

        public static List<FichierSet> findAll()
        {
            List<FichierSet> list = new List<FichierSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.FichierSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<FichierSet> findByIdRapportIntegrationSet(int IdRapportIntegrationSet)
        {
            List<FichierSet> list = new List<FichierSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.FichierSet
                    where tmp.Id_RapportIntegration == IdRapportIntegrationSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}