using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class DroitUtilisateurDataAccess
    {
        
        public MessageModel AddDroitUtilisateur(ref DroitUtilisateur droitUtilisateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.DroitUtilisateur.Add(droitUtilisateur);
                    db.SaveChanges();
                    return new MessageModel(true, "DroitUtilisateur ajouté avec succès.", droitUtilisateur);
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

        public MessageModel UpdateDroitUtilisateur(DroitUtilisateur droitUtilisateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DroitUtilisateur droitUtilisateur_fromDB = (from tmp in db.DroitUtilisateur where tmp.Id_DroitUtilisateur == droitUtilisateur.Id_DroitUtilisateur select tmp).FirstOrDefault();
                    if (droitUtilisateur_fromDB != null)
                    {
                        db.Entry(droitUtilisateur_fromDB).CurrentValues.SetValues(droitUtilisateur);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", droitUtilisateur_fromDB);
                    }
                    else return new MessageModel(false, "DroitUtilisateur non éxistant.", null);
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


        public MessageModel RemoveDroitUtilisateur(int Id_DroitUtilisateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DroitUtilisateur droitUtilisateur_fromDB = (from tmp in db.DroitUtilisateur where tmp.Id_DroitUtilisateur == Id_DroitUtilisateur select tmp).FirstOrDefault();
                    droitUtilisateur_fromDB.IsSuppr = true;
                    db.Entry(droitUtilisateur_fromDB).CurrentValues.SetValues(droitUtilisateur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "DroitUtilisateur effacé", null);
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
    
        public MessageModel RemoveDroitUtilisateur(int Id_DroitUtilisateur, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveDroitUtilisateur(Id_DroitUtilisateur);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DroitUtilisateur droitUtilisateur_fromDB = (from tmp in db.DroitUtilisateur where tmp.Id_DroitUtilisateur == Id_DroitUtilisateur select tmp).FirstOrDefault();
                    db.DroitUtilisateur.Remove(droitUtilisateur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "DroitUtilisateur effacé", null);
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

        public MessageModel GetDroitUtilisateurById(int Id_DroitUtilisateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DroitUtilisateur droitUtilisateur = (from tmp in db.DroitUtilisateur where tmp.Id_DroitUtilisateur == Id_DroitUtilisateur select tmp).FirstOrDefault();
                    return new MessageModel(true, "", droitUtilisateur);
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

        public MessageModel RemoveAllDroitUtilisateur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM DroitUtilisateur");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM DroitUtilisateur where Id_DroitUtilisateur = {0}", Id_DroitUtilisateur);
                    return new MessageModel(true, "DroitUtilisateurs effacé(e)s", null);
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

        public MessageModel GetAllDroitUtilisateur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<DroitUtilisateur> list = (from tmp in db.DroitUtilisateur where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByUtilisateurId(int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<DroitUtilisateur> list = (from tmp in db.DroitUtilisateur
                                where tmp.IsSuppr == false && tmp.Id_Utilisateur == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des droitUtilisateur", list);
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
                    List<DroitUtilisateur> list = (from tmp in db.DroitUtilisateur
                                where tmp.IsSuppr == false && tmp.Id_Role == roleId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des droitUtilisateur", list);
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

        public MessageModel GetByUtilisateurId_RoleId(int utilisateurId, int roleId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<DroitUtilisateur> list = (from tmp in db.DroitUtilisateur
                                where tmp.IsSuppr == false && tmp.Id_Utilisateur == utilisateurId && tmp.Id_Role == roleId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des droitUtilisateur", list);
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