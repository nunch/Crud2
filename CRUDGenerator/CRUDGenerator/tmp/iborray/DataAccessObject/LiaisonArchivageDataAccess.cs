using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class LiaisonArchivageDataAccess
    {
        
        public MessageModel AddLiaisonArchivage(ref LiaisonArchivage liaisonArchivage)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.LiaisonArchivage.Add(liaisonArchivage);
                    db.SaveChanges();
                    return new MessageModel(true, "LiaisonArchivage ajouté avec succès.", liaisonArchivage);
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

        public MessageModel UpdateLiaisonArchivage(LiaisonArchivage liaisonArchivage)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    LiaisonArchivage liaisonArchivage_fromDB = (from tmp in db.LiaisonArchivage where tmp.Id_LiaisonArchivage == liaisonArchivage.Id_LiaisonArchivage select tmp).FirstOrDefault();
                    if (liaisonArchivage_fromDB != null)
                    {
                        db.Entry(liaisonArchivage_fromDB).CurrentValues.SetValues(liaisonArchivage);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", liaisonArchivage_fromDB);
                    }
                    else return new MessageModel(false, "LiaisonArchivage non éxistant.", null);
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


        public MessageModel RemoveLiaisonArchivage(int Id_LiaisonArchivage)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    LiaisonArchivage liaisonArchivage_fromDB = (from tmp in db.LiaisonArchivage where tmp.Id_LiaisonArchivage == Id_LiaisonArchivage select tmp).FirstOrDefault();
                    liaisonArchivage_fromDB.IsSuppr = true;
                    db.Entry(liaisonArchivage_fromDB).CurrentValues.SetValues(liaisonArchivage_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "LiaisonArchivage effacé", null);
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
    
        public MessageModel RemoveLiaisonArchivage(int Id_LiaisonArchivage, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveLiaisonArchivage(Id_LiaisonArchivage);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    LiaisonArchivage liaisonArchivage_fromDB = (from tmp in db.LiaisonArchivage where tmp.Id_LiaisonArchivage == Id_LiaisonArchivage select tmp).FirstOrDefault();
                    db.LiaisonArchivage.Remove(liaisonArchivage_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "LiaisonArchivage effacé", null);
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

        public MessageModel GetLiaisonArchivageById(int Id_LiaisonArchivage)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    LiaisonArchivage liaisonArchivage = (from tmp in db.LiaisonArchivage where tmp.Id_LiaisonArchivage == Id_LiaisonArchivage select tmp).FirstOrDefault();
                    return new MessageModel(true, "", liaisonArchivage);
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

        public MessageModel RemoveAllLiaisonArchivage()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM LiaisonArchivage");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM LiaisonArchivage where Id_LiaisonArchivage = {0}", Id_LiaisonArchivage);
                    return new MessageModel(true, "LiaisonArchivages effacé(e)s", null);
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

        public MessageModel GetAllLiaisonArchivage()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<LiaisonArchivage> list = (from tmp in db.LiaisonArchivage where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByArchivageChampId(int archivageChampId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<LiaisonArchivage> list = (from tmp in db.LiaisonArchivage
                                where tmp.IsSuppr == false && tmp.Id_ArchivageChamp == archivageChampId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des liaisonArchivage", list);
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

        public MessageModel GetByArchivageValeurId(int archivageValeurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<LiaisonArchivage> list = (from tmp in db.LiaisonArchivage
                                where tmp.IsSuppr == false && tmp.Id_ArchivageValeur == archivageValeurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des liaisonArchivage", list);
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

        public MessageModel GetByArchivageChampId_ArchivageValeurId(int archivageChampId, int archivageValeurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<LiaisonArchivage> list = (from tmp in db.LiaisonArchivage
                                where tmp.IsSuppr == false && tmp.Id_ArchivageChamp == archivageChampId && tmp.Id_ArchivageValeur == archivageValeurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des liaisonArchivage", list);
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