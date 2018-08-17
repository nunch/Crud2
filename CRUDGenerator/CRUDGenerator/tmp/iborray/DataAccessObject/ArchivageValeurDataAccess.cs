using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class ArchivageValeurDataAccess
    {
        
        public MessageModel AddArchivageValeur(ref ArchivageValeur archivageValeur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.ArchivageValeur.Add(archivageValeur);
                    db.SaveChanges();
                    return new MessageModel(true, "ArchivageValeur ajouté avec succès.", archivageValeur);
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

        public MessageModel UpdateArchivageValeur(ArchivageValeur archivageValeur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ArchivageValeur archivageValeur_fromDB = (from tmp in db.ArchivageValeur where tmp.Id_ArchivageValeur == archivageValeur.Id_ArchivageValeur select tmp).FirstOrDefault();
                    if (archivageValeur_fromDB != null)
                    {
                        db.Entry(archivageValeur_fromDB).CurrentValues.SetValues(archivageValeur);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", archivageValeur_fromDB);
                    }
                    else return new MessageModel(false, "ArchivageValeur non éxistant.", null);
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


        public MessageModel RemoveArchivageValeur(int Id_ArchivageValeur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ArchivageValeur archivageValeur_fromDB = (from tmp in db.ArchivageValeur where tmp.Id_ArchivageValeur == Id_ArchivageValeur select tmp).FirstOrDefault();
                    archivageValeur_fromDB.IsSuppr = true;
                    db.Entry(archivageValeur_fromDB).CurrentValues.SetValues(archivageValeur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "ArchivageValeur effacé", null);
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
    
        public MessageModel RemoveArchivageValeur(int Id_ArchivageValeur, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveArchivageValeur(Id_ArchivageValeur);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ArchivageValeur archivageValeur_fromDB = (from tmp in db.ArchivageValeur where tmp.Id_ArchivageValeur == Id_ArchivageValeur select tmp).FirstOrDefault();
                    db.ArchivageValeur.Remove(archivageValeur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "ArchivageValeur effacé", null);
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

        public MessageModel GetArchivageValeurById(int Id_ArchivageValeur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ArchivageValeur archivageValeur = (from tmp in db.ArchivageValeur where tmp.Id_ArchivageValeur == Id_ArchivageValeur select tmp).FirstOrDefault();
                    return new MessageModel(true, "", archivageValeur);
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

        public MessageModel RemoveAllArchivageValeur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM ArchivageValeur");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM ArchivageValeur where Id_ArchivageValeur = {0}", Id_ArchivageValeur);
                    return new MessageModel(true, "ArchivageValeurs effacé(e)s", null);
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

        public MessageModel GetAllArchivageValeur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<ArchivageValeur> list = (from tmp in db.ArchivageValeur where tmp.IsSuppr == false select tmp).ToList();
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