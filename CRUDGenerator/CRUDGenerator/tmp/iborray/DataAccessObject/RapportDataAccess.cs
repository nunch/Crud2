using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class RapportDataAccess
    {
        
        public MessageModel AddRapport(ref Rapport rapport)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Rapport.Add(rapport);
                    db.SaveChanges();
                    return new MessageModel(true, "Rapport ajouté avec succès.", rapport);
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

        public MessageModel UpdateRapport(Rapport rapport)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Rapport rapport_fromDB = (from tmp in db.Rapport where tmp.Id_Rapport == rapport.Id_Rapport select tmp).FirstOrDefault();
                    if (rapport_fromDB != null)
                    {
                        db.Entry(rapport_fromDB).CurrentValues.SetValues(rapport);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", rapport_fromDB);
                    }
                    else return new MessageModel(false, "Rapport non éxistant.", null);
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


        public MessageModel RemoveRapport(int Id_Rapport)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Rapport rapport_fromDB = (from tmp in db.Rapport where tmp.Id_Rapport == Id_Rapport select tmp).FirstOrDefault();
                    rapport_fromDB.IsSuppr = true;
                    db.Entry(rapport_fromDB).CurrentValues.SetValues(rapport_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Rapport effacé", null);
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
    
        public MessageModel RemoveRapport(int Id_Rapport, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveRapport(Id_Rapport);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Rapport rapport_fromDB = (from tmp in db.Rapport where tmp.Id_Rapport == Id_Rapport select tmp).FirstOrDefault();
                    db.Rapport.Remove(rapport_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Rapport effacé", null);
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

        public MessageModel GetRapportById(int Id_Rapport)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Rapport rapport = (from tmp in db.Rapport where tmp.Id_Rapport == Id_Rapport select tmp).FirstOrDefault();
                    return new MessageModel(true, "", rapport);
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

        public MessageModel RemoveAllRapport()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Rapport");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Rapport where Id_Rapport = {0}", Id_Rapport);
                    return new MessageModel(true, "Rapports effacé(e)s", null);
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

        public MessageModel GetAllRapport()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Rapport> list = (from tmp in db.Rapport where tmp.IsSuppr == false select tmp).ToList();
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
                    List<Rapport> list = (from tmp in db.Rapport
                                where tmp.IsSuppr == false && tmp.Id_Utilisateur == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des rapport", list);
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