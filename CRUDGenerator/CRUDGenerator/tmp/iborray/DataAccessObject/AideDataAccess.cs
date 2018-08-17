using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class AideDataAccess
    {
        
        public MessageModel AddAide(ref Aide aide)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Aide.Add(aide);
                    db.SaveChanges();
                    return new MessageModel(true, "Aide ajouté avec succès.", aide);
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

        public MessageModel UpdateAide(Aide aide)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Aide aide_fromDB = (from tmp in db.Aide where tmp.Id_Aide == aide.Id_Aide select tmp).FirstOrDefault();
                    if (aide_fromDB != null)
                    {
                        db.Entry(aide_fromDB).CurrentValues.SetValues(aide);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", aide_fromDB);
                    }
                    else return new MessageModel(false, "Aide non éxistant.", null);
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


        public MessageModel RemoveAide(int Id_Aide)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Aide aide_fromDB = (from tmp in db.Aide where tmp.Id_Aide == Id_Aide select tmp).FirstOrDefault();
                    aide_fromDB.IsSuppr = true;
                    db.Entry(aide_fromDB).CurrentValues.SetValues(aide_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Aide effacé", null);
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
    
        public MessageModel RemoveAide(int Id_Aide, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveAide(Id_Aide);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Aide aide_fromDB = (from tmp in db.Aide where tmp.Id_Aide == Id_Aide select tmp).FirstOrDefault();
                    db.Aide.Remove(aide_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Aide effacé", null);
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

        public MessageModel GetAideById(int Id_Aide)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Aide aide = (from tmp in db.Aide where tmp.Id_Aide == Id_Aide select tmp).FirstOrDefault();
                    return new MessageModel(true, "", aide);
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

        public MessageModel RemoveAllAide()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Aide");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Aide where Id_Aide = {0}", Id_Aide);
                    return new MessageModel(true, "Aides effacé(e)s", null);
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

        public MessageModel GetAllAide()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Aide> list = (from tmp in db.Aide where tmp.IsSuppr == false select tmp).ToList();
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