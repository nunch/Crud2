using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PerennisationSPI.Models;

namespace PerennisationSPI.Dao
{
    public class AssoCleActionSetDao : AbstractDao
    {
        public AssoCleActionSetDao() : base()
        {
            
        }

        
        public static AssoCleActionSet create(AssoCleActionSet assoCleActionSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.AssoCleActionSet.Add(assoCleActionSet);
                AbstractDao.db.SaveChanges();
                return assoCleActionSet;
            }
        }

        public static string remove(int Id)
        {
            string message = "";
            lock(AbstractDao.dbLock)
            {
                try
                {
                    AssoCleActionSet assoCleActionSet = AbstractDao.db.AssoCleActionSet.Find(Id);
                    AbstractDao.db.AssoCleActionSet.Remove(assoCleActionSet);
                    AbstractDao.db.SaveChanges();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return message;
        }

        public static void update(AssoCleActionSet assoCleActionSet)
        {
            lock(AbstractDao.dbLock)
            {
                AbstractDao.db.AssoCleActionSet.Attach(assoCleActionSet);
                AbstractDao.db.Entry(assoCleActionSet).State = System.Data.Entity.EntityState.Modified;     
                AbstractDao.db.SaveChanges();
            }
        }

        public static AssoCleActionSet find(int Id)
        {
            AssoCleActionSet assoCleActionSet = null;
            lock(AbstractDao.dbLock)
            {
                assoCleActionSet = (from tmp in AbstractDao.db.AssoCleActionSet
                                where tmp.Id_AssoCleAction == Id
                                select tmp).FirstOrDefault();
            }
            return assoCleActionSet;
        }

        public static List<AssoCleActionSet> findAll()
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet(int IdActionSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdArticleSet(int IdArticleSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdArticleSet(int IdActionSet, int IdArticleSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdAssoArticleSourceSet(int IdAssoArticleSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdAssoArticleSourceSet(int IdActionSet, int IdAssoArticleSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdArticleSet_IdAssoArticleSourceSet(int IdArticleSet, int IdAssoArticleSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet(int IdActionSet, int IdArticleSet, int IdAssoArticleSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdSourceSet(int IdSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdSourceSet(int IdActionSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdArticleSet_IdSourceSet(int IdArticleSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdArticleSet_IdSourceSet(int IdActionSet, int IdArticleSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdAssoArticleSourceSet_IdSourceSet(int IdAssoArticleSourceSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdAssoArticleSourceSet_IdSourceSet(int IdActionSet, int IdAssoArticleSourceSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdArticleSet_IdAssoArticleSourceSet_IdSourceSet(int IdArticleSet, int IdAssoArticleSourceSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet_IdSourceSet(int IdActionSet, int IdArticleSet, int IdAssoArticleSourceSet, int IdSourceSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdLienArticlesSet(int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdLienArticlesSet(int IdActionSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdArticleSet_IdLienArticlesSet(int IdArticleSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdArticleSet_IdLienArticlesSet(int IdActionSet, int IdArticleSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdAssoArticleSourceSet_IdLienArticlesSet(int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdAssoArticleSourceSet_IdLienArticlesSet(int IdActionSet, int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdArticleSet_IdAssoArticleSourceSet_IdLienArticlesSet(int IdArticleSet, int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet_IdLienArticlesSet(int IdActionSet, int IdArticleSet, int IdAssoArticleSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdSourceSet_IdLienArticlesSet(int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdSourceSet_IdLienArticlesSet(int IdActionSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdArticleSet_IdSourceSet_IdLienArticlesSet(int IdArticleSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdArticleSet_IdSourceSet_IdLienArticlesSet(int IdActionSet, int IdArticleSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(int IdAssoArticleSourceSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(int IdActionSet, int IdAssoArticleSourceSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdArticleSet_IdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(int IdArticleSet, int IdAssoArticleSourceSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

        public static List<AssoCleActionSet> findByIdActionSet_IdArticleSet_IdAssoArticleSourceSet_IdSourceSet_IdLienArticlesSet(int IdActionSet, int IdArticleSet, int IdAssoArticleSourceSet, int IdSourceSet, int IdLienArticlesSet)
        {
            List<AssoCleActionSet> list = new List<AssoCleActionSet>();
            lock(AbstractDao.dbLock)
            {
                list = (from tmp in AbstractDao.db.AssoCleActionSet
                    where tmp.Id_Action == IdActionSet && tmp.AssoCleActionArticle_AssoCleAction_Id_Article == IdArticleSet && tmp.AssoCleActionAssoArticleSource_AssoCleAction_Id_AssoArticleSource == IdAssoArticleSourceSet && tmp.AssoCleActionSource_AssoCleAction_Id_Source == IdSourceSet && tmp.LienArticlesAssoCleAction_AssoCleAction_Id_LienArticles == IdLienArticlesSet
                    select tmp).ToList();
            }
            return list;
        }

    }
}