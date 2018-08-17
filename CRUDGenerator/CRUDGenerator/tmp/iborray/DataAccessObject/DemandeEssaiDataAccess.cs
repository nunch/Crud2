using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class DemandeEssaiDataAccess
    {
        
        public MessageModel AddDemandeEssai(ref DemandeEssai demandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.DemandeEssai.Add(demandeEssai);
                    db.SaveChanges();
                    return new MessageModel(true, "DemandeEssai ajouté avec succès.", demandeEssai);
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

        public MessageModel UpdateDemandeEssai(DemandeEssai demandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DemandeEssai demandeEssai_fromDB = (from tmp in db.DemandeEssai where tmp.Id_DemandeEssai == demandeEssai.Id_DemandeEssai select tmp).FirstOrDefault();
                    if (demandeEssai_fromDB != null)
                    {
                        db.Entry(demandeEssai_fromDB).CurrentValues.SetValues(demandeEssai);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", demandeEssai_fromDB);
                    }
                    else return new MessageModel(false, "DemandeEssai non éxistant.", null);
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


        public MessageModel RemoveDemandeEssai(int Id_DemandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DemandeEssai demandeEssai_fromDB = (from tmp in db.DemandeEssai where tmp.Id_DemandeEssai == Id_DemandeEssai select tmp).FirstOrDefault();
                    demandeEssai_fromDB.IsSuppr = true;
                    db.Entry(demandeEssai_fromDB).CurrentValues.SetValues(demandeEssai_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "DemandeEssai effacé", null);
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
    
        public MessageModel RemoveDemandeEssai(int Id_DemandeEssai, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveDemandeEssai(Id_DemandeEssai);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DemandeEssai demandeEssai_fromDB = (from tmp in db.DemandeEssai where tmp.Id_DemandeEssai == Id_DemandeEssai select tmp).FirstOrDefault();
                    db.DemandeEssai.Remove(demandeEssai_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "DemandeEssai effacé", null);
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

        public MessageModel GetDemandeEssaiById(int Id_DemandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DemandeEssai demandeEssai = (from tmp in db.DemandeEssai where tmp.Id_DemandeEssai == Id_DemandeEssai select tmp).FirstOrDefault();
                    return new MessageModel(true, "", demandeEssai);
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

        public MessageModel RemoveAllDemandeEssai()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM DemandeEssai");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM DemandeEssai where Id_DemandeEssai = {0}", Id_DemandeEssai);
                    return new MessageModel(true, "DemandeEssais effacé(e)s", null);
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

        public MessageModel GetAllDemandeEssai()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<DemandeEssai> list = (from tmp in db.DemandeEssai where tmp.IsSuppr == false select tmp).ToList();
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