using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class BESpecificateurDataAccess
    {
        
        public MessageModel AddBESpecificateur(ref BESpecificateur bESpecificateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.BESpecificateur.Add(bESpecificateur);
                    db.SaveChanges();
                    return new MessageModel(true, "BESpecificateur ajouté avec succès.", bESpecificateur);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel UpdateBESpecificateur(BESpecificateur bESpecificateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    BESpecificateur bESpecificateur_fromDB = (from tmp in db.BESpecificateur where tmp.Id_BESpecificateur == bESpecificateur.Id_BESpecificateur select tmp).FirstOrDefault();
                    if (bESpecificateur_fromDB != null)
                    {
                        db.Entry(bESpecificateur_fromDB).CurrentValues.SetValues(bESpecificateur);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", bESpecificateur_fromDB);
                    }
                    else return new MessageModel(false, "BESpecificateur non éxistant.", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }


        public MessageModel RemoveBESpecificateur(int Id_BESpecificateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    BESpecificateur bESpecificateur_fromDB = (from tmp in db.BESpecificateur where tmp.Id_BESpecificateur == Id_BESpecificateur select tmp).FirstOrDefault();
                    bESpecificateur_fromDB.IsSuppr = true;
                    db.Entry(bESpecificateur_fromDB).CurrentValues.SetValues(bESpecificateur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "BESpecificateur effacé", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
    
        public MessageModel RemoveBESpecificateur(int Id_BESpecificateur, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveBESpecificateur(Id_BESpecificateur);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    BESpecificateur bESpecificateur_fromDB = (from tmp in db.BESpecificateur where tmp.Id_BESpecificateur == Id_BESpecificateur select tmp).FirstOrDefault();
                    db.BESpecificateur.Remove(bESpecificateur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "BESpecificateur effacé", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel GetBESpecificateurById(int Id_BESpecificateur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    BESpecificateur bESpecificateur = (from tmp in db.BESpecificateur where tmp.Id_BESpecificateur == Id_BESpecificateur select tmp).FirstOrDefault();
                    return new MessageModel(true, "", bESpecificateur);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel RemoveAllBESpecificateur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM BESpecificateur");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM BESpecificateur where Id_BESpecificateur = {0}", Id_BESpecificateur);
                    return new MessageModel(true, "BESpecificateurs effacé(e)s", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel GetAllBESpecificateur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur where tmp.IsSuppr == false select tmp).ToList();
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BESpecificateur> list = (from tmp in db.BESpecificateur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bESpecificateur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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