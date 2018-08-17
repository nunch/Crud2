using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class BEConcepteurDataAccess
    {
        
        public MessageModel AddBEConcepteur(ref BEConcepteur bEConcepteur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.BEConcepteur.Add(bEConcepteur);
                    db.SaveChanges();
                    return new MessageModel(true, "BEConcepteur ajouté avec succès.", bEConcepteur);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel UpdateBEConcepteur(BEConcepteur bEConcepteur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    BEConcepteur bEConcepteur_fromDB = (from tmp in db.BEConcepteur where tmp.Id_BEConcepteur == bEConcepteur.Id_BEConcepteur select tmp).FirstOrDefault();
                    if (bEConcepteur_fromDB != null)
                    {
                        db.Entry(bEConcepteur_fromDB).CurrentValues.SetValues(bEConcepteur);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", bEConcepteur_fromDB);
                    }
                    else return new MessageModel(false, "BEConcepteur non éxistant.", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }


        public MessageModel RemoveBEConcepteur(int Id_BEConcepteur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    BEConcepteur bEConcepteur_fromDB = (from tmp in db.BEConcepteur where tmp.Id_BEConcepteur == Id_BEConcepteur select tmp).FirstOrDefault();
                    bEConcepteur_fromDB.IsSuppr = true;
                    db.Entry(bEConcepteur_fromDB).CurrentValues.SetValues(bEConcepteur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "BEConcepteur effacé", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
    
        public MessageModel RemoveBEConcepteur(int Id_BEConcepteur, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveBEConcepteur(Id_BEConcepteur);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    BEConcepteur bEConcepteur_fromDB = (from tmp in db.BEConcepteur where tmp.Id_BEConcepteur == Id_BEConcepteur select tmp).FirstOrDefault();
                    db.BEConcepteur.Remove(bEConcepteur_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "BEConcepteur effacé", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel GetBEConcepteurById(int Id_BEConcepteur)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    BEConcepteur bEConcepteur = (from tmp in db.BEConcepteur where tmp.Id_BEConcepteur == Id_BEConcepteur select tmp).FirstOrDefault();
                    return new MessageModel(true, "", bEConcepteur);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel RemoveAllBEConcepteur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM BEConcepteur");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM BEConcepteur where Id_BEConcepteur = {0}", Id_BEConcepteur);
                    return new MessageModel(true, "BEConcepteurs effacé(e)s", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel GetAllBEConcepteur()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur where tmp.IsSuppr == false select tmp).ToList();
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<BEConcepteur> list = (from tmp in db.BEConcepteur
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des bEConcepteur", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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