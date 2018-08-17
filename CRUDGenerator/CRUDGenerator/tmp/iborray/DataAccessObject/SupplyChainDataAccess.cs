using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class SupplyChainDataAccess
    {
        
        public MessageModel AddSupplyChain(ref SupplyChain supplyChain)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.SupplyChain.Add(supplyChain);
                    db.SaveChanges();
                    return new MessageModel(true, "SupplyChain ajouté avec succès.", supplyChain);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel UpdateSupplyChain(SupplyChain supplyChain)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    SupplyChain supplyChain_fromDB = (from tmp in db.SupplyChain where tmp.Id_SupplyChain == supplyChain.Id_SupplyChain select tmp).FirstOrDefault();
                    if (supplyChain_fromDB != null)
                    {
                        db.Entry(supplyChain_fromDB).CurrentValues.SetValues(supplyChain);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", supplyChain_fromDB);
                    }
                    else return new MessageModel(false, "SupplyChain non éxistant.", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }


        public MessageModel RemoveSupplyChain(int Id_SupplyChain)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    SupplyChain supplyChain_fromDB = (from tmp in db.SupplyChain where tmp.Id_SupplyChain == Id_SupplyChain select tmp).FirstOrDefault();
                    supplyChain_fromDB.IsSuppr = true;
                    db.Entry(supplyChain_fromDB).CurrentValues.SetValues(supplyChain_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "SupplyChain effacé", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
    
        public MessageModel RemoveSupplyChain(int Id_SupplyChain, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveSupplyChain(Id_SupplyChain);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    SupplyChain supplyChain_fromDB = (from tmp in db.SupplyChain where tmp.Id_SupplyChain == Id_SupplyChain select tmp).FirstOrDefault();
                    db.SupplyChain.Remove(supplyChain_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "SupplyChain effacé", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel GetSupplyChainById(int Id_SupplyChain)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    SupplyChain supplyChain = (from tmp in db.SupplyChain where tmp.Id_SupplyChain == Id_SupplyChain select tmp).FirstOrDefault();
                    return new MessageModel(true, "", supplyChain);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel RemoveAllSupplyChain()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM SupplyChain");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM SupplyChain where Id_SupplyChain = {0}", Id_SupplyChain);
                    return new MessageModel(true, "SupplyChains effacé(e)s", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel GetAllSupplyChain()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<SupplyChain> list = (from tmp in db.SupplyChain where tmp.IsSuppr == false select tmp).ToList();
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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
                    List<SupplyChain> list = (from tmp in db.SupplyChain
                                where tmp.IsSuppr == false && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.InterlocuteurId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.SuppleantId == utilisateurId && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des supplyChain", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
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