using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class InfoAssoArticleSourceSetDao : AbstractDao
    {
        public InfoAssoArticleSourceSetDao() : base()
        {
            
        }

        
        public static InfoAssoArticleSourceSet create(InfoAssoArticleSourceSet infoAssoArticleSourceSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.InfoAssoArticleSourceSet.Add(infoAssoArticleSourceSet);
                AbstractDao.db.SaveChanges();
                return infoAssoArticleSourceSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    InfoAssoArticleSourceSet infoAssoArticleSourceSet = AbstractDao.db.InfoAssoArticleSourceSet.Find(Id);
                    AbstractDao.db.InfoAssoArticleSourceSet.Remove(infoAssoArticleSourceSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(InfoAssoArticleSourceSet infoAssoArticleSourceSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.InfoAssoArticleSourceSet.Attach(infoAssoArticleSourceSet);
                AbstractDao.db.Entry(infoAssoArticleSourceSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static InfoAssoArticleSourceSet find(int Id)
        {
            InfoAssoArticleSourceSet infoAssoArticleSourceSet = null;
            lock(AbstractDao.dbLock)
            {
                infoAssoArticleSourceSet = (from tmp in AbstractDao.db.InfoAssoArticleSourceSet
                                where tmp.Id_InfoAssoArticleSource == Id
                                select tmp).FirstOrDefault();
            }
            return infoAssoArticleSourceSet;
        }

        public static List<InfoAssoArticleSourceSet> findAll()
        {
            List<InfoAssoArticleSourceSet> list = new List<InfoAssoArticleSourceSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.InfoAssoArticleSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<InfoAssoArticleSourceSet> findByIdAssoArticleSourceSet(int IdAssoArticleSourceSet)
        {
            List<InfoAssoArticleSourceSet> list = new List<InfoAssoArticleSourceSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.InfoAssoArticleSourceSet
                    where tmp.Id_AssoArticleSource == IdAssoArticleSourceSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}