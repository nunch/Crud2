using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class DateDataAccess
    {
        
        public MessageModel AddDate(ref Date date)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Date.Add(date);
                    db.SaveChanges();
                    return new MessageModel(true, "Date ajouté avec succès.", date);
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

        public MessageModel UpdateDate(Date date)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Date date_fromDB = (from tmp in db.Date where tmp.Id_Date == date.Id_Date select tmp).FirstOrDefault();
                    if (date_fromDB != null)
                    {
                        db.Entry(date_fromDB).CurrentValues.SetValues(date);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", date_fromDB);
                    }
                    else return new MessageModel(false, "Date non éxistant.", null);
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


        public MessageModel RemoveDate(int Id_Date)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Date date_fromDB = (from tmp in db.Date where tmp.Id_Date == Id_Date select tmp).FirstOrDefault();
                    date_fromDB.IsSuppr = true;
                    db.Entry(date_fromDB).CurrentValues.SetValues(date_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Date effacé", null);
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
    
        public MessageModel RemoveDate(int Id_Date, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveDate(Id_Date);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Date date_fromDB = (from tmp in db.Date where tmp.Id_Date == Id_Date select tmp).FirstOrDefault();
                    db.Date.Remove(date_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Date effacé", null);
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

        public MessageModel GetDateById(int Id_Date)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Date date = (from tmp in db.Date where tmp.Id_Date == Id_Date select tmp).FirstOrDefault();
                    return new MessageModel(true, "", date);
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

        public MessageModel RemoveAllDate()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Date");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Date where Id_Date = {0}", Id_Date);
                    return new MessageModel(true, "Dates effacé(e)s", null);
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

        public MessageModel GetAllDate()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Date> list = (from tmp in db.Date where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByTypeId(int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Date> list = (from tmp in db.Date
                                where tmp.IsSuppr == false && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des date", list);
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