using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class AssoDescriptifDemandeEssaiDataAccess
    {
        
        public MessageModel AddAssoDescriptifDemandeEssai(ref AssoDescriptifDemandeEssai assoDescriptifDemandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.AssoDescriptifDemandeEssai.Add(assoDescriptifDemandeEssai);
                    db.SaveChanges();
                    return new MessageModel(true, "AssoDescriptifDemandeEssai ajouté avec succès.", assoDescriptifDemandeEssai);
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

        public MessageModel UpdateAssoDescriptifDemandeEssai(AssoDescriptifDemandeEssai assoDescriptifDemandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    AssoDescriptifDemandeEssai assoDescriptifDemandeEssai_fromDB = (from tmp in db.AssoDescriptifDemandeEssai where tmp.Id_AssoDescriptifDemandeEssai == assoDescriptifDemandeEssai.Id_AssoDescriptifDemandeEssai select tmp).FirstOrDefault();
                    if (assoDescriptifDemandeEssai_fromDB != null)
                    {
                        db.Entry(assoDescriptifDemandeEssai_fromDB).CurrentValues.SetValues(assoDescriptifDemandeEssai);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", assoDescriptifDemandeEssai_fromDB);
                    }
                    else return new MessageModel(false, "AssoDescriptifDemandeEssai non éxistant.", null);
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


        public MessageModel RemoveAssoDescriptifDemandeEssai(int Id_AssoDescriptifDemandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    AssoDescriptifDemandeEssai assoDescriptifDemandeEssai_fromDB = (from tmp in db.AssoDescriptifDemandeEssai where tmp.Id_AssoDescriptifDemandeEssai == Id_AssoDescriptifDemandeEssai select tmp).FirstOrDefault();
                    assoDescriptifDemandeEssai_fromDB.IsSuppr = true;
                    db.Entry(assoDescriptifDemandeEssai_fromDB).CurrentValues.SetValues(assoDescriptifDemandeEssai_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "AssoDescriptifDemandeEssai effacé", null);
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
    
        public MessageModel RemoveAssoDescriptifDemandeEssai(int Id_AssoDescriptifDemandeEssai, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveAssoDescriptifDemandeEssai(Id_AssoDescriptifDemandeEssai);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    AssoDescriptifDemandeEssai assoDescriptifDemandeEssai_fromDB = (from tmp in db.AssoDescriptifDemandeEssai where tmp.Id_AssoDescriptifDemandeEssai == Id_AssoDescriptifDemandeEssai select tmp).FirstOrDefault();
                    db.AssoDescriptifDemandeEssai.Remove(assoDescriptifDemandeEssai_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "AssoDescriptifDemandeEssai effacé", null);
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

        public MessageModel GetAssoDescriptifDemandeEssaiById(int Id_AssoDescriptifDemandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    AssoDescriptifDemandeEssai assoDescriptifDemandeEssai = (from tmp in db.AssoDescriptifDemandeEssai where tmp.Id_AssoDescriptifDemandeEssai == Id_AssoDescriptifDemandeEssai select tmp).FirstOrDefault();
                    return new MessageModel(true, "", assoDescriptifDemandeEssai);
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

        public MessageModel RemoveAllAssoDescriptifDemandeEssai()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM AssoDescriptifDemandeEssai");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM AssoDescriptifDemandeEssai where Id_AssoDescriptifDemandeEssai = {0}", Id_AssoDescriptifDemandeEssai);
                    return new MessageModel(true, "AssoDescriptifDemandeEssais effacé(e)s", null);
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

        public MessageModel GetAllAssoDescriptifDemandeEssai()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<AssoDescriptifDemandeEssai> list = (from tmp in db.AssoDescriptifDemandeEssai where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByDescriptifId(int descriptifId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<AssoDescriptifDemandeEssai> list = (from tmp in db.AssoDescriptifDemandeEssai
                                where tmp.IsSuppr == false && tmp.Id_Descriptif == descriptifId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des assoDescriptifDemandeEssai", list);
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

        public MessageModel GetByDemandeEssaiId(int demandeEssaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<AssoDescriptifDemandeEssai> list = (from tmp in db.AssoDescriptifDemandeEssai
                                where tmp.IsSuppr == false && tmp.Id_DemandeEssai == demandeEssaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des assoDescriptifDemandeEssai", list);
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

        public MessageModel GetByDescriptifId_DemandeEssaiId(int descriptifId, int demandeEssaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<AssoDescriptifDemandeEssai> list = (from tmp in db.AssoDescriptifDemandeEssai
                                where tmp.IsSuppr == false && tmp.Id_Descriptif == descriptifId && tmp.Id_DemandeEssai == demandeEssaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des assoDescriptifDemandeEssai", list);
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