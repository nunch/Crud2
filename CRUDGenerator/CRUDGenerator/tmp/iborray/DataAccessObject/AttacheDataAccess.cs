using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class AttacheDataAccess
    {
        
        public MessageModel AddAttache(ref Attache attache)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Attache.Add(attache);
                    db.SaveChanges();
                    return new MessageModel(true, "Attache ajouté avec succès.", attache);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel UpdateAttache(Attache attache)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Attache attache_fromDB = (from tmp in db.Attache where tmp.Id_Attache == attache.Id_Attache select tmp).FirstOrDefault();
                    if (attache_fromDB != null)
                    {
                        db.Entry(attache_fromDB).CurrentValues.SetValues(attache);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", attache_fromDB);
                    }
                    else return new MessageModel(false, "Attache non éxistant.", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }


        public MessageModel RemoveAttache(int Id_Attache)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Attache attache_fromDB = (from tmp in db.Attache where tmp.Id_Attache == Id_Attache select tmp).FirstOrDefault();
                    attache_fromDB.IsSuppr = true;
                    db.Entry(attache_fromDB).CurrentValues.SetValues(attache_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Attache effacé", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
    
        public MessageModel RemoveAttache(int Id_Attache, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveAttache(Id_Attache);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Attache attache_fromDB = (from tmp in db.Attache where tmp.Id_Attache == Id_Attache select tmp).FirstOrDefault();
                    db.Attache.Remove(attache_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Attache effacé", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel GetAttacheById(int Id_Attache)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Attache attache = (from tmp in db.Attache where tmp.Id_Attache == Id_Attache select tmp).FirstOrDefault();
                    return new MessageModel(true, "", attache);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel RemoveAllAttache()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Attache");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Attache where Id_Attache = {0}", Id_Attache);
                    return new MessageModel(true, "Attaches effacé(e)s", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel GetAllAttache()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Attache> list = (from tmp in db.Attache where tmp.IsSuppr == false select tmp).ToList();
                    return new MessageModel(true, "", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

    }
}