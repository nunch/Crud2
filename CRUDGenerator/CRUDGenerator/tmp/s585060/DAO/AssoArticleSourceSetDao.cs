using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class AssoArticleSourceSetDao : AbstractDao
    {
        public AssoArticleSourceSetDao() : base()
        {
            
        }

        
        public static AssoArticleSourceSet create(AssoArticleSourceSet assoArticleSourceSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.AssoArticleSourceSet.Add(assoArticleSourceSet);
                AbstractDao.db.SaveChanges();
                return assoArticleSourceSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    AssoArticleSourceSet assoArticleSourceSet = AbstractDao.db.AssoArticleSourceSet.Find(Id);
                    AbstractDao.db.AssoArticleSourceSet.Remove(assoArticleSourceSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(AssoArticleSourceSet assoArticleSourceSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.AssoArticleSourceSet.Attach(assoArticleSourceSet);
                AbstractDao.db.Entry(assoArticleSourceSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static AssoArticleSourceSet find(int Id)
        {
            AssoArticleSourceSet assoArticleSourceSet = null;
            lock(AbstractDao.dbLock)
            {
                assoArticleSourceSet = (from tmp in AbstractDao.db.AssoArticleSourceSet
                                where tmp.Id_AssoArticleSource == Id
                                select tmp).FirstOrDefault();
            }
            return assoArticleSourceSet;
        }

        public static List<AssoArticleSourceSet> findAll()
        {
            List<AssoArticleSourceSet> list = new List<AssoArticleSourceSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoArticleSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoArticleSourceSet> findByIdArticleSet(int IdArticleSet)
        {
            List<AssoArticleSourceSet> list = new List<AssoArticleSourceSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoArticleSourceSet
                    where tmp.Id_Article == IdArticleSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoArticleSourceSet> findByIdSourceSet(int IdSourceSet)
        {
            List<AssoArticleSourceSet> list = new List<AssoArticleSourceSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoArticleSourceSet
                    where tmp.Id_Source == IdSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoArticleSourceSet> findByIdArticleSet_IdSourceSet(int IdArticleSet, int IdSourceSet)
        {
            List<AssoArticleSourceSet> list = new List<AssoArticleSourceSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoArticleSourceSet
                    where tmp.Id_Article == IdArticleSet && tmp.Id_Source == IdSourceSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}