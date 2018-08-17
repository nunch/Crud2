using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class EssayeurDataAccess
    {
        
        public MessageModel AddEssayeur(ref Essayeur essayeur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Essayeur.Add(essayeur);
                    db.SaveChanges();
                    return new MessageModel(true, "Essayeur ajouté avec succès.", essayeur);
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

        public MessageModel UpdateEssayeur(Essayeur essayeur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Essayeur essayeur_fromDB = (from tmp in db.Essayeur where tmp.Id_Essayeur == essayeur.Id_Essayeur select tmp).FirstOrDefault();
                    if (essayeur_fromDB != null)
                    {
                        db.Entry(essayeur_fromDB).CurrentValues.SetValues(essayeur);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", essayeur_fromDB);
                    }
                    else return new MessageModel(false, "Essayeur non éxistant.", null);
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


        public MessageModel RemoveEssayeur(int Id_Essayeur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Essayeur essayeur_fromDB = (from tmp in db.Essayeur where tmp.Id_Essayeur == Id_Essayeur select tmp).FirstOrDefault();
                    essayeur_fromDB.IsSuppr = true;
                    db.Entry(essayeur_fromDB).CurrentValues.SetValues(essayeur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Essayeur effacé", null);
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
    
        public MessageModel RemoveEssayeur(int Id_Essayeur, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveEssayeur(Id_Essayeur);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Essayeur essayeur_fromDB = (from tmp in db.Essayeur where tmp.Id_Essayeur == Id_Essayeur select tmp).FirstOrDefault();
                    db.Essayeur.Remove(essayeur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Essayeur effacé", null);
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

        public MessageModel GetEssayeurById(int Id_Essayeur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Essayeur essayeur = (from tmp in db.Essayeur where tmp.Id_Essayeur == Id_Essayeur select tmp).FirstOrDefault();
                    return new MessageModel(true, "", essayeur);
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

        public MessageModel RemoveAllEssayeur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Essayeur");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Essayeur where Id_Essayeur = {0}", Id_Essayeur);
                    return new MessageModel(true, "Essayeurs effacé(e)s", null);
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

        public MessageModel GetAllEssayeur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur where tmp.IsSuppr == false select tmp).ToList();
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
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_EssaiId(int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_EssaiId(int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_EssaiId(int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_EssaiId(int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_EssaiId(int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_EssaiId(int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_EssaiId(int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_EssaiId(int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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

        public MessageModel GetByUtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_UtilisateurId_EssaiId(int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int utilisateurId, int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essayeur> list = (from tmp in db.Essayeur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essayeur", list);
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