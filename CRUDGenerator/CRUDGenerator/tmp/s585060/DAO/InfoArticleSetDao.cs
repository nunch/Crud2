using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class InfoArticleSetDao : AbstractDao
    {
        public InfoArticleSetDao() : base()
        {
            
        }

        
        public static InfoArticleSet create(InfoArticleSet infoArticleSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.InfoArticleSet.Add(infoArticleSet);
                AbstractDao.db.SaveChanges();
                return infoArticleSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    InfoArticleSet infoArticleSet = AbstractDao.db.InfoArticleSet.Find(Id);
                    AbstractDao.db.InfoArticleSet.Remove(infoArticleSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(InfoArticleSet infoArticleSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.InfoArticleSet.Attach(infoArticleSet);
                AbstractDao.db.Entry(infoArticleSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static InfoArticleSet find(int Id)
        {
            InfoArticleSet infoArticleSet = null;
            lock(AbstractDao.dbLock)
            {
                infoArticleSet = (from tmp in AbstractDao.db.InfoArticleSet
                                where tmp.Id_InfoArticle == Id
                                select tmp).FirstOrDefault();
            }
            return infoArticleSet;
        }

        public static List<InfoArticleSet> findAll()
        {
            List<InfoArticleSet> list = new List<InfoArticleSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.InfoArticleSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<InfoArticleSet> findByIdArticleSet(int IdArticleSet)
        {
            List<InfoArticleSet> list = new List<InfoArticleSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.InfoArticleSet
                    where tmp.Id_Article == IdArticleSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<InfoArticleSet> findByIdEntiteSet(int IdEntiteSet)
        {
            List<InfoArticleSet> list = new List<InfoArticleSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.InfoArticleSet
                    where tmp.Id_Entite == IdEntiteSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<InfoArticleSet> findByIdArticleSet_IdEntiteSet(int IdArticleSet, int IdEntiteSet)
        {
            List<InfoArticleSet> list = new List<InfoArticleSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.InfoArticleSet
                    where tmp.Id_Article == IdArticleSet && tmp.Id_Entite == IdEntiteSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}