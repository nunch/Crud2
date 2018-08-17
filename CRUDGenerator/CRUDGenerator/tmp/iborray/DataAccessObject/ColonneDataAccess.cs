using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class ColonneDataAccess
    {
        
        public MessageModel AddColonne(ref Colonne colonne)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Colonne.Add(colonne);
                    db.SaveChanges();
                    return new MessageModel(true, "Colonne ajouté avec succès.", colonne);
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

        public MessageModel UpdateColonne(Colonne colonne)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Colonne colonne_fromDB = (from tmp in db.Colonne where tmp.Id_Colonne == colonne.Id_Colonne select tmp).FirstOrDefault();
                    if (colonne_fromDB != null)
                    {
                        db.Entry(colonne_fromDB).CurrentValues.SetValues(colonne);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", colonne_fromDB);
                    }
                    else return new MessageModel(false, "Colonne non éxistant.", null);
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


        public MessageModel RemoveColonne(int Id_Colonne)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Colonne colonne_fromDB = (from tmp in db.Colonne where tmp.Id_Colonne == Id_Colonne select tmp).FirstOrDefault();
                    colonne_fromDB.IsSuppr = true;
                    db.Entry(colonne_fromDB).CurrentValues.SetValues(colonne_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Colonne effacé", null);
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
    
        public MessageModel RemoveColonne(int Id_Colonne, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveColonne(Id_Colonne);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Colonne colonne_fromDB = (from tmp in db.Colonne where tmp.Id_Colonne == Id_Colonne select tmp).FirstOrDefault();
                    db.Colonne.Remove(colonne_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Colonne effacé", null);
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

        public MessageModel GetColonneById(int Id_Colonne)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Colonne colonne = (from tmp in db.Colonne where tmp.Id_Colonne == Id_Colonne select tmp).FirstOrDefault();
                    return new MessageModel(true, "", colonne);
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

        public MessageModel RemoveAllColonne()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Colonne");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Colonne where Id_Colonne = {0}", Id_Colonne);
                    return new MessageModel(true, "Colonnes effacé(e)s", null);
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

        public MessageModel GetAllColonne()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Colonne> list = (from tmp in db.Colonne where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByRapportId(int rapportId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Colonne> list = (from tmp in db.Colonne
                                where tmp.IsSuppr == false && tmp.Id_Rapport == rapportId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des colonne", list);
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