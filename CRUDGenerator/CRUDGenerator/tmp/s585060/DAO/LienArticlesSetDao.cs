using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class LienArticlesSetDao : AbstractDao
    {
        public LienArticlesSetDao() : base()
        {
            
        }

        
        public static LienArticlesSet create(LienArticlesSet lienArticlesSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.LienArticlesSet.Add(lienArticlesSet);
                AbstractDao.db.SaveChanges();
                return lienArticlesSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    LienArticlesSet lienArticlesSet = AbstractDao.db.LienArticlesSet.Find(Id);
                    AbstractDao.db.LienArticlesSet.Remove(lienArticlesSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(LienArticlesSet lienArticlesSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.LienArticlesSet.Attach(lienArticlesSet);
                AbstractDao.db.Entry(lienArticlesSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static LienArticlesSet find(int Id)
        {
            LienArticlesSet lienArticlesSet = null;
            lock(AbstractDao.dbLock)
            {
                lienArticlesSet = (from tmp in AbstractDao.db.LienArticlesSet
                                where tmp.Id_LienArticles == Id
                                select tmp).FirstOrDefault();
            }
            return lienArticlesSet;
        }

        public static List<LienArticlesSet> findAll()
        {
            List<LienArticlesSet> list = new List<LienArticlesSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.LienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<LienArticlesSet> findByIdArticleSet(int IdArticleSet)
        {
            List<LienArticlesSet> list = new List<LienArticlesSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.LienArticlesSet
                    where tmp.Id_ArticleParent == IdArticleSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<LienArticlesSet> findByIdArticleSet(int IdArticleSet)
        {
            List<LienArticlesSet> list = new List<LienArticlesSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.LienArticlesSet
                    where tmp.Id_ArticleEnfant == IdArticleSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<LienArticlesSet> findByIdArticleSet_IdArticleSet(int IdArticleSet, int IdArticleSet)
        {
            List<LienArticlesSet> list = new List<LienArticlesSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.LienArticlesSet
                    where tmp.Id_ArticleParent == IdArticleSet && tmp.Id_ArticleEnfant == IdArticleSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}