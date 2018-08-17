using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class ArticleSetDao : AbstractDao
    {
        public ArticleSetDao() : base()
        {
            
        }

        
        public static ArticleSet create(ArticleSet articleSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ArticleSet.Add(articleSet);
                AbstractDao.db.SaveChanges();
                return articleSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    ArticleSet articleSet = AbstractDao.db.ArticleSet.Find(Id);
                    AbstractDao.db.ArticleSet.Remove(articleSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(ArticleSet articleSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.ArticleSet.Attach(articleSet);
                AbstractDao.db.Entry(articleSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static ArticleSet find(int Id)
        {
            ArticleSet articleSet = null;
            lock(AbstractDao.dbLock)
            {
                articleSet = (from tmp in AbstractDao.db.ArticleSet
                                where tmp.Id_Article == Id
                                select tmp).FirstOrDefault();
            }
            return articleSet;
        }

        public static List<ArticleSet> findAll()
        {
            List<ArticleSet> list = new List<ArticleSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.ArticleSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}