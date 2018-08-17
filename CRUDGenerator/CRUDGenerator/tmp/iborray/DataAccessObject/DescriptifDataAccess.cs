using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class DescriptifDataAccess
    {
        
        public MessageModel AddDescriptif(ref Descriptif descriptif)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Descriptif.Add(descriptif);
                    db.SaveChanges();
                    return new MessageModel(true, "Descriptif ajouté avec succès.", descriptif);
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

        public MessageModel UpdateDescriptif(Descriptif descriptif)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Descriptif descriptif_fromDB = (from tmp in db.Descriptif where tmp.Id_Descriptif == descriptif.Id_Descriptif select tmp).FirstOrDefault();
                    if (descriptif_fromDB != null)
                    {
                        db.Entry(descriptif_fromDB).CurrentValues.SetValues(descriptif);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", descriptif_fromDB);
                    }
                    else return new MessageModel(false, "Descriptif non éxistant.", null);
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


        public MessageModel RemoveDescriptif(int Id_Descriptif)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Descriptif descriptif_fromDB = (from tmp in db.Descriptif where tmp.Id_Descriptif == Id_Descriptif select tmp).FirstOrDefault();
                    descriptif_fromDB.IsSuppr = true;
                    db.Entry(descriptif_fromDB).CurrentValues.SetValues(descriptif_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Descriptif effacé", null);
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
    
        public MessageModel RemoveDescriptif(int Id_Descriptif, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveDescriptif(Id_Descriptif);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Descriptif descriptif_fromDB = (from tmp in db.Descriptif where tmp.Id_Descriptif == Id_Descriptif select tmp).FirstOrDefault();
                    db.Descriptif.Remove(descriptif_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Descriptif effacé", null);
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

        public MessageModel GetDescriptifById(int Id_Descriptif)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Descriptif descriptif = (from tmp in db.Descriptif where tmp.Id_Descriptif == Id_Descriptif select tmp).FirstOrDefault();
                    return new MessageModel(true, "", descriptif);
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

        public MessageModel RemoveAllDescriptif()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Descriptif");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Descriptif where Id_Descriptif = {0}", Id_Descriptif);
                    return new MessageModel(true, "Descriptifs effacé(e)s", null);
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

        public MessageModel GetAllDescriptif()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Descriptif> list = (from tmp in db.Descriptif where tmp.IsSuppr == false select tmp).ToList();
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