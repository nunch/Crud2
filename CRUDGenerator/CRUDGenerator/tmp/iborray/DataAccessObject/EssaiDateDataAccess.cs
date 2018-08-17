using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class EssaiDateDataAccess
    {
        
        public MessageModel AddEssaiDate(ref EssaiDate essaiDate)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.EssaiDate.Add(essaiDate);
                    db.SaveChanges();
                    return new MessageModel(true, "EssaiDate ajouté avec succès.", essaiDate);
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

        public MessageModel UpdateEssaiDate(EssaiDate essaiDate)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    EssaiDate essaiDate_fromDB = (from tmp in db.EssaiDate where tmp.Id_EssaiDate == essaiDate.Id_EssaiDate select tmp).FirstOrDefault();
                    if (essaiDate_fromDB != null)
                    {
                        db.Entry(essaiDate_fromDB).CurrentValues.SetValues(essaiDate);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", essaiDate_fromDB);
                    }
                    else return new MessageModel(false, "EssaiDate non éxistant.", null);
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


        public MessageModel RemoveEssaiDate(int Id_EssaiDate)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    EssaiDate essaiDate_fromDB = (from tmp in db.EssaiDate where tmp.Id_EssaiDate == Id_EssaiDate select tmp).FirstOrDefault();
                    essaiDate_fromDB.IsSuppr = true;
                    db.Entry(essaiDate_fromDB).CurrentValues.SetValues(essaiDate_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "EssaiDate effacé", null);
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
    
        public MessageModel RemoveEssaiDate(int Id_EssaiDate, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveEssaiDate(Id_EssaiDate);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    EssaiDate essaiDate_fromDB = (from tmp in db.EssaiDate where tmp.Id_EssaiDate == Id_EssaiDate select tmp).FirstOrDefault();
                    db.EssaiDate.Remove(essaiDate_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "EssaiDate effacé", null);
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

        public MessageModel GetEssaiDateById(int Id_EssaiDate)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    EssaiDate essaiDate = (from tmp in db.EssaiDate where tmp.Id_EssaiDate == Id_EssaiDate select tmp).FirstOrDefault();
                    return new MessageModel(true, "", essaiDate);
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

        public MessageModel RemoveAllEssaiDate()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM EssaiDate");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM EssaiDate where Id_EssaiDate = {0}", Id_EssaiDate);
                    return new MessageModel(true, "EssaiDates effacé(e)s", null);
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

        public MessageModel GetAllEssaiDate()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<EssaiDate> list = (from tmp in db.EssaiDate where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByEssaiId(int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<EssaiDate> list = (from tmp in db.EssaiDate
                                where tmp.IsSuppr == false && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essaiDate", list);
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

        public MessageModel GetByDateId(int dateId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<EssaiDate> list = (from tmp in db.EssaiDate
                                where tmp.IsSuppr == false && tmp.Id_Date == dateId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essaiDate", list);
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

        public MessageModel GetByEssaiId_DateId(int essaiId, int dateId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<EssaiDate> list = (from tmp in db.EssaiDate
                                where tmp.IsSuppr == false && tmp.Id_Essai == essaiId && tmp.Id_Date == dateId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essaiDate", list);
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