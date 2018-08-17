using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class AssoEssaiDemandeEssaiDataAccess
    {
        
        public MessageModel AddAssoEssaiDemandeEssai(ref AssoEssaiDemandeEssai assoEssaiDemandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.AssoEssaiDemandeEssai.Add(assoEssaiDemandeEssai);
                    db.SaveChanges();
                    return new MessageModel(true, "AssoEssaiDemandeEssai ajouté avec succès.", assoEssaiDemandeEssai);
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

        public MessageModel UpdateAssoEssaiDemandeEssai(AssoEssaiDemandeEssai assoEssaiDemandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    AssoEssaiDemandeEssai assoEssaiDemandeEssai_fromDB = (from tmp in db.AssoEssaiDemandeEssai where tmp.Id_AssoEssaiDemandeEssai == assoEssaiDemandeEssai.Id_AssoEssaiDemandeEssai select tmp).FirstOrDefault();
                    if (assoEssaiDemandeEssai_fromDB != null)
                    {
                        db.Entry(assoEssaiDemandeEssai_fromDB).CurrentValues.SetValues(assoEssaiDemandeEssai);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", assoEssaiDemandeEssai_fromDB);
                    }
                    else return new MessageModel(false, "AssoEssaiDemandeEssai non éxistant.", null);
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


        public MessageModel RemoveAssoEssaiDemandeEssai(int Id_AssoEssaiDemandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    AssoEssaiDemandeEssai assoEssaiDemandeEssai_fromDB = (from tmp in db.AssoEssaiDemandeEssai where tmp.Id_AssoEssaiDemandeEssai == Id_AssoEssaiDemandeEssai select tmp).FirstOrDefault();
                    assoEssaiDemandeEssai_fromDB.IsSuppr = true;
                    db.Entry(assoEssaiDemandeEssai_fromDB).CurrentValues.SetValues(assoEssaiDemandeEssai_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "AssoEssaiDemandeEssai effacé", null);
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
    
        public MessageModel RemoveAssoEssaiDemandeEssai(int Id_AssoEssaiDemandeEssai, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveAssoEssaiDemandeEssai(Id_AssoEssaiDemandeEssai);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    AssoEssaiDemandeEssai assoEssaiDemandeEssai_fromDB = (from tmp in db.AssoEssaiDemandeEssai where tmp.Id_AssoEssaiDemandeEssai == Id_AssoEssaiDemandeEssai select tmp).FirstOrDefault();
                    db.AssoEssaiDemandeEssai.Remove(assoEssaiDemandeEssai_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "AssoEssaiDemandeEssai effacé", null);
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

        public MessageModel GetAssoEssaiDemandeEssaiById(int Id_AssoEssaiDemandeEssai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    AssoEssaiDemandeEssai assoEssaiDemandeEssai = (from tmp in db.AssoEssaiDemandeEssai where tmp.Id_AssoEssaiDemandeEssai == Id_AssoEssaiDemandeEssai select tmp).FirstOrDefault();
                    return new MessageModel(true, "", assoEssaiDemandeEssai);
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

        public MessageModel RemoveAllAssoEssaiDemandeEssai()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM AssoEssaiDemandeEssai");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM AssoEssaiDemandeEssai where Id_AssoEssaiDemandeEssai = {0}", Id_AssoEssaiDemandeEssai);
                    return new MessageModel(true, "AssoEssaiDemandeEssais effacé(e)s", null);
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

        public MessageModel GetAllAssoEssaiDemandeEssai()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<AssoEssaiDemandeEssai> list = (from tmp in db.AssoEssaiDemandeEssai where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByDemandeEssaiId(int demandeEssaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<AssoEssaiDemandeEssai> list = (from tmp in db.AssoEssaiDemandeEssai
                                where tmp.IsSuppr == false && tmp.Id_DemandeEssai == demandeEssaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des assoEssaiDemandeEssai", list);
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
                    List<AssoEssaiDemandeEssai> list = (from tmp in db.AssoEssaiDemandeEssai
                                where tmp.IsSuppr == false && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des assoEssaiDemandeEssai", list);
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

        public MessageModel GetByDemandeEssaiId_EssaiId(int demandeEssaiId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<AssoEssaiDemandeEssai> list = (from tmp in db.AssoEssaiDemandeEssai
                                where tmp.IsSuppr == false && tmp.Id_DemandeEssai == demandeEssaiId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des assoEssaiDemandeEssai", list);
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