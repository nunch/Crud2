using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class EcartSetDao : AbstractDao
    {
        public EcartSetDao() : base()
        {
            
        }

        
        public static EcartSet create(EcartSet ecartSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.EcartSet.Add(ecartSet);
                AbstractDao.db.SaveChanges();
                return ecartSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    EcartSet ecartSet = AbstractDao.db.EcartSet.Find(Id);
                    AbstractDao.db.EcartSet.Remove(ecartSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(EcartSet ecartSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.EcartSet.Attach(ecartSet);
                AbstractDao.db.Entry(ecartSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static EcartSet find(int Id)
        {
            EcartSet ecartSet = null;
            lock(AbstractDao.dbLock)
            {
                ecartSet = (from tmp in AbstractDao.db.EcartSet
                                where tmp.Id_Ecart == Id
                                select tmp).FirstOrDefault();
            }
            return ecartSet;
        }

        public static List<EcartSet> findAll()
        {
            List<EcartSet> list = new List<EcartSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.EcartSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<EcartSet> findByIdArticleSet(int IdArticleSet)
        {
            List<EcartSet> list = new List<EcartSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.EcartSet
                    where tmp.Article_Id_Article == IdArticleSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<EcartSet> findByIdAssoArticleSourceSet(int IdAssoArticleSourceSet)
        {
            List<EcartSet> list = new List<EcartSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.EcartSet
                    where tmp.AssoArticleSource_Id_AssoArticleSource == IdAssoArticleSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<EcartSet> findByIdArticleSet_IdAssoArticleSourceSet(int IdArticleSet, int IdAssoArticleSourceSet)
        {
            List<EcartSet> list = new List<EcartSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.EcartSet
                    where tmp.Article_Id_Article == IdArticleSet && tmp.AssoArticleSource_Id_AssoArticleSource == IdAssoArticleSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<EcartSet> findByIdLienArticlesSet(int IdLienArticlesSet)
        {
            List<EcartSet> list = new List<EcartSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.EcartSet
                    where tmp.LienArticles_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<EcartSet> findByIdArticleSet_IdLienArticlesSet(int IdArticleSet, int IdLienArticlesSet)
        {
            List<EcartSet> list = new List<EcartSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.EcartSet
                    where tmp.Article_Id_Article == IdArticleSet && tmp.LienArticles_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<EcartSet> findByIdAssoArticleSourceSet_IdLienArticlesSet(int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<EcartSet> list = new List<EcartSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.EcartSet
                    where tmp.AssoArticleSource_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.LienArticles_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<EcartSet> findByIdArticleSet_IdAssoArticleSourceSet_IdLienArticlesSet(int IdArticleSet, int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<EcartSet> list = new List<EcartSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.EcartSet
                    where tmp.Article_Id_Article == IdArticleSet && tmp.AssoArticleSource_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.LienArticles_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}