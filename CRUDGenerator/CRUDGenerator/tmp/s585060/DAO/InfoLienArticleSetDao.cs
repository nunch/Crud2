using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class InfoLienArticleSetDao : AbstractDao
    {
        public InfoLienArticleSetDao() : base()
        {
            
        }

        
        public static InfoLienArticleSet create(InfoLienArticleSet infoLienArticleSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.InfoLienArticleSet.Add(infoLienArticleSet);
                AbstractDao.db.SaveChanges();
                return infoLienArticleSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    InfoLienArticleSet infoLienArticleSet = AbstractDao.db.InfoLienArticleSet.Find(Id);
                    AbstractDao.db.InfoLienArticleSet.Remove(infoLienArticleSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(InfoLienArticleSet infoLienArticleSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.InfoLienArticleSet.Attach(infoLienArticleSet);
                AbstractDao.db.Entry(infoLienArticleSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static InfoLienArticleSet find(int Id)
        {
            InfoLienArticleSet infoLienArticleSet = null;
            lock(AbstractDao.dbLock)
            {
                infoLienArticleSet = (from tmp in AbstractDao.db.InfoLienArticleSet
                                where tmp.Id_InfoLienArticle == Id
                                select tmp).FirstOrDefault();
            }
            return infoLienArticleSet;
        }

        public static List<InfoLienArticleSet> findAll()
        {
            List<InfoLienArticleSet> list = new List<InfoLienArticleSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.InfoLienArticleSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<InfoLienArticleSet> findByIdLienArticlesSet(int IdLienArticlesSet)
        {
            List<InfoLienArticleSet> list = new List<InfoLienArticleSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.InfoLienArticleSet
                    where tmp.Id_LienArticle == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}