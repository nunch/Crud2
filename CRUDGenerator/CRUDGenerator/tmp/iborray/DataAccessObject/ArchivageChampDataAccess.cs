using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class ArchivageChampDataAccess
    {
        
        public MessageModel AddArchivageChamp(ref ArchivageChamp archivageChamp)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.ArchivageChamp.Add(archivageChamp);
                    db.SaveChanges();
                    return new MessageModel(true, "ArchivageChamp ajouté avec succès.", archivageChamp);
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

        public MessageModel UpdateArchivageChamp(ArchivageChamp archivageChamp)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ArchivageChamp archivageChamp_fromDB = (from tmp in db.ArchivageChamp where tmp.Id_ArchivageChamp == archivageChamp.Id_ArchivageChamp select tmp).FirstOrDefault();
                    if (archivageChamp_fromDB != null)
                    {
                        db.Entry(archivageChamp_fromDB).CurrentValues.SetValues(archivageChamp);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", archivageChamp_fromDB);
                    }
                    else return new MessageModel(false, "ArchivageChamp non éxistant.", null);
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


        public MessageModel RemoveArchivageChamp(int Id_ArchivageChamp)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ArchivageChamp archivageChamp_fromDB = (from tmp in db.ArchivageChamp where tmp.Id_ArchivageChamp == Id_ArchivageChamp select tmp).FirstOrDefault();
                    archivageChamp_fromDB.IsSuppr = true;
                    db.Entry(archivageChamp_fromDB).CurrentValues.SetValues(archivageChamp_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "ArchivageChamp effacé", null);
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
    
        public MessageModel RemoveArchivageChamp(int Id_ArchivageChamp, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveArchivageChamp(Id_ArchivageChamp);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ArchivageChamp archivageChamp_fromDB = (from tmp in db.ArchivageChamp where tmp.Id_ArchivageChamp == Id_ArchivageChamp select tmp).FirstOrDefault();
                    db.ArchivageChamp.Remove(archivageChamp_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "ArchivageChamp effacé", null);
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

        public MessageModel GetArchivageChampById(int Id_ArchivageChamp)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ArchivageChamp archivageChamp = (from tmp in db.ArchivageChamp where tmp.Id_ArchivageChamp == Id_ArchivageChamp select tmp).FirstOrDefault();
                    return new MessageModel(true, "", archivageChamp);
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

        public MessageModel RemoveAllArchivageChamp()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM ArchivageChamp");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM ArchivageChamp where Id_ArchivageChamp = {0}", Id_ArchivageChamp);
                    return new MessageModel(true, "ArchivageChamps effacé(e)s", null);
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

        public MessageModel GetAllArchivageChamp()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<ArchivageChamp> list = (from tmp in db.ArchivageChamp where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByRoleId(int roleId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<ArchivageChamp> list = (from tmp in db.ArchivageChamp
                                where tmp.IsSuppr == false && tmp.Id_Role == roleId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des archivageChamp", list);
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