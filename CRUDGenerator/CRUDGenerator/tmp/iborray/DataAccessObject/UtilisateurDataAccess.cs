using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class UtilisateurDataAccess
    {
        
        public MessageModel AddUtilisateur(ref Utilisateur utilisateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Utilisateur.Add(utilisateur);
                    db.SaveChanges();
                    return new MessageModel(true, "Utilisateur ajouté avec succès.", utilisateur);
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

        public MessageModel UpdateUtilisateur(Utilisateur utilisateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Utilisateur utilisateur_fromDB = (from tmp in db.Utilisateur where tmp.Id_Utilisateur == utilisateur.Id_Utilisateur select tmp).FirstOrDefault();
                    if (utilisateur_fromDB != null)
                    {
                        db.Entry(utilisateur_fromDB).CurrentValues.SetValues(utilisateur);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", utilisateur_fromDB);
                    }
                    else return new MessageModel(false, "Utilisateur non éxistant.", null);
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


        public MessageModel RemoveUtilisateur(int Id_Utilisateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Utilisateur utilisateur_fromDB = (from tmp in db.Utilisateur where tmp.Id_Utilisateur == Id_Utilisateur select tmp).FirstOrDefault();
                    utilisateur_fromDB.IsSuppr = true;
                    db.Entry(utilisateur_fromDB).CurrentValues.SetValues(utilisateur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Utilisateur effacé", null);
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
    
        public MessageModel RemoveUtilisateur(int Id_Utilisateur, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveUtilisateur(Id_Utilisateur);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Utilisateur utilisateur_fromDB = (from tmp in db.Utilisateur where tmp.Id_Utilisateur == Id_Utilisateur select tmp).FirstOrDefault();
                    db.Utilisateur.Remove(utilisateur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Utilisateur effacé", null);
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

        public MessageModel GetUtilisateurById(int Id_Utilisateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Utilisateur utilisateur = (from tmp in db.Utilisateur where tmp.Id_Utilisateur == Id_Utilisateur select tmp).FirstOrDefault();
                    return new MessageModel(true, "", utilisateur);
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

        public MessageModel RemoveAllUtilisateur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Utilisateur");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Utilisateur where Id_Utilisateur = {0}", Id_Utilisateur);
                    return new MessageModel(true, "Utilisateurs effacé(e)s", null);
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

        public MessageModel GetAllUtilisateur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Utilisateur> list = (from tmp in db.Utilisateur where tmp.IsSuppr == false select tmp).ToList();
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